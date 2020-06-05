using System;

namespace Notion.Services.Helper
{
    public static class DatetimeExtensions
    {
        public static DateTime ConvertTimeFromUtc(this DateTime dateTime)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), cstZone);
        }

        
    }
}