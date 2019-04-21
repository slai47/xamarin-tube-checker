using System;
using SQLite;

namespace NotifyYou.Models
{
    public class NotificationSetting
    {
        [PrimaryKey]
        public string ChannelId { get; set; }
        public bool Active { get; set; }
        public bool Sound { get; set; }

        public NotificationSetting(string id)
        {
            ChannelId = id;
        }

        public NotificationSetting()
        {

        }
    }
}
