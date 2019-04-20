using System;

namespace NotifyYou.Models.Channel
{
    public class ChannelSnippet
    {
        public string PublishedAt { get; set; }
        public string ChannelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Thumbnails Thumbnails { get; set; }
        public string ChannelTitle { get; set; }
        public ChannelId Id {get;set;}
    }
}
