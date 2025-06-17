using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadFileApi.Data;
using UploadFileApi.Entities;

namespace UploadFileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ImagesController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var imageEntity= new ImageEntity { FileName=file.FileName };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                imageEntity.Data = memoryStream.ToArray();
            }

            await _dbContext.Images.AddAsync(imageEntity);
            await _dbContext.SaveChangesAsync();

            return Ok(imageEntity.Id);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var imageEntity = await _dbContext.Images.FirstOrDefaultAsync(image => image.Id == id);

            if (imageEntity == null)
                return NotFound();

            var fileContentResult = new FileContentResult(imageEntity.Data, "application/octet-stream")
            {
                FileDownloadName = imageEntity.FileName
            };

            return fileContentResult;

        }
    }
}
