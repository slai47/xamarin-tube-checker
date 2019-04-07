using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.Services
{
    public interface IChannelsDataStore
    {
        Task<bool> AddUpdateAsync(StoredChannel item, NotificationSetting setting = null);
        Task<bool> DeleteAsync(string id);
        Task<StoredChannel> GetAsync(string id);
        Task<IEnumerable<StoredChannel>> GetAllChannelsAsync();
        Task<IEnumerable<NotificationSetting>> GetAllSettingsAsync();
        Task<NotificationSetting> GetSettingAsync(string id);
        Task<bool> UpdateNotificationSetting(string id, NotificationSetting setting);
    }
}
