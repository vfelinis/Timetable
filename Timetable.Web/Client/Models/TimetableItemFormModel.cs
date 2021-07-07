using System;
using System.ComponentModel.DataAnnotations;
using Timetable.Core.Enums;

namespace Timetable.Web.Client.Models
{
    public class TimetableItemFormModel
    {
        public DateTime Date { get; set; }
        [Required]
        public string GroupName { get; set; }
        public LessonNumber LessonNumber { get; set; } = LessonNumber.One;
        [Required]
        public string LessonName { get; set; }
        [Required]
        public string Teacher { get; set; }
        [Required]
        public string Room { get; set; }
    }
}
