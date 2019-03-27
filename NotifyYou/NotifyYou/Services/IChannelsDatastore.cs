using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.Services
{
    public interface IChannelsDataStore
    {
        Task<bool> AddUpdateAsync(Channel item, NotificationSetting setting = null);
        Task<bool> DeleteAsync(string id);
        Task<Channel> GetAsync(string id);
        Task<IEnumerable<Channel>> GetAllChannelsAsync();
        Task<IEnumerable<NotificationSetting>> GetAllSettingsAsync();
        Task<NotificationSetting> GetSettingAsync(string id);
        Task<bool> UpdateNotificationSetting(string id, NotificationSetting setting);
    }
}
