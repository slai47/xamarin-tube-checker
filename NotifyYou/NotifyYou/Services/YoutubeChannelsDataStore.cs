using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.Services
{
    public class YoutubeChannelsDataStore : IChannelsDataStore
    {
        List<Channel> channels;
        List<NotificationSetting> settings;
        bool mockMode;

        public YoutubeChannelsDataStore()
        {
            channels = new List<Channel>();
            settings = new List<NotificationSetting>();
            // grab stored data from saved data
            //this.mockMode = mockMode;
            //if (mockMode)
            //{
            //    InsertMockData();
            //}
        }


        #region Interface methods

        public async Task<bool> AddUpdateAsync(Channel item, NotificationSetting setting = null)
        {
            bool exists = channels.Exists(channel => channel.Id == item.Id);
            if (!exists)
            {
                channels.Add(item);
                if(setting == null)
                {
                    setting = new NotificationSetting(item.Id);
                }
                settings.Add(setting);
            }
            else
            {
                int index = FindIndexOfChannelId(item.Id);
                channels[index] = item;
                if(setting != null)
                {
                    int settingIndex = FindIndexOfSettingId(item.Id);
                    settings[settingIndex] = setting;
                }
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            channels.RemoveAll(channel => channel.Id == id);
            settings.RemoveAll(setting => setting.ChannelId == id);

            return await Task.FromResult(true);
        }

        public async Task<Channel> GetAsync(string id)
        {
            Channel channel = channels.Find(chan => chan.Id == id);
            return await Task.FromResult(channel);
        }

        public async Task<IEnumerable<Channel>> GetAllChannelsAsync()
        {
            return await Task.FromResult(channels);
        }

        public async Task<IEnumerable<NotificationSetting>> GetAllSettingsAsync()
        {
            return await Task.FromResult(settings);
        }

        public async Task<NotificationSetting> GetSettingAsync(string id)
        {
            NotificationSetting setting = settings.Find(set => set.ChannelId == id);
            return await Task.FromResult(setting);
        }

        public async Task<bool> UpdateNotificationSetting(string id, NotificationSetting setting)
        {
            int index = FindIndexOfSettingId(id);
            settings[index] = setting;
            return await Task.FromResult(true);
        }

        #endregion

        #region private methods

        private int FindIndexOfChannelId(string id)
        {
            return channels.FindIndex(channel => channel.Id == id);
        }

        private int FindIndexOfSettingId(string id)
        {
            return settings.FindIndex(set => set.ChannelId == id);
        }

        #endregion

        #region mocking

        public void InsertMockData()
        {
            var mockItems = new List<Channel>
            {
                new Channel { Id = Guid.NewGuid().ToString(), Name = "Phillip Defranco", Link="" },
                new Channel { Id = Guid.NewGuid().ToString(), Name = "MCGamerz", Link="" },
                new Channel { Id = Guid.NewGuid().ToString(), Name = "Brozime", Link="" },
                new Channel { Id = Guid.NewGuid().ToString(), Name = "NPR", Link="" },
                new Channel { Id = Guid.NewGuid().ToString(), Name = "Quiet Shallow", Link="" },
                new Channel { Id = Guid.NewGuid().ToString(), Name = "Screen Junkies", Link="" },
            };

            var mockSettings = new List<NotificationSetting>
            {
                new NotificationSetting(mockItems[0].Id) { Active = false, Sound = false },
                new NotificationSetting(mockItems[1].Id) { Active = false, Sound = false },
                new NotificationSetting(mockItems[2].Id) { Active = false, Sound = false },
                new NotificationSetting(mockItems[3].Id) { Active = false, Sound = false },
                new NotificationSetting(mockItems[4].Id) { Active = false, Sound = false },
                new NotificationSetting(mockItems[5].Id) { Active = false, Sound = false }
            };

            channels.AddRange(mockItems);
            settings.AddRange(mockSettings);
        }

        #endregion
    }
}
