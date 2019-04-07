using System;

namespace NotifyYou.Models.Channel
{
    public class ChannelSnippet
    {
        public string publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string channelTitle { get; set; }
        public ChannelId id {get;set;}
    }
}
