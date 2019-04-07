﻿using System;
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
        const string API_KEY = "&key=AIzaSyBXr_YJWJIi5SSOCIESPeIaVb7bUGu4pSM";

        HttpClient _client;

        public YoutubeApi()
        {
            _client = new HttpClient();
        }

        public async Task<YoutubeCall<YoutubeActivity>> GetChannelActivity(string id)
        {
            YoutubeCall<YoutubeActivity> activity = new YoutubeCall<YoutubeActivity>();

            var uri = new Uri(GenerateUrl(ACTIIVTY, "&channelId=" + id ));

            var response = await _client.GetAsync(uri);
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

            var uri = new Uri(GenerateUrl(ACTIIVTY, "&q=" + search + "&type=channel"));

            var response = await _client.GetAsync(uri);
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

        private string GenerateUrl(string pathVar, string extras)
        {
            return BASE_YOUTUBE_URL + pathVar + "?" + PART + extras + API_KEY;
        }

    }

    public interface IYoutube
    {
        Task<YoutubeCall<YoutubeActivity>> GetChannelActivity(string id);
        Task<YoutubeCall<YoutubeChannel>> GetChannels(string search);
    }
}