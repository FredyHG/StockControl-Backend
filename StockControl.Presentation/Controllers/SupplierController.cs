using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StockControl.Application.Requests.Supplier;
using StockControl.Application.UseCases;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(ILogger<SupplierController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterSupplier([FromBody] SupplierPostRequest supplierPostRequest,
        [FromServices] RegisterSupplierUseCase useCase,
        [FromServices] IValidator<SupplierPostRequest> validator)
    {
        logger.LogInformation("Receive request to register supplier with name: {}", supplierPostRequest.Name);

        // var result = await validator.ValidateAsync(supplierPostRequest);
        //
        // if (!result.IsValid) return Conflict(new { Errors = result.Errors.Select(e => e.ErrorMessage) });

        var supplier = await useCase.Execute(supplierPostRequest);

        return Created(string.Empty, supplier);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteSuppliers([FromQuery] List<string> cnpjs,
        [FromServices] DeleteSuppliersByListOfCnpjUseCase useCase)
    {
        logger.LogInformation("Receive request to delete products of sku codes with size: {}", cnpjs.Count);
        await useCase.Execute(cnpjs);

        return NoContent();
    }

    [HttpGet("list-all")]
    public async Task<ActionResult<List<SupplierGetRequest>>> GetSuppliers(
        [FromServices] ListAllSuppliersUseCase useCase)
    {
        logger.LogInformation("Receive request to get suppliers");

        return Ok(await useCase.Execute());
    }
}