using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.Services
{
    public class YoutubeChannelsDataStore : IDataStore<Channel>
    {
        List<Channel> channels;
        Dictionary<Channel, NotificationSetting> settings;

        public YoutubeChannelsDataStore()
        {
            channels = new List<Channel>();
            settings = new Dictionary<Channel, NotificationSetting>();

        }

        public Task<bool> AddItemAsync(Channel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Channel> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Channel>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Channel item)
        {
            throw new NotImplementedException();
        }
    }
}
