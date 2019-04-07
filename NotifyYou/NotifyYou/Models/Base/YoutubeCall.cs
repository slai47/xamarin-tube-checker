using System;
using System.Collections.Generic;
using System.Net;

namespace NotifyYou.Models
{
    public class YoutubeCall<T>
    {
        public string kind { get; set; }
        public string newPageToken { get; set; }
        public PageInfo pageInfo { get; set; }
        public List<T> items { get; set; }
        public HttpStatusCode code { get; set; }

    }
}
