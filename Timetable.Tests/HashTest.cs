using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Core.Enums;
using Timetable.Core.Models;
using Timetable.Domain.Notification;
using Xunit;
using Xunit.Abstractions;

namespace Timetable.Tests
{
    public class HashTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public HashTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CollisionFullTest()
        {
            var days = Enumerable.Range(1, 31);
            var lessons = Enum.GetValues<LessonNumber>();
            var groupPrefixes = new List<string> { "л", "ох", "ч", "щ", "ас" };
            var groupNumbers = Enumerable.Range(1, 99);
            var groupForms = new List<string> { "", "б", "я", "яб", "с" };
            Analize(days, lessons, groupPrefixes, groupNumbers, groupForms);
        }

        [Fact]
        public void CollisionSimpleTest()
        {
            var random = new Random();
            var rnd1 = random.Next(1, 32);
            var days = Enumerable.Range(1, 31).Where(s => s == rnd1);
            var lessons = Enum.GetValues<LessonNumber>();
            var groupPrefixes = new List<string> { "л", "ох", "ч", "щ", "ас" };
            var rnd2 = random.Next(1, 100);
            var groupNumbers = Enumerable.Range(1, 99).Where(s => s == rnd2);
            var groupForms = new List<string> { "", "б", "я", "яб", "с" };
            Analize(days, lessons, groupPrefixes, groupNumbers, groupForms);
        }

        private void Analize(IEnumerable<int> days, LessonNumber[] lessons, IEnumerable<string> groupPrefixes,
            IEnumerable<int> groupNumbers, IEnumerable<string> groupForms)
        {
            var buffer = new NotificationBuffer();
            var hashes = new List<int>();
            var items = new List<TimetableItemModel>();
            foreach (var day in days)
            {
                foreach (var lesson in lessons)
                {
                    foreach (var groupPrefix in groupPrefixes)
                    {
                        foreach (var groupNumber in groupNumbers)
                        {
                            foreach (var groupForm in groupForms)
                            {
                                items.Add(
                                    new TimetableItemModel(
                                        new DateModel(day, 1, 1),
                                        new GroupModel($"{groupPrefix}-{groupNumber}{groupForm}"),
                                        new LessonModel("", "", ""),
                                        lesson
                                    )
                                );
                            }
                        }
                    }
                }
            }

            var upTo100Count = 0;
            var upTo200Count = 0;
            var upTo300Count = 0;
            var upTo400Count = 0;
            var upTo500Count = 0;
            var upTo600Count = 0;
            var upTo700Count = 0;
            var upTo800Count = 0;
            var upTo900Count = 0;
            var upTo1000Count = 0;
            foreach (var item in items)
            {
                var hash = buffer.GetHash(item);
                switch (hash)
                {
                    case < 100:
                        upTo100Count++;
                        break;
                    case < 200:
                        upTo200Count++;
                        break;
                    case < 300:
                        upTo300Count++;
                        break;
                    case < 400:
                        upTo400Count++;
                        break;
                    case < 500:
                        upTo500Count++;
                        break;
                    case < 600:
                        upTo600Count++;
                        break;
                    case < 700:
                        upTo700Count++;
                        break;
                    case < 800:
                        upTo800Count++;
                        break;
                    case < 900:
                        upTo900Count++;
                        break;
                    case < 1000:
                        upTo1000Count++;
                        break;
                }
            }

            _testOutputHelper.WriteLine($"The number of items: {items.Count}");
            _testOutputHelper.WriteLine($"The number of upTo100: {upTo100Count}");
            _testOutputHelper.WriteLine($"The number of upTo200: {upTo200Count}");
            _testOutputHelper.WriteLine($"The number of upTo300: {upTo300Count}");
            _testOutputHelper.WriteLine($"The number of upTo400: {upTo400Count}");
            _testOutputHelper.WriteLine($"The number of upTo500: {upTo500Count}");
            _testOutputHelper.WriteLine($"The number of upTo600: {upTo600Count}");
            _testOutputHelper.WriteLine($"The number of upTo700: {upTo700Count}");
            _testOutputHelper.WriteLine($"The number of upTo800: {upTo800Count}");
            _testOutputHelper.WriteLine($"The number of upTo900: {upTo900Count}");
            _testOutputHelper.WriteLine($"The number of upTo1000: {upTo1000Count}");
        }
    }
}
