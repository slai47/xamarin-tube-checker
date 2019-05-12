using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotifyYou.Models;
using NotifyYou.Models.Activity;
using NotifyYou.Models.Channel;

namespace NotifyYou.API
{
    public class YoutubeApi : IYoutube
    {

        const string BASE_YOUTUBE_URL = "https://www.googleapis.com/youtube/v3/";
        const string SEARCH = "search";
        const string CHANNELS = "channels";
        const string ACTIIVTY = "activities";
        const string PART = "part=snippet";
        const string PART_CONTENTS = "part=snippet%2CcontentDetails";
        const string API_KEY = "&key=";

        HttpClient _client;

        public YoutubeApi()
        {
            _client = new HttpClient();
        }

        public async Task<YoutubeCall<YoutubeActivity>> GetChannelActivity(string id)
        {
            YoutubeCall<YoutubeActivity> activity = new YoutubeCall<YoutubeActivity>();

            var uri = new Uri(GenerateUrl(ACTIIVTY, "&channelId=" + id, true));

            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                activity = JsonConvert.DeserializeObject<YoutubeCall<YoutubeActivity>>(content);
            }
            else
            {
                activity.code = response.StatusCode;

            }

            return await Task.FromResult(activity);
        }

        public async Task<YoutubeCall<YoutubeChannel>> GetChannels(string search)
        {
            YoutubeCall<YoutubeChannel> activity = new YoutubeCall<YoutubeChannel>();

            var uri = new Uri(GenerateUrl(SEARCH, "&q=" + search + "&type=channel", false));

            var response = await _client.GetAsync(uri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                activity = JsonConvert.DeserializeObject<YoutubeCall<YoutubeChannel>>(content);
            }
            else
            {
                activity.code = response.StatusCode;
            }

            return await Task.FromResult(activity);
        }

        private string GenerateUrl(string pathVar, string extras, bool containParts)
        {
            string part = !containParts ? PART : PART_CONTENTS;
            return BASE_YOUTUBE_URL + pathVar + "?" + part  + extras + API_KEY + YoutubeApiKey.Key;
        }

    }

    public interface IYoutube
    {
        Task<YoutubeCall<YoutubeActivity>> GetChannelActivity(string id);
        Task<YoutubeCall<YoutubeChannel>> GetChannels(string search);
    }
}
