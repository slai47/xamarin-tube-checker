using System;
using System.ComponentModel;
using NotifyYou.Models.Activity;
using NotifyYou.Models.Channel;
using SQLite;

namespace NotifyYou.Models
{
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
            ImageUri = channel.Snippet.Thumbnails;
        }

        [PrimaryKey]
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public Thumbnails ImageUri { get; set; }
        public string LastVideoId { get; set; }
        public string LastVideoImageLink { get; set; }

        [Ignore]
        public YoutubeActivity _activity{get; set;}
        [Ignore]
        public YoutubeActivity Activity { 
        get { return _activity; } 
        set { 
            _activity = value;
            OnPropertyChanged(nameof(Activity));
        } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
