using Microsoft.AspNetCore.Mvc;
using StockControl.Application.Requests.Contact;
using StockControl.Application.UseCases;
using StockControl.Domain.Entities;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController(
    FindContactByIdUseCase findContactByIdUseCase,
    DeleteContactByIdAndSupplierIdUseCase deleteContactByIdAndSupplierIdUseCase,
    ILogger<SupplierController> logger)
    : ControllerBase
{
    [HttpGet("find")]
    public async Task<ActionResult<Contact>> FindById(string contactId)
    {
        logger.LogInformation("receive request to find contact by ID: {}", contactId);

        return Ok(await findContactByIdUseCase.Execute(contactId));
    }

    [HttpDelete("delete/{contactId}")]
    public async Task<ActionResult> DeleteById(string contactId)
    {
        logger.LogInformation("receive request to delete contact by ID: {}", contactId);

        await deleteContactByIdAndSupplierIdUseCase.Execute(contactId);
        return NoContent();
    }

    [HttpPost("add-contact")]
    public async Task<ActionResult<ContactGetRequest>> AddContact([FromBody] ContactPostRequest contactPostRequest,
        [FromServices] AddContactInSupplierUseCase useCase)
    {
        logger.LogInformation("Receive request to add contact in supplier with CNPJ: {}",
            contactPostRequest.SupplierCnpj);

        await useCase.Execute(contactPostRequest);

        return NoContent();
    }
}