namespace updaterKC13.Models
{
    public class ProductInfo
    {
        public string Product { get; set; }
        public Release Current { get; set; }
        public List<Release> History { get; set; }
    }

    public class Release
    {
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public bool Mandatory { get; set; }
        public DownloadInfo Download { get; set; }
        public ChangeLog Changelog { get; set; }
        public Links Links { get; set; }
    }

    public class DownloadInfo
    {
        public string Url { get; set; }
        public long FileSize { get; set; }
        public string Sha256 { get; set; }
    }

    public class ChangeLog
    {
        public List<string> Features { get; set; }
        public List<string> Improvements { get; set; }
        public List<string> Fixes { get; set; }
    }

    public class Links
    {
        public string Documentation { get; set; }
        public string ReleaseNotes { get; set; }
    }
}
