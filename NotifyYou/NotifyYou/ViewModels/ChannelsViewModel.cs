using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotifyYou.API;
using NotifyYou.Models;
using NotifyYou.Models.Activity;
using Xamarin.Forms;
using System.Linq;

namespace NotifyYou.ViewModels
{
    public class ChannelsViewModel : BaseViewModel
    {
        public const string EVENT_ACTIVITY = "Activity";

        public ObservableCollection<StoredChannel> Channels;

        public ChannelsViewModel()
        {
            InitChannels();
            CallForActivity(false);
            SetupSubscribes();
        }

        private void SetupSubscribes()
        {
            MessagingCenter.Subscribe<ChannelActivityEvent>(this, EVENT_ACTIVITY, (activity) =>
            {
                StoredChannel channel = Channels.First(c => c.Id == activity.ChannelId);
                YoutubeActivity latest = activity.Result.items.OrderBy(act => act.Snippet.publishedAt).First();
                channel.LastVideoId = latest.Id;
                channel.Activity = latest;
                App.ChannelsDatastore.AddUpdate(channel);
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
                        MessagingCenter.Send(vm, EVENT_ACTIVITY, new ChannelActivityEvent(channel.ChannelId, result.Result));
                    };
            }
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

