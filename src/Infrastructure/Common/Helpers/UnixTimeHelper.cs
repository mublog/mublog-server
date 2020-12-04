using System;

namespace Mublog.Server.Infrastructure.Common.Helpers
{
    public static class UnixTimeHelper
    {
        public static long GetTimeStamp(DateTime dateTime)
        {
            var dateTimeOffset = new DateTimeOffset(dateTime);
            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}