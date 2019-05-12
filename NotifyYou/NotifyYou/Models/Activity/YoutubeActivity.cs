using System;
using NotifyYou.Models.Channel;
using SQLite;

namespace NotifyYou.Models.Activity
{
    public class YoutubeActivity
    {
        public string Id { get; set; }
        public string Kind { get; set; }
        public ActivitySnippet Snippet { get; set; }
        public ChannelContent ContentDetails { get; set; }

        public string VideoId
        {
            get
            {
                string videoId = "";
                if(ContentDetails != null && ContentDetails.Upload != null)
                    videoId = ContentDetails.Upload.VideoId;
                return videoId;
            }
        }

        public string ImageLink
        {
            get
            {
                string imageUrl = string.Empty;
                if (Snippet.Thumbnails != null)
                {
                    var thumbnails = Snippet.Thumbnails;
                    if (thumbnails.maxres != null && thumbnails.maxres.url != null && thumbnails.maxres.url.Length > 0)
                    {
                        imageUrl = thumbnails.maxres.url;
                    }
                    else if (thumbnails.high != null && thumbnails.high.url != null && thumbnails.high.url.Length > 0)
                    {
                        imageUrl = thumbnails.high.url;
                    }
                    else if (thumbnails.medium != null && thumbnails.medium.url != null && thumbnails.medium.url.Length > 0)
                    {
                        imageUrl = thumbnails.medium.url;
                    }
                    else if (thumbnails.standard != null && thumbnails.standard.url != null && thumbnails.standard.url.Length > 0)
                    {
                        imageUrl = thumbnails.standard.url;
                    }
                }
                return imageUrl;
            }
        }
    }
}
