using System;
namespace NotifyYou.Models.Channel
{
    public class YoutubeChannel
    {
        public ChannelId id { get; set; }
        public ChannelSnippet snippet { get; set; }
        public string ChannelTitle
        {
            get
            {
                return snippet.channelTitle;
            }
        }
        public string ChannelId
        {
            get
            {
                return id.channelId;
            }
        }
    }
}
