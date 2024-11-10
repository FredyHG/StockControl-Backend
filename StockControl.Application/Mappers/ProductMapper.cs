using StockControl.Application.Requests;
using StockControl.Application.Requests.Product;
using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappers;

public class ProductMapper
{
    public static Product ToEntity(ProductPostRequest request, Supplier supplier)
    {
        return new Product(request.Name,
            request.Description,
            request.CostPrice,
            request.PricePerUnit,
            request.Stock,
            request.ImageUrl,
            supplier,
            request.CategoryId);
    }

    public static Product ToEntity(ProductPutRequest request)
    {
        return new Product(
            request.Name,
            request.SkuNumber,
            request.Description,
            request.CostPrice,
            request.PricePerUnit
        );
    }

    public static ProductGetRequest ToGetRequest(Product product)
    {
        return new ProductGetRequest(
            product.Name,
            product.SkuNumber,
            product.Description,
            product.CostPrice,
            product.PricePerUnit,
            product.Stock,
            product.MinStock,
            product.MaxStock,
            product.ImageUrl,
            product.Category,
            SupplierMapper.ToGetRequest(product.Supplier));
    }

    public static ProductForecastResponse dtoToResponse(ProductForecastDto dto)
    {
        return new ProductForecastResponse(dto.SkuCode, dto.ForecastedStockNextMonths, dto.SalesPerMonth);
    }
}