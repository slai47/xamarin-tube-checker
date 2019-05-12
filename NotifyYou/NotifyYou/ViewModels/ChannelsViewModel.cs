using System;
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

        public ChannelsViewModel()
        {
            SetupSubscribes();
            CallForAllChannelActivities(false);
        }

        private void SetupSubscribes()
        {
            MessagingCenter.Subscribe<ChannelsAddRemoveEvent>(this, EVENT_ADDREMOVE, (addRemoveEvent) =>
            {
                string channelId = addRemoveEvent.Id;
                if (addRemoveEvent.Add)
                {
                    StoredChannel channel = App.ChannelsDatastore.Get(channelId);
                    Channels.Add(channel);
                    GetChannelActivity(channel, false);
                } else
                {
                    var channel = Channels.First(c => c.ChannelId == channelId);
                    if(channel != null)
                        Channels.Remove(channel);
                    if (App.ChannelsDatastore.Exists(channel.ChannelId))
                        App.ChannelsDatastore.Delete(channelId);
                }
            });
        }

        public void CallForAllChannelActivities(bool force)
        {
            if(Channels.Count == 0)
            {
                var channels = App.ChannelsDatastore.GetAllChannels();
                foreach(var channel in channels)
                {
                    Channels.Add(channel);
                }
            }

            foreach (StoredChannel channel in Channels)
            {
                GetChannelActivity(channel, force);
            }
        }

        private void GetChannelActivity(StoredChannel channel, bool force)
        {
            IYoutube api = new YoutubeApi();
            channel.Searching = true;
            if (channel.Activity == null || force)
                Task.Run(() => {
                    Task<YoutubeCall<YoutubeActivity>> result = Task.Run(() => api.GetChannelActivity(channel.ChannelId));
                    var webResult = result.Result;
                    OnActivityReceive(new ChannelActivityEvent(channel.ChannelId, webResult));
                });
        }

        private void OnActivityReceive(ChannelActivityEvent activityEvent)
        {
            StoredChannel channel = Channels.First(c => c.ChannelId == activityEvent.ChannelId);
            YoutubeActivity latest = activityEvent.Result.items.OrderByDescending(act => act.Snippet.PublishedAt).First();
            channel.NewVideo = channel.LastVideoId == null || !channel.LastVideoId.Equals(latest.VideoId);
            channel.LastVideoId = latest.VideoId;
            channel.LastVideoImageLink = latest.ImageLink;
            channel.LastVideoTitle = latest.Snippet.Title;
            channel.LastVideoTime = latest.Snippet.PublishedAt.ToShortDateString() + " " + latest.Snippet.PublishedAt.ToShortTimeString();
            channel.Activity = latest;
            channel.Searching = false;
            App.ChannelsDatastore.AddUpdate(channel);
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

