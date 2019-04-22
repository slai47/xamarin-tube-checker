using System;
using SQLite;

namespace NotifyYou.Models.Activity
{
    public class YoutubeActivity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Kind { get; set; }
        public YoutubeSnippet Snippet { get; set; }
        
    }
}
