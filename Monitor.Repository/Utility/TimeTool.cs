using System;

namespace Monitor.Service.Utility
{
    public static class TimeTool
    {
        public static DateTime ToDateTime(this DateTime dateTime,TimeLevel timeLevel=TimeLevel.Minute)
        {
            int year = dateTime.Year,month=dateTime.Month,
                day=dateTime.Day,hour=dateTime.Hour,minute=dateTime.Minute;
            
            switch (timeLevel)
            {
               case TimeLevel.Minute:
                    return new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
                    
                case TimeLevel.Hour:
                    return new DateTime(year, month, day, hour, 0, 0, DateTimeKind.Utc);
                  
                case TimeLevel.Day:
                    return new DateTime(year,month,day,0,0,0,DateTimeKind.Utc);
            }
            return dateTime;
        }
    }

    public enum TimeLevel
    {
        Minute,Hour,Day
    }
}