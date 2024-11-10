using Microsoft.AspNetCore.Mvc;
using StockControl.Application.Requests;
using StockControl.Application.Requests.Product;
using StockControl.Application.UseCases;
using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ILogger<ProductController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Product>> RegisterProduct([FromBody] ProductPostRequest productPostRequest,
        [FromServices] RegisterProductUseCase useCase)
    {
        logger.LogInformation("Receive request to register a product with name: {}", productPostRequest.Name);
        var product = await useCase.Execute(productPostRequest);

        return CreatedAtAction(nameof(RegisterProduct), new { id = product.Id }, product);
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<List<ProductGetRequest>>> ListProducts([FromServices] ListAllProductsUseCase useCase)
    {
        logger.LogInformation("Receive request to list all products");
        return Ok(await useCase.Execute());
    }

    [HttpGet("list-all/filtered")]
    public async Task<ActionResult<List<Product>>> ListProductsFiltered([FromQuery] string cnpj,
        [FromServices] FindAllProductsBySupplierCnpjUseCase useCase)
    {
        logger.LogInformation("Receive request to list all products filtered");
        return Ok(await useCase.Execute(cnpj));
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteProducts([FromQuery] List<string> skuCodes,
        [FromServices] DeleteProductsByListOfSkuCodesUseCase useCase)
    {
        logger.LogInformation("Receive request to delete products of sku codes with size: {}", skuCodes.Count);
        await useCase.Execute(skuCodes);

        return NoContent();
    }

    [HttpPut("update")]
    public async Task<ActionResult<Product>> UpdateProduct(ProductPutRequest productPutRequest,
        [FromServices] UpdateProductInfosUseCase useCase)
    {
        logger.LogInformation("Receive request to update a product with name: {}", productPutRequest.Name);

        return Ok(await useCase.Execute(productPutRequest));
    }

    [HttpPost("check-stock")]
    public async Task<ActionResult<bool>> CheckStock([FromQuery] string skuCode,
        [FromQuery] int quantity,
        [FromServices] CheckProductStockUseCase useCase)
    {
        logger.LogInformation("Receive request to check stock with sku code: {}", skuCode);

        return Ok(await useCase.Execute(skuCode, quantity));
    }

    [HttpPost("register-sell")]
    public async Task<ActionResult> RegisterSell([FromBody] RegisterSellPostRequest request,
        [FromServices] RegisterProductSellUseCase useCase)
    {
        logger.LogInformation("Receive request to register a sell of list of products with size: {}",
            request.Products.Count);
        await useCase.Execute(request);

        return Created();
    }

    [HttpPatch("restock")]
    public async Task<ActionResult> Restock([FromBody] RestockProductDto request,
        [FromServices] RestockProductUseCase useCase)
    {
        logger.LogInformation("Receive request to restock a product with product sku: {}", request.ProductSku);
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPost("set-stock-limit")]
    public async Task<ActionResult> SetStockLimit([FromBody] SetStockPost setStockPost,
        [FromServices] SetStockLimitUseCase useCase)
    {
        logger.LogInformation("Receive request to set limit stock alert");
        await useCase.Execute(setStockPost);

        return NoContent();
    }

    [HttpGet("forecast-demand/{skuCode}/months/{months}")]
    public async Task<ProductForecastResponse> ForecastDemand(string skuCode, string months,
        [FromServices] ProductForecastUseCase useCase)
    {
        logger.LogInformation("Receive request to forecast demand of product with sku code: {}", skuCode);
        return await useCase.Execute(skuCode, int.Parse(months));
    }
}