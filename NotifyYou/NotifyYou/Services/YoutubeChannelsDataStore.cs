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
        List<StoredChannel> channels;
        List<NotificationSetting> settings;
        bool mockMode;

        public YoutubeChannelsDataStore(bool mockMode)
        {
            channels = new List<StoredChannel>();
            settings = new List<NotificationSetting>();
            // grab stored data from saved data
            this.mockMode = mockMode;
            if (mockMode)
            {
                InsertMockData();
            }
        }


        #region Interface methods

        public void AddUpdate(StoredChannel item, NotificationSetting setting = null)
        {
            bool exists = channels.Exists(channel => channel.Id == item.Id);
            if (!exists)
            {
                channels.Add(item);
                if(setting == null)
                {
                    setting = new NotificationSetting(item.Id);
                    Save(setting);
                }
                settings.Add(setting);
                Save(item);
            }
            else
            {
                int index = FindIndexOfChannelId(item.Id);
                channels[index] = item;
                Update(item);
                if(setting != null)
                {
                    int settingIndex = FindIndexOfSettingId(item.Id);
                    settings[settingIndex] = setting;
                    Update(setting);
                }
            }

        }

        private bool Delete(string id)
        {
            StoredChannel channel = channels.First(c => c.Id == id);
            channels.Remove(channel);
            NotificationSetting setting = settings.First(c => channel.ChannelId == id);
            settings.Remove(setting);

            DeleteChannel(channel.Id, channel.ImageUri.Id);
            DeleteSetting(channel.ChannelId);

            return true;
        }

        public StoredChannel Get(string id)
        {
            StoredChannel channel = channels.Find(chan => chan.Id == id);
            return channel;
        }

        public ICollection<StoredChannel> GetAllChannels()
        {
            return channels;
        }

        public ICollection<NotificationSetting> GetAllSettings()
        {
            return settings;
        }

        public NotificationSetting GetSetting(string id)
        {
            NotificationSetting setting = settings.Find(set => set.ChannelId == id);
            return setting;
        }

        public bool UpdateNotificationSetting(string id, NotificationSetting setting)
        {
            int index = FindIndexOfSettingId(id);
            settings[index] = setting;
            return true;
        }

        public Task<bool> Init()
        {
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteConnection(databasePath);
            db.CreateTable<StoredChannel>();
            db.CreateTable<Thumbnails>();
            db.CreateTable<NotificationSetting>();

            channels = db.Table<StoredChannel>().ToList();
            settings = db.Table<NotificationSetting>().ToList();

            return Task.FromResult(true);
        }

        private void Save(StoredChannel channel)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteAsyncConnection(databasePath);
            db.InsertAsync(channel);
            db.InsertAsync(channel.ImageUri);
        }

        private void Update(StoredChannel channel)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteAsyncConnection(databasePath);
            db.UpdateAsync(channel);
            db.UpdateAsync(channel.ImageUri);
        }

        private void Save(NotificationSetting setting)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteAsyncConnection(databasePath);
            db.InsertAsync(setting);
        }

        private void Update(NotificationSetting setting)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteAsyncConnection(databasePath);
            db.UpdateAsync(setting);
        }

        private void DeleteChannel(String channelId, int thumbnailID = -1)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteConnection(databasePath);
            db.Delete(channelId, db.GetMapping<StoredChannel>());
            if(thumbnailID != -1)
            {
                db.Delete(thumbnailID, db.GetMapping<Thumbnails>());
            }
        }

        private void DeleteSetting(String settingId)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NotifyYou.db");

            var db = new SQLiteConnection(databasePath);
            db.Delete(settingId, db.GetMapping<NotificationSetting>());
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
            var mockItems = new List<StoredChannel>
            {
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "Phillip Defranco", Link="" },
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "MCGamerz", Link="" },
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "Brozime", Link="" },
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "NPR", Link="" },
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "Quiet Shallow", Link="" },
                new StoredChannel { Id = Guid.NewGuid().ToString(), Name = "Screen Junkies", Link="" },
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
