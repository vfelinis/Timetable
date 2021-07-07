using System;
using Timetable.Common.Helpers;

namespace Timetable.Core.Models
{
    public record DateModel
    {
        public DateModel(int day, int month, int year)
        {
            Validate(day, month, year);
            Day = day;
            Month = month;
            Year = year;
        }
        public int Day { get; init; }
        public int Month { get; init; }
        public int Year { get; init; }

        private void Validate(int day, int month, int year)
        {
            try
            {
                new DateTime(year, month, day);
            }
            catch
            {
                ThrowHelper.Throw("Incorrect date", ThrowHelper.ExceptionType.Argument);
            }
        }

        public override string ToString()
        {
            return $"{Day.ToString("D2")}.{Month.ToString("D2")}.{Year}";
        }
    }
}
