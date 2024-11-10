using Microsoft.AspNetCore.Mvc;
using StockControl.Application.UseCases;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("list-all")]
    public async Task<ActionResult> GetCategories([FromServices] ListAllCategoriesUseCase useCase)
    {
        return Ok(await useCase.Execute());
    }
}