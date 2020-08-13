using CalendarApp.Application.Interfaces;
using System;
using System.Globalization;

namespace CalendarApp.Application.Services
{
    public class CalendarService : ICalendarService
    {
        public int[,] GetCalendarArrayForMonth(int year, int month)
        {
            try
            {
                bool isDateTime = DateTime.TryParse($"1/{month}/{year}", out DateTime result);

                if (isDateTime)
                {
                    Calendar calendar = CultureInfo.CurrentCulture.Calendar;

                    int firstDayInMonthDayToInt = (int)calendar.GetDayOfWeek(new DateTime(year, month, 1)) != 0 ? (int)calendar.GetDayOfWeek(new DateTime(year, month, 1)) : 7;
                    int daysInCurrentMonth = calendar.GetDaysInMonth(year, month);

                    int weeksInCurrentMonth = (int)Math.Ceiling((firstDayInMonthDayToInt + daysInCurrentMonth) / 7.0);

                    int[,] arr = new int[weeksInCurrentMonth, 7];

                    int currentDay = 1;
                    bool fill = false;

                    for (int t = 0; t < weeksInCurrentMonth; t++)
                    {
                        for (int f = 0; f < 7 && currentDay <= daysInCurrentMonth; f++)
                        {
                            if (fill)
                            {
                                arr[t, f] = currentDay;
                                currentDay++;
                                continue;
                            }

                            if (t == 0 && f == firstDayInMonthDayToInt - 1)
                            {
                                arr[t, f] = currentDay;
                                currentDay++;
                                fill = true;
                            }

                        }
                    }

                    return arr;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
