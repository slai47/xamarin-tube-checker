using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NotifyYou.Models;
using SQLite;

namespace NotifyYou.Services
{
    public class YoutubeChannelsDataStore : IChannelsDataStore
    {
        List<StoredChannel> Channels;
        List<NotificationSetting> Settings;
        bool MockMode;

        private static object CollisionLock = new object();

        private SQLiteAsyncConnection DbConnection;

        public YoutubeChannelsDataStore(bool mockMode)
        {
            Channels = new List<StoredChannel>();
            Settings = new List<NotificationSetting>();
            // grab stored data from saved data
            MockMode = mockMode;
            if (mockMode)
            {
                InsertMockData();
            }
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");
            DbConnection = new SQLiteAsyncConnection(databasePath);
        }


        #region Interface methods

        public void AddUpdate(StoredChannel item, NotificationSetting setting = null)
        {
            bool exists = Exists(item.ChannelId);
            if (!exists)
            {
                Channels.Add(item);
                if(setting == null)
                {
                    setting = new NotificationSetting(item.ChannelId);
                    Save(setting);
                }
                Settings.Add(setting);
                Save(item);
            }
            else
            {
                int index = FindIndexOfChannelId(item.ChannelId);
                Channels[index] = item;
                Update(item);
                if(setting != null)
                {
                    int settingIndex = FindIndexOfSettingId(item.ChannelId);
                    Settings[settingIndex] = setting;
                    Update(setting);
                }
            }
        }

        public void AddUpdate(NotificationSetting setting)
        {
            int settingIndex = FindIndexOfSettingId(setting.ChannelId);
            Settings[settingIndex] = setting;
            Update(setting);
        }

        public bool Exists(string channelId)
        {
            return Channels.Exists(channel => channel.ChannelId == channelId);
        }

        public bool Delete(string id)
        {
            StoredChannel channel = Get(id);
            Channels.Remove(channel);
            NotificationSetting setting = Settings.First(c => channel.ChannelId == id);
            Settings.Remove(setting);

            DeleteChannel(channel.ChannelId);
            DeleteSetting(channel.ChannelId);

            return true;
        }

        public StoredChannel Get(string id)
        {
            StoredChannel channel = Channels.Find(chan => chan.ChannelId == id);
            return channel;
        }

        public ICollection<StoredChannel> GetAllChannels()
        {
            return Channels;
        }

        public ICollection<NotificationSetting> GetAllSettings()
        {
            return Settings;
        }

        public NotificationSetting GetSetting(string id)
        {
            NotificationSetting setting = Settings.Find(set => set.ChannelId == id);
            return setting;
        }

        public bool UpdateNotificationSetting(string id, NotificationSetting setting)
        {
            int index = FindIndexOfSettingId(id);
            Settings[index] = setting;
            return true;
        }

        public Task<bool> Init()
        {
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteConnection(databasePath);
            db.CreateTable<StoredChannel>();
            db.CreateTable<NotificationSetting>();

            Channels = db.Table<StoredChannel>().ToList();
            Settings = db.Table<NotificationSetting>().ToList();
            return Task.FromResult(true);
        }

        private void Save(StoredChannel channel)
        {
            lock (CollisionLock)
            {
                DbConnection.InsertAsync(channel);
            }
        }

        private void Update(StoredChannel channel)
        {
            lock (CollisionLock)
            {
                DbConnection.UpdateAsync(channel);
            }
        }

        private void Save(NotificationSetting setting)
        {
            lock (CollisionLock)
            {
                DbConnection.InsertAsync(setting);
            }
        }

        private void Update(NotificationSetting setting)
        {
            lock (CollisionLock)
            {
                DbConnection.UpdateAsync(setting);
            }
        }

        private void DeleteChannel(String channelId)
        {
            lock (CollisionLock)
            {
                DbConnection.DeleteAsync<StoredChannel>(channelId);
            }
        }

        private void DeleteSetting(String settingId)
        {
            lock (CollisionLock)
            {
                DbConnection.DeleteAsync<NotificationSetting>(settingId);
            }
        }

        #endregion

        #region private methods

        private int FindIndexOfChannelId(string id)
        {
            return Channels.FindIndex(channel => channel.ChannelId == id);
        }

        private int FindIndexOfSettingId(string id)
        {
            return Settings.FindIndex(set => set.ChannelId == id);
        }
         
        #endregion

        #region mocking

        public void InsertMockData()
        {
            var mockItems = new List<StoredChannel>
            {
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "Phillip Defranco", Link="" },
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "MCGamerz", Link="" },
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "Brozime", Link="" },
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "NPR", Link="" },
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "Quiet Shallow", Link="" },
                new StoredChannel { ChannelId = Guid.NewGuid().ToString(), Name = "Screen Junkies", Link="" },
            };

            var mockSettings = new List<NotificationSetting>
            {
                new NotificationSetting(mockItems[0].ChannelId) { Active = false, Sound = false },
                new NotificationSetting(mockItems[1].ChannelId) { Active = false, Sound = false },
                new NotificationSetting(mockItems[2].ChannelId) { Active = false, Sound = false },
                new NotificationSetting(mockItems[3].ChannelId) { Active = false, Sound = false },
                new NotificationSetting(mockItems[4].ChannelId) { Active = false, Sound = false },
                new NotificationSetting(mockItems[5].ChannelId) { Active = false, Sound = false }
            };

            Channels.AddRange(mockItems);
            Settings.AddRange(mockSettings);
        }


        #endregion
    }
}
