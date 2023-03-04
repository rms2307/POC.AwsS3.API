using Microsoft.AspNetCore.Mvc;
using POC.AWS.S3.API.Services.Interfaces;

namespace POC.AWS.S3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageService _service;

        public FileStorageController(IFileStorageService service)
        {
            _service = service;
        }

        [HttpPost("/upload-file")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file)
        {
            await _service.UploadFile(file);
            return Ok();
        }
    }
}