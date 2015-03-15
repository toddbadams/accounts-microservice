using System;

namespace tba.Core.Utilities
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}