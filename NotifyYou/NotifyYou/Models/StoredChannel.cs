using System;
using System.ComponentModel;
using System.Windows.Input;
using NotifyYou.Models.Activity;
using NotifyYou.Models.Channel;
using NotifyYou.Models.Events;
using SQLite;
using Xamarin.Forms;

namespace NotifyYou.Models
{
    [Table("storedchannel")]
    public class StoredChannel : INotifyPropertyChanged
    {
        public StoredChannel()
        {
        }

        public StoredChannel(YoutubeChannel channel)
        {
            ChannelId = channel.ChannelId;
            Name = channel.ChannelTitle;
            Active = true;
            Link = "https://www.youtube.com/channel/" + ChannelId;
            BestImageUrl = GrabUrl(channel.Snippet.Thumbnails.maxres);
            HighImageUrl = GrabUrl(channel.Snippet.Thumbnails.high);
            MediumImageUrl = GrabUrl(channel.Snippet.Thumbnails.medium);
            StandardImageUrl = GrabUrl(channel.Snippet.Thumbnails.standard);
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ChannelId { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string BestImageUrl { get; set; }
        public string StandardImageUrl { get; set; }
        public string HighImageUrl { get; set; }
        public string MediumImageUrl { get; set; }
        public string LastVideoId { get; set; }
        public string LastVideoImageLink { get; set; }
        public string LastVideoTitle { get; set; }
        public string LastVideoTime { get; set; }
        [Ignore]
        public bool NewVideo { get; set; }
        public bool _searching;
        [Ignore]
        public bool Searching
        {
            get
            {
                return _searching;
            }
            set
            {
                _searching = value;
                OnPropertyChanged(nameof(Searching));
            }
        }

        [Ignore]
        public string ImageUrl
        {
            get
            {
                String url = LastVideoImageLink;
                if(url == null || (string.IsNullOrEmpty(url)))
                {
                    if(!string.IsNullOrEmpty(BestImageUrl))
                    {
                        url = BestImageUrl;
                    } else if(!string.IsNullOrEmpty(HighImageUrl))
                    {
                        url = HighImageUrl;
                    } else if (!string.IsNullOrEmpty(StandardImageUrl)) {
                        url = StandardImageUrl;
                    } else if(!string.IsNullOrEmpty(MediumImageUrl))
                    {
                        url = MediumImageUrl;
                    }
                }
                return url;
            }
        }

        [Ignore]
        public YoutubeActivity _activity{get; set;}
        [Ignore]
        public YoutubeActivity Activity { 
        get { return _activity; } 
        set { 
                _activity = value;
                OnPropertyChanged(nameof(Activity));
                OnPropertyChanged(nameof(NewVideo));
                OnPropertyChanged(nameof(LastVideoId));
                OnPropertyChanged(nameof(LastVideoImageLink));
                OnPropertyChanged(nameof(LastVideoTitle));
                OnPropertyChanged(nameof(LastVideoTime));
                OnPropertyChanged(nameof(ImageUrl));
            } 
        }
        [Ignore]
        public ICommand ViewUrlCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    string url = "";
                    if (!string.IsNullOrEmpty(LastVideoId))
                    {
                        url = "http://youtube.com/watch?v=" + LastVideoId;
                    }
                    else
                    {
                        url = "http://youtube.com/channel/" + ChannelId;
                    }

                    Device.OpenUri(new Uri(url));
                });
            }
        }
        [Ignore]
        public ICommand ViewSettingCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    MessagingCenter.Send(this, "View Settings", new ChannelViewSettingsEvent(ChannelId));
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String GrabUrl(ImageInfo info)
        {
            string ret = "";
            if(info != null)
                ret = info.url;
            return ret;
        }
    }
}
