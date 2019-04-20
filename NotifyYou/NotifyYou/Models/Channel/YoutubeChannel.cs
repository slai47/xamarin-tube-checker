using System;
namespace NotifyYou.Models.Channel
{
    public class YoutubeChannel
    {
        public ChannelId id { get; set; }
        public ChannelSnippet Snippet { get; set; }
        public string ChannelTitle
        {
            get
            {
                return Snippet.channelTitle;
            }
        }
        public string ChannelId
        {
            get
            {
                return id.channelId;
            }
        }
        public bool IsActive { get; set; }
    }
}
