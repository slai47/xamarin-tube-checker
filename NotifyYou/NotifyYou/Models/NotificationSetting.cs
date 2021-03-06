﻿using System;
using SQLite;

namespace NotifyYou.Models
{
    [Table("notification_settings")]
    public class NotificationSetting
    {
        [PrimaryKey]
        public string ChannelId { get; set; }
        public bool Active { get; set; }
        public bool Sound { get; set; }
        public bool Vibrate { get; set; }

        public NotificationSetting(string id)
        {
            ChannelId = id;
            Active = true;
            Sound = false;
            Vibrate = false;
        }

        public NotificationSetting()
        {

        }
    }
}
