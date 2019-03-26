using System;
namespace NotifyYou.Models
{
    public class Channel
    {
        public string name { get; set; }
        public string link { get; set; }

        public Channel(string name)
        {
            this.name = name;
        }
    }
}
