using System;
namespace NotifyYou.Models.Events
{
    public class ChannelViewUrlEvent
    {
        public string ChannelId { get; set; }
        public ChannelViewUrlEvent(string channelId)
        {
            ChannelId = channelId;
        }
    }
}
