using Microsoft.AspNetCore.Mvc;
using Timetable.Core.Interfaces.Notification;
using Timetable.Core.Models;
using Timetable.Domain.Helpers;
using Timetable.Domain.Notification;
using Timetable.Web.Client.Models;

namespace Timetable.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly NotificationBuffer _buffer;
        public TimetableController(NotificationBuffer buffer)
        {
            _buffer = buffer;

        }

        [HttpGet]
        public IActionResult GetTimetable()
        {
            var items = _buffer.GetItems();
            SortHelper.Quicksort(items, 0, items.Count - 1);
            return Ok(items);
        }

        [HttpPost]
        public IActionResult UpdateTimetable(TimetableItemFormModel model)
        {
            if (model != null)
            {
                var item = new TimetableItemModel(
                    new DateModel(model.Date.Day, model.Date.Month, model.Date.Year),
                    new GroupModel(model.GroupName),
                    new LessonModel(model.LessonName, model.Teacher, model.Room),
                    model.LessonNumber
                );
                _buffer.InsertOrUpdate(item);
            }
            return GetTimetable();
        }
    }
}
