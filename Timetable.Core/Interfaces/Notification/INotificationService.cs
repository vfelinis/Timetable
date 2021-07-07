using System.Collections.Generic;

namespace Timetable.Core.Interfaces.Notification
{
    public interface INotificationService
    {
        Dictionary<string, string> Send();
    }
}
