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
                    try
                    {
                        var channel = Channels.First(c => c.ChannelId == channelId);
                        Channels.Remove(channel);
                        StoredChannel sc = App.ChannelsDatastore.Get(channel.ChannelId);
                        if (sc != null)
                            App.ChannelsDatastore.Delete(channel.ChannelId);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        public void CallForAllChannelActivities(bool force)
        {
            ChannelsViewModel vm = this;
            var channels = App.ChannelsDatastore.GetAllChannels();
            foreach (StoredChannel channel in channels)
            {
                if (!force)
                    Channels.Add(channel);
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
            channel.NewVideo = !channel.LastVideoId.Equals(latest.VideoId);
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

