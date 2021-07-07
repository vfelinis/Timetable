using Timetable.Core.Enums;

namespace Timetable.Core.Models
{
    public record TimetableItemModel(DateModel Date, GroupModel Group, LessonModel Lesson, LessonNumber LessonNumber);
}