using System;

namespace Mublog.Server.Infrastructure.Common.Helpers
{
    public static class UnixTimeHelper
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
            => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
}