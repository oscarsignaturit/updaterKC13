namespace updaterKC13.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Cryptography;
    using System.Text.Json;
    using updaterKC13.Models;

    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public AdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("update-metadata")]
        public async Task<IActionResult> UpdateMetadata()
        {
            var dataPath = Path.Combine(_env.ContentRootPath, "Data", "version.json");
            var downloadsPath = Path.Combine(_env.ContentRootPath, "Downloads");

            if (!System.IO.File.Exists(dataPath))
                return NotFound("version.json not found");

            var json = await System.IO.File.ReadAllTextAsync(dataPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            var productInfo = JsonSerializer.Deserialize<ProductInfo>(json, options);

            if (productInfo?.Current?.Download?.Url == null)
                return BadRequest("Invalid JSON structure");

            // 🔍 obtener nombre del archivo desde la URL
            var fileName = Path.GetFileName(new Uri(productInfo.Current.Download.Url).LocalPath);
            var filePath = Path.Combine(downloadsPath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound($"File not found: {fileName}");

            // 📏 FILE SIZE
            var fileInfo = new FileInfo(filePath);
            var fileSize = fileInfo.Length;

            // 🔐 SHA256
            string sha256Hash;
            await using (var stream = System.IO.File.OpenRead(filePath))
            {
                using var sha256 = SHA256.Create();
                var hash = await sha256.ComputeHashAsync(stream);
                sha256Hash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }

            // ✏️ actualizar JSON
            productInfo.Current.Download.FileSize = fileSize;
            productInfo.Current.Download.Sha256 = sha256Hash;

            // 💾 guardar
            var updatedJson = JsonSerializer.Serialize(productInfo, options);
            await System.IO.File.WriteAllTextAsync(dataPath, updatedJson);

            return Ok(new
            {
                fileName,
                fileSize,
                sha256 = sha256Hash
            });
        }
    }
}
