using System;
namespace NotifyYou.Models.Activity
{
    public class YoutubeSnippet
    {
        public string publishedAt { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string channelTitle { get; set; }
        public string type { get; set; }
        public string groupId { get; set; }
        public Thumbnails thumbnails { get; set; }
    }
}
