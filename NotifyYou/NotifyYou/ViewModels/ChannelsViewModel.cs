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

        public ObservableCollection<StoredChannel> Channels;

        public ChannelsViewModel()
        {
            InitChannels();
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

        private void CallForActivity(bool force)
        {
            IYoutube api = new YoutubeApi();
            ChannelsViewModel vm = this;
            foreach(StoredChannel channel in Channels)
            {
                if(channel.Activity == null || force)
                    Task.Run(() => {
                        Task<YoutubeCall<YoutubeActivity>> result = Task.Run(() => api.GetChannelActivity(channel.ChannelId));
                        var webResult = result.Result;
                        onActivityReceive(new ChannelActivityEvent(channel.ChannelId, webResult));
                    });
            }
        }

        private void onActivityReceive(ChannelActivityEvent activityEvent)
        {
            StoredChannel channel = Channels.First(c => c.ChannelId == activityEvent.ChannelId);
            YoutubeActivity latest = activityEvent.Result.items.OrderBy(act => act.Snippet.PublishedAt).First();
            channel.LastVideoId = latest.Id;
            channel.LastVideoImageLink = latest.ImageLink;
            channel.Activity = latest;
            App.ChannelsDatastore.AddUpdate(channel);
            int index = Channels.IndexOf(channel);
            Channels[index] = channel;
        }

        private void InitChannels()
        {
            Channels = new ObservableCollection<StoredChannel>(App.ChannelsDatastore.GetAllChannels());
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

