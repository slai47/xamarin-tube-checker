﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotifyYou.API;
using NotifyYou.Models;
using NotifyYou.Models.Activity;
using Xamarin.Forms;
using System.Linq;
using NotifyYou.Models.Events;

namespace NotifyYou.ViewModels
{
    public class ChannelsViewModel : BaseViewModel
    {
        public const string EVENT_ACTIVITY = "Activity";
        public const string EVENT_ADDREMOVE = "addRemove";

        public ObservableCollection<StoredChannel> Channels = new ObservableCollection<StoredChannel>();

        public bool _isProgressVisible;
        public bool IsProgressVisible
        {
            get
            {
                return _isProgressVisible;
            }
            set
            {
                _isProgressVisible = value;
                OnPropertyChanged(nameof(IsProgressVisible));
            }
        }

        public ChannelsViewModel()
        {
            SetupSubscribes();
            CallForActivity(false);
        }

        private void SetupSubscribes()
        {
            MessagingCenter.Subscribe<ChannelActivityEvent>(this, EVENT_ACTIVITY, (activity) =>
            {
                StoredChannel channel = Channels.First(c => c.ChannelId == activity.ChannelId);
                YoutubeActivity latest = activity.Result.items.OrderBy(act => act.Snippet.PublishedAt).First();
                channel.LastVideoId = latest.Id;
                channel.LastVideoImageLink = latest.ImageLink;
                channel.Activity = latest;
                App.ChannelsDatastore.AddUpdate(channel);
                
            });
            MessagingCenter.Subscribe<ChannelsAddRemoveEvent>(this, EVENT_ADDREMOVE, (addRemoveEvent) =>
            {
                string channelId = addRemoveEvent.Id;
                if (addRemoveEvent.Add)
                {
                    StoredChannel channel = App.ChannelsDatastore.Get(channelId);
                    Channels.Add(channel);
                } else
                {
                    try
                    {
                        var channel = Channels.First(c => c.ChannelId == channelId);
                        Channels.Remove(channel);
                    } 
                    catch (Exception)
                    {
                    }
                }
            });
        }

        public void CallForActivity(bool force)
        {
            IYoutube api = new YoutubeApi();
            ChannelsViewModel vm = this;
            if (force || Channels.Count == 0)
                IsProgressVisible = true;
            else if (force)
            {
                Channels.Clear();
            }
            var channels = App.ChannelsDatastore.GetAllChannels();
            foreach (StoredChannel channel in channels)
            {
                if(channel.Activity == null || force)
                    Task.Run(() => {
                        Task<YoutubeCall<YoutubeActivity>> result = Task.Run(() => api.GetChannelActivity(channel.ChannelId));
                        var webResult = result.Result;
                        OnActivityReceive(new ChannelActivityEvent(channel.ChannelId, webResult));
                    });
            }
        }

        private void OnActivityReceive(ChannelActivityEvent activityEvent)
        {
            StoredChannel channel = App.ChannelsDatastore.GetAllChannels().First(c => c.ChannelId == activityEvent.ChannelId);
            YoutubeActivity latest = activityEvent.Result.items.OrderByDescending(act => act.Snippet.PublishedAt).First();
            channel.LastVideoId = latest.Id;
            channel.LastVideoImageLink = latest.ImageLink;
            channel.LastVideoTitle = latest.Snippet.Title;
            channel.LastVideoTime = latest.Snippet.PublishedAt.ToShortDateString() + " " + latest.Snippet.PublishedAt.ToShortTimeString();
            channel.Activity = latest;
            App.ChannelsDatastore.AddUpdate(channel);
            Channels.Add(channel);
            IsProgressVisible &= Channels.Count != App.ChannelsDatastore.GetAllChannels().Count;
        }
    }

    class ChannelActivityEvent
    {
        public string ChannelId;
        public YoutubeCall<YoutubeActivity> Result;

        public ChannelActivityEvent(string channelId, YoutubeCall<YoutubeActivity> results)
        {
            ChannelId = channelId;
            Result = results;
        }
    }
}

