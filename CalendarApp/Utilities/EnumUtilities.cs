using CalendarApp.Domain.Enums;
using System;

namespace CalendarApp.Utilities
{
    public static class EnumUtilities
    {
        public static string GetTodayDayOfWeekName()
        {
            try
            {
                return Enum.GetName(typeof(WeekDays), DateTime.Now.DayOfWeek);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        public static void DisplayAllRemindersPeriodicityOptions()
        {
            Console.WriteLine(Environment.NewLine);

            int count = 1;
            
            foreach (RemindersPeriodicity enumOption in Enum.GetValues(typeof(RemindersPeriodicity)))
            {
                Console.WriteLine($"{count}. {enumOption}");
                count++;
            }
        }
    }
}
