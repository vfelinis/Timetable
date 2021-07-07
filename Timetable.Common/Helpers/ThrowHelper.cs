using System;

namespace Timetable.Common.Helpers
{
    public static class ThrowHelper
    {
        public enum ExceptionType
        {
            Base = 1,
            ArgumentNull = 2,
            Argument = 3
        }
        public static Exception Throw(string message, ExceptionType type = ExceptionType.Base) => type switch
        {
            ExceptionType.Base => throw new Exception(message),
            ExceptionType.ArgumentNull => throw new ArgumentNullException(message),
            ExceptionType.Argument => throw new ArgumentException(message),
            _ => throw new Exception(message)
        };
    }
}