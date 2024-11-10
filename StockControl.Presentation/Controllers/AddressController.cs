using Microsoft.AspNetCore.Mvc;
using StockControl.Application.Requests.Address;
using StockControl.Application.UseCases;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController(ILogger<SupplierController> logger) : ControllerBase
{
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAddress(string id,
        [FromServices] DeleteAddressByIdUseCase useCase)
    {
        logger.LogInformation("Receive request to delete address by id: {0}", id);

        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPost("add-address")]
    public async Task<ActionResult<AddressGetRequest>> AddAddress([FromBody] AddressPostRequest addressPostRequest,
        [FromServices] AddAddressInSupplierUseCase useCase)
    {
        logger.LogInformation("Receive request to add address in supplier with CNPJ: {0}", addressPostRequest.SupplierCnpj);

        await useCase.Execute(addressPostRequest);

        return NoContent();
    }
}