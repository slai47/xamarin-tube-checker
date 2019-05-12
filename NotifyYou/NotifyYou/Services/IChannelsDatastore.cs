using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.Services
{
    public interface IChannelsDataStore
    {
        void AddUpdate(StoredChannel item, NotificationSetting setting = null);
        bool Delete(string id);
        StoredChannel Get(string id);
        ICollection<StoredChannel> GetAllChannels();
        ICollection<NotificationSetting> GetAllSettings();
        NotificationSetting GetSetting(string id);
        bool UpdateNotificationSetting(string id, NotificationSetting setting);
        bool Exists(string channelId);
        Task<bool> Init();
    }
}
