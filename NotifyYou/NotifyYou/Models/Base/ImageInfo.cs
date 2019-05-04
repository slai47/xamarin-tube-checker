using System;
using SQLite;

namespace NotifyYou.Models
{
    [Table("imageinfo")]
    public class ImageInfo
    {
        public string url { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }
}
