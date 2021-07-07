using System.Collections.Generic;
using System.Linq;
using Timetable.Core.Interfaces.Notification;
using Timetable.Core.Models;
using Timetable.Domain.Helpers;

namespace Timetable.Domain.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationBuffer _buffer;

        public NotificationService(NotificationBuffer buffer)
        {
            _buffer = buffer;
        }

        public Dictionary<string, string> Send()
        {
            var items = _buffer.Flash();

            if (items?.Count > 0)
            {
                return GroupAndSort(items);
            }
            else
            {
                return new Dictionary<string, string>();
            }
        }

        private Dictionary<string, string> GroupAndSort(List<TimetableItemModel> items)
        {
            SortHelper.Quicksort(items, 0, items.Count - 1);
            var result = new Dictionary<string, string>();
            foreach(var group in items.GroupBy(s => s.Group.Name.ToUpper()))
            {
                result.Add(group.Key, string.Join(", ", group.Select(s => s.Date.ToString())));
            }
            return result;
        }
    }
}
