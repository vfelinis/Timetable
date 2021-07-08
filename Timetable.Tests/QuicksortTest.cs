using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Core.Enums;
using Timetable.Core.Models;
using Timetable.Domain.Helpers;
using Timetable.Domain.Notification;
using Xunit;
using Xunit.Abstractions;

namespace Timetable.Tests
{
    public class QuicksortTest
    {
        [Fact]
        public void RightSortingTest()
        {
            var item1 = new TimetableItemModel(new DateModel(30, 12, 2020), new GroupModel(""), new LessonModel("", "", ""), LessonNumber.One);
            var item2 = new TimetableItemModel(new DateModel(30, 12, 2020), new GroupModel(""), new LessonModel("", "", ""), LessonNumber.Six);
            var item3 = new TimetableItemModel(new DateModel(1, 1, 2021), new GroupModel(""), new LessonModel("", "", ""), LessonNumber.One);
            var item4 = new TimetableItemModel(new DateModel(2, 1, 2021), new GroupModel(""), new LessonModel("", "", ""), LessonNumber.One);

            var actual = new List<TimetableItemModel>
            {
                item3, item2, item1, item4
            };
            SortHelper.Quicksort(actual, 0, 3);

            var expected = new List<TimetableItemModel>
            {
                item1, item2, item3, item4
            };

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
