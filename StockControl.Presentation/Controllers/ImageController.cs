using Microsoft.AspNetCore.Mvc;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController(ILogger<SupplierController> logger) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile? image)
    {
        logger.LogInformation("Receive request to upload image with: ");

        if (image == null || image.Length == 0)
            return BadRequest("Error to save image.");

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var imageName = Guid.NewGuid() + Path.GetExtension(image.FileName);

        var filePath = Path.Combine(path, imageName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{imageName}";
        return Ok(new { imageUrl });
    }
}