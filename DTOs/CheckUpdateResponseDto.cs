using updaterKC13.Models;

namespace updaterKC13.DTOs
{
    public class CheckUpdateResponseDto
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the current release information of the product.
        /// </summary>
        public Release Current { get; set; }

        /// <summary>
        /// Gets or sets the list of previous product releases.
        /// </summary>
        public List<Release> History { get; set; }

        public bool UpdateAvailable { get; set; }
    }
}
