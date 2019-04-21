using System;
using NotifyYou.Models.Channel;
using SQLite;

namespace NotifyYou.Models
{
    public class StoredChannel
    {
        public StoredChannel()
        {
        }

        public StoredChannel(YoutubeChannel channel)
        {
            ChannelId = channel.ChannelId;
            Name = channel.ChannelTitle;
            Link = "https://www.youtube.com/channel/" + ChannelId;
            ImageUri = channel.Snippet.Thumbnails;
        }

        [PrimaryKey]
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Thumbnails ImageUri { get; set; }
        public string LastVideoId { get; set; }
    }
}
