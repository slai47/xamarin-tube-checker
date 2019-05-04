using System;
namespace NotifyYou.Models.Events
{
    public class ChannelsAddRemoveEvent
    {

        public bool Add { get; set; }
        public string Id { get; set; }

        public ChannelsAddRemoveEvent(bool added, string id)
        {
            Add = added;
            Id = id;
        }
    }
}
