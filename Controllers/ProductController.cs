using Microsoft.AspNetCore.Mvc;
using updaterKC13.DTOs;
using updaterKC13.Models;
using updaterKC13.Services;

namespace updaterKC13.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly VersionService _versionService;

        public ProductController(VersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet]
        public IActionResult GetInfo()
        {
            var data = _versionService.GetProductInfo();
            return Ok(data);
        }

        [HttpGet("check-update")]
        public IActionResult CheckUpdate([FromQuery] string version)
        {
            if (string.IsNullOrEmpty(version))
                return BadRequest("version parameter required");

            var productInfo = _versionService.GetProductInfo();
            var updateAvailable = _versionService.IsUpdateAvailable(version);

            var response = new CheckUpdateResponseDto
            {
                Product = productInfo.Product,
                Current = productInfo.Current,
                History = productInfo.History ?? new List<Release>(),
                UpdateAvailable = updateAvailable
            };

            return Ok(response);
        }
    }
}
