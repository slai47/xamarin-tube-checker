using System;
namespace NotifyYou.Models
{
    public class StoredChannel
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string ImageUri { get; set; }
        public string LastVideoId { get; set; }
    }
}
