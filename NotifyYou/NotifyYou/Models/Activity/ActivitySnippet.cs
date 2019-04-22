using System;
namespace NotifyYou.Models.Activity
{
    public class ActivitySnippet
    {
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChannelTitle { get; set; }
        public string Type { get; set; }
        public string GroupId { get; set; }
        public Thumbnails Thumbnails { get; set; }
    }
}
