using System;
using System.ComponentModel;
using NotifyYou.Models.Activity;
using NotifyYou.Models.Channel;
using SQLite;

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
            Link = "https://www.youtube.com/channel/" + ChannelId;
            BestImageUrl = grabUrl(channel.Snippet.Thumbnails.maxres);
            HighImageUrl = grabUrl(channel.Snippet.Thumbnails.high);
            MediumImageUrl = grabUrl(channel.Snippet.Thumbnails.medium);
            StandardImageUrl = grabUrl(channel.Snippet.Thumbnails.standard);
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ChannelId { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String grabUrl(ImageInfo info)
        {
            string ret = "";
            if(info != null)
                ret = info.url;
            return ret;
        }
    }
}
