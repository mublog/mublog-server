using System;

namespace Mublog.Server.Infrastructure.Common.Helpers
{
    public static class UnixTimeHelper
    {
        public static long ToUnixTimeStamp(this DateTime dateTime)
            => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
}