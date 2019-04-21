using System;
using SQLite;

namespace NotifyYou.Models
{
    public class Thumbnails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public ImageInfo medium { get; set;}
        public ImageInfo high { get; set; }
        public ImageInfo standard { get; set; }
        public ImageInfo maxres { get; set; }
    }
}
