using System;
using System.Collections.Generic;
using Timetable.Core.Models;

namespace Timetable.Domain.Helpers
{
    public static class SortHelper
    {
        public static void Quicksort(List<TimetableItemModel> items, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = Partition(items, start, end);
            Quicksort(items, start, pivot - 1);
            Quicksort(items, pivot + 1, end);
        }

        private static int Partition(List<TimetableItemModel> items, int start, int end)
        {
            TimetableItemModel temp;
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                if (IsLess(items[i], items[end]))
                {
                    temp = items[marker];
                    items[marker] = items[i];
                    items[i] = temp;
                    marker += 1;
                }
            }
            temp = items[marker];
            items[marker] = items[end];
            items[end] = temp;
            return marker;
        }

        private static bool IsLess(TimetableItemModel left, TimetableItemModel right)
        {
            var leftDate = new DateTime(left.Date.Year, left.Date.Month, left.Date.Day);
            var rightDate = new DateTime(right.Date.Year, right.Date.Month, right.Date.Day);
            return leftDate < rightDate || (leftDate == rightDate && (int)left.LessonNumber < (int)right.LessonNumber);
        }
    }
}
