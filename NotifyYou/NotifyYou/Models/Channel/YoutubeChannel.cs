using System;
using System.ComponentModel;

namespace NotifyYou.Models.Channel
{
    public class YoutubeChannel : INotifyPropertyChanged
    {
        public ChannelInfo Id { get; set; }
        public ChannelSnippet Snippet { get; set; }

        #region Quick code
        public string ChannelTitle
        {
            get
            {
                return Snippet.ChannelTitle;
            }
        }
        public string ChannelId
        {
            get
            {
                return Id.ChannelId;
            }
        }
        public bool _isActive;
        public bool IsActive { get
            {
                return _isActive;
            } set
            {
                _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
         }

        public string ImageLink
        {
            get
            {
                string imageUrl = string.Empty;
                if(Snippet.Thumbnails != null)
                {
                    var thumbnails = Snippet.Thumbnails;
                    if(thumbnails.maxres != null && thumbnails.maxres.url != null && thumbnails.maxres.url.Length > 0)
                    {
                        imageUrl = thumbnails.maxres.url;
                    } else if (thumbnails.high != null && thumbnails.high.url != null && thumbnails.high.url.Length > 0)
                    {
                        imageUrl = thumbnails.high.url;
                    } else if (thumbnails.medium != null && thumbnails.medium.url != null && thumbnails.medium.url.Length > 0)
                    {
                        imageUrl = thumbnails.medium.url;
                    } else if (thumbnails.standard != null && thumbnails.standard.url != null && thumbnails.standard.url.Length > 0)
                    {
                        imageUrl = thumbnails.standard.url;
                    }
                }
                return imageUrl;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
#endregion