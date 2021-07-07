using System.Collections.Generic;
using System.Linq;
using Timetable.Common.Helpers;
using Timetable.Core.Models;

namespace Timetable.Domain.Notification
{
    public class NotificationBuffer
    {
        record BufferItem(string Key, TimetableItemModel Item);

        private List<BufferItem>[] _buffer;
        private readonly object _locker = new object();

        public NotificationBuffer()
        {
            _buffer = new List<BufferItem>[1000];
        }

        public void InsertOrUpdate(TimetableItemModel item)
        {
            if (item is null) ThrowHelper.Throw(nameof(item), ThrowHelper.ExceptionType.ArgumentNull);

            var key = GetKey(item);
            var hash = GetHash(item);
            lock (_locker)
            {
                var existingItems = _buffer[hash];
                var existingItem = existingItems?.FirstOrDefault(s => s.Key == key);
                if (existingItem == null)
                {
                    existingItem = new BufferItem(key, item);
                    if (existingItems == null)
                    {
                        _buffer[hash] = new List<BufferItem>
                        {
                            existingItem
                        };
                    }
                    else
                    {
                        existingItems.Add(existingItem);
                    }
                }
                else
                {
                    existingItems.Remove(existingItem);
                    existingItems.Add(new BufferItem(key, item));
                }
            }
        }

        public List<TimetableItemModel> Flash()
        {
            List<TimetableItemModel> items = default;
            lock (_locker)
            {
                items = _buffer.Where(s => s != null).SelectMany(s => s).Select(s => s.Item).ToList();
                if (items.Any())
                {
                    _buffer = new List<BufferItem>[1000];
                }
            }
            return items;
        }

        public List<TimetableItemModel> GetItems()
        {
            return _buffer.Where(s => s != null).SelectMany(s => s).Select(s => s.Item).ToList();
        }

        private string GetKey(TimetableItemModel item)
        {
            return $"{item.Date}_{item.LessonNumber}_{item.Group.Name}";
        }

        public int GetHash(TimetableItemModel item)
        {
            int value = int.Parse($"{item.Date.Day}{(int)item.LessonNumber}");
            int groupNameValue = 0;
            foreach (var ch in item.Group.Name)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    groupNameValue += ch * 37;
                }
            }
            value += groupNameValue % (1000 - value);
            return value;
        }
    }
}
