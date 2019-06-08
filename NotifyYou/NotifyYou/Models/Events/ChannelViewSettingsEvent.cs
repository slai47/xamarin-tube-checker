using System;
namespace NotifyYou.Models.Events
{
    public class ChannelViewSettingsEvent
    {
        public string ChannelId { get; set; }

        public ChannelViewSettingsEvent(string channelId)
        {
            ChannelId = channelId;
        }
    }
}
