using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockControl.Domain.Dto;
using StockControl.Domain.Entities;
using StockControl.Domain.Enums;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;
using IStockHistoryRepository = StockControl.Domain.Repository.IStockHistoryRepository;

namespace StockControl.Domain.Services;

public class ProductService(
    IProductRepository productRepository,
    IStockHistoryRepository stockHistoryRepository,
    IMqService mqService,
    ILogger<ProductService> logger) : IProductService
{
    public Task<Product> RegisterProduct(Product product)
    {
        logger.LogInformation("Starting RegisterProduct for product: {ProductName}", product.Name);

        product.SkuNumber = GenSku(product.Id, product.Name);
        
        var result = productRepository.AddAsync(product);
        logger.LogInformation("Successfully registered product with SKU: {SkuNumber}", product.SkuNumber);

        return result;
    }

    public async Task<List<Product>> ListAllProductsAsync()
    {
        logger.LogInformation("Starting ListAllProductsAsync to retrieve all products.");

        return await productRepository.GetAllAsync(query =>
            query.Include(prod => prod.Supplier)
                .ThenInclude(s => s.Contacts)
                .Include(prod => prod.Supplier)
                .ThenInclude(s => s.Addresses)
                .Include(cat => cat.Category));
    }

    public Task<List<Product>> FindAllProductsFiltered(string cpnj)
    {
        logger.LogInformation("Starting FindAllProductsFiltered with CNPJ: {0}", cpnj);

        return productRepository.FindAllBySupplierCnpj(cpnj);
    }

    public async Task DeleteProductBySkuCode(string skuCode)
    {
        logger.LogInformation("Starting DeleteProductBySkuCode for SKU: {SkuCode}", skuCode);

        await productRepository.DeleteBySkuCode(skuCode);

        logger.LogInformation("Successfully deleted product with SKU: {SkuCode}", skuCode);
    }

    public async Task DeleteAllProductsByListOfSkuCodes(List<string> skuCodes)
    {
        logger.LogInformation("Starting DeleteAllProductsByListOfSkuCodes for SKU codes: {SkuCodes}",
            string.Join(", ", skuCodes));

        await productRepository.DeleteByListOfSkuCode(skuCodes);

        logger.LogInformation("Successfully deleted products with SKU codes: {SkuCodes}", string.Join(", ", skuCodes));
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        logger.LogInformation("Starting UpdateProduct for SKU: {SkuNumber}", product.SkuNumber);

        var updatedProduct = await productRepository.UpdateAsync(product);

        logger.LogInformation("Successfully updated product with SKU: {SkuNumber}", updatedProduct.SkuNumber);

        return updatedProduct;
    }

    public async Task<Product> FindBySku(string sku)
    {
        logger.LogInformation("Starting FindBySku for SKU: {Sku}", sku);
        return await productRepository.FindByAsync(s => s.SkuNumber == sku,
            products => products.Include(p => p.Category));
    }

    public async Task<bool> CheckStock(string productSku, int quantity)
    {
        logger.LogInformation("Starting CheckStock for SKU: {Sku} with requested quantity: {Quantity}", productSku,
            quantity);
        var productExists = await FindBySku(productSku);

        logger.LogInformation("Product found with SKU: {Sku}. Available stock: {AvailableStock}", productSku,
            productExists.Stock);


        return productExists.Stock < quantity;
    }

    public async Task RegisterProductSellAsync(List<ProductSellDto> requestProducts)
    {
        logger.LogInformation("Starting RegisterProductSellAsync with {ProductCount} products.", requestProducts.Count);

        var productsToUpdate = new List<Product>();
        var stockHistories = new List<StockHistory>();
        var transactionId = Guid.NewGuid().ToString();

        foreach (var soldProduct in requestProducts)
        {
            logger.LogInformation("Processing sale for product SKU: {Sku}, Quantity: {Quantity}",
                soldProduct.ProductSku, soldProduct.Quantity);

            var product = await EnsureProductExistsBySkuCode(soldProduct.ProductSku);

            if (product.Stock < soldProduct.Quantity)
            {
                logger.LogWarning(
                    "Insufficient stock for product SKU: {Sku}. Requested: {RequestedQuantity}, Available: {AvailableStock}",
                    soldProduct.ProductSku, soldProduct.Quantity, product.Stock);
                throw new InvalidOperationException($"Insufficient stock for product SKU: {soldProduct.ProductSku}");
            }

            var previousStock = product.Stock;
            product.Stock -= soldProduct.Quantity;
            logger.LogInformation("Product SKU: {Sku}. Stock updated from {PreviousStock} to {NewStock}.",
                product.SkuNumber, previousStock, product.Stock);

            productsToUpdate.Add(product);
            stockHistories.Add(CreateStockHistory(transactionId, product, previousStock - product.Stock, previousStock,
                "Sell product"));
        }

        CheckMinProductQuantity(productsToUpdate);

        await stockHistoryRepository.AddAllAsync(stockHistories);
        await productRepository.UpdateAllAsync(productsToUpdate);

        logger.LogInformation("Successfully completed product sale transaction with ID: {TransactionId}",
            transactionId);
    }


    public async Task<Product> RestockProductAsync(RestockProductDto restockProductDto)
    {
        var transactionId = Guid.NewGuid().ToString();
        logger.LogInformation("Starting RestockProductAsync for SKU: {Sku}, Quantity: {Quantity}",
            restockProductDto.ProductSku, restockProductDto.Quantity);

        var productExists = await EnsureProductExistsBySkuCode(restockProductDto.ProductSku);

        var previousStock = productExists.Stock;
        productExists.Stock += restockProductDto.Quantity;

        logger.LogInformation("Product SKU: {Sku}. Stock updated from {PreviousStock} to {NewStock}.",
            productExists.SkuNumber, previousStock, productExists.Stock);

        CheckMaxProductQuantity(productExists);

        var stockHistory = CreateStockHistory(transactionId, productExists, productExists.Stock, previousStock,
            "Restock product");

        await stockHistoryRepository.AddAsync(stockHistory);
        await productRepository.UpdateAsync(productExists);

        return productExists;
    }

    public async Task SetProductStockLimit(string productSku, int minStock, int maxStock)
    {
        var product = await EnsureProductExistsBySkuCode(productSku);

        product.MinStock = minStock;
        product.MaxStock = maxStock;

        await productRepository.UpdateAsync(product);
    }

    public async Task<ProductForecastDto> ForecastBySku(string skuCode, int months)
    {
        await EnsureProductExistsBySkuCode(skuCode);

        var totalUnitsAsync = await stockHistoryRepository.GetTotalUnitsAsync(months, skuCode);

        var forecastedStock = totalUnitsAsync / months;

        var salesPerMonth = stockHistoryRepository.GetSalesByMonthForCurrentYear();

        return new ProductForecastDto(skuCode, forecastedStock, salesPerMonth);
    }

    private StockHistory CreateStockHistory(string transactionId, Product product, int quantity, int previousStock,
        string transactionType)
    {
        logger.LogInformation(
            "Creating StockHistory for SKU: {Sku}, Transaction ID: {TransactionId}, Quantity: {Quantity}, Previous Stock: {PreviousStock}, Transaction Type: {TransactionType}",
            product.SkuNumber, transactionId, quantity, previousStock, transactionType);

        return new StockHistory(
            transactionId,
            product.SkuNumber,
            "Successfully",
            quantity,
            previousStock,
            product.PricePerUnit * quantity,
            product.CostPrice * quantity,
            "Stock Update",
            product.Category.Name
        );
    }

    private void CheckMaxProductQuantity(Product product)
    {
        logger.LogInformation(
            "Checking maximum stock quantity for SKU: {Sku}. Current Stock: {CurrentStock}, Minimum Stock: {MinStock}",
            product.SkuNumber, product.Stock, product.MinStock);

        if (product.Stock < product.MinStock) return;

        logger.LogWarning("Stock for SKU: {Sku} has exceeded minimum threshold. Sending notifications.",
            product.SkuNumber);

        SendStockLimitNotification(NotificationMethod.EMAIL, product);

        // DEMONSTRATION
        SendStockLimitNotification(NotificationMethod.SMS, product);
        SendStockLimitNotification(NotificationMethod.WHATSAPP, product);

        logger.LogInformation("Notifications sent for SKU: {Sku}.", product.SkuNumber);
    }


    private void CheckMinProductQuantity(List<Product> products)
    {
        logger.LogInformation("Checking minimum stock quantity for {ProductCount} products.", products.Count);

        foreach (var product in products)
        {
            logger.LogInformation("Checking SKU: {Sku}. Current Stock: {CurrentStock}, Minimum Stock: {MinStock}",
                product.SkuNumber, product.Stock, product.MinStock);

            if (product.Stock > product.MinStock) continue;

            logger.LogWarning("Stock for SKU: {Sku} is below the minimum threshold. Sending notifications.",
                product.SkuNumber);

            SendStockLimitNotification(NotificationMethod.EMAIL, product);

            // DEMONSTRATION
            SendStockLimitNotification(NotificationMethod.SMS, product);
            SendStockLimitNotification(NotificationMethod.WHATSAPP, product);

            logger.LogInformation("Notifications sent for SKU: {Sku} due to low stock.", product.SkuNumber);
        }
    }

    private async Task<Product> EnsureProductExistsBySkuCode(string skuCode)
    {
        logger.LogInformation("Checking existence of product with SKU: {SkuCode}", skuCode);

        var productExists = await FindBySku(skuCode);

        if (productExists == null)
        {
            logger.LogError("Product with SKU: {SkuCode} not found.", skuCode);
            throw new KeyNotFoundException($"Product with SKU {skuCode} not found.");
        }

        logger.LogInformation("Product with SKU: {SkuCode} exists.", skuCode);
        return productExists;
    }

    private void SendStockLimitNotification(NotificationMethod notificationMethod, Product product)
    {
        logger.LogInformation(
            "Preparing to send stock limit notification for SKU: {Sku}, Notification Method: {NotificationMethod}",
            product.SkuNumber, notificationMethod.ToString());

        var msg = JsonSerializer.Serialize(new NotificationDto(
            notificationMethod.ToString(),
            product.Name,
            product.Stock,
            product.MinStock,
            product.MaxStock,
            "pedrobling@tuamaeaquelaursa.com")); //Change to email address

        mqService.SendMessage(msg);

        logger.LogInformation("Notification sent for SKU: {Sku} using method: {NotificationMethod}. Message: {Message}",
            product.SkuNumber, notificationMethod.ToString(), msg);
    }

    private string GenSku(string productId, string productName)
    {
        logger.LogInformation("Generating SKU for Product ID: {ProductId}, Product Name: {ProductName}", productId,
            productName);

        var sku = string.Concat(productName.AsSpan(0, 3), productId.AsSpan(0, 3)).ToUpper();

        logger.LogInformation("Generated SKU: {Sku}", sku);

        return sku;
    }
}