using Player.Models;
using Player.Services;
using System.Collections.ObjectModel;

namespace Player.ViewModels
{
    public class EventTagViewModel
    {
        public ObservableCollection<EventTag> EventTags { get; set; }

        public void GetEventTags()
        {
            EventTagService _eventTagService = new EventTagService();

            EventTags = _eventTagService.GetEvents();
        }
    }
}
