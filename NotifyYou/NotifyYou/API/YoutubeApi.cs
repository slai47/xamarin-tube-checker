using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotifyYou.Models;

namespace NotifyYou.API
{
    public class YoutubeApi : IYoutube
    {
        public YoutubeApi()
        {
        }

        public async IEnumerable<Channel> GetChannels(string search = null)
        {
            List<Channel> channels = new List<Channel>();




            return channels;
        }
    }

    public interface IYoutube
    {
        Task<IEnumerable<Channel>> GetChannels(string search = null);
    }
}
