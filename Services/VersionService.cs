using System.Text.Json;
using updaterKC13.Models;

namespace updaterKC13.Services
{
    public class VersionService
    {
        private readonly IWebHostEnvironment _env;
        private ProductInfo _cache;

        public VersionService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ProductInfo GetProductInfo()
        {
            if (_cache != null)
                return _cache;

            var path = Path.Combine(_env.ContentRootPath, "Data", "version.json");
            var json = File.ReadAllText(path);

            _cache = JsonSerializer.Deserialize<ProductInfo>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return _cache;
        }

        public bool IsUpdateAvailable(string clientVersion)
        {
            var data = GetProductInfo();

            var client = new Version(clientVersion);
            var current = new Version(data.Current.Version);

            return current > client;
        }
    }
}
