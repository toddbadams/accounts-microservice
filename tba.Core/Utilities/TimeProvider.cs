using System;

namespace tba.Core.Utilities
{
    /// <summary>
    /// 
    /// http://stackoverflow.com/questions/2425721/unit-testing-datetime-now
    /// http://stackoverflow.com/questions/2840470/how-can-this-ambient-context-become-null
    /// 
    /// </summary>
    public abstract class TimeProvider : ITimeProvider
    {
        private static TimeProvider _current = DefaultTimeProvider.Instance;

        public static TimeProvider Current
        {
            get { return _current; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _current = value;
            }
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            _current = DefaultTimeProvider.Instance;
        }
    }
}
