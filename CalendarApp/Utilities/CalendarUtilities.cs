using CalendarApp.Domain.Entities;
using CalendarApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CalendarApp.Utilities
{
    public static class CalendarUtilities
    {
        public static void DisplayCalendarWithRemindersForMonth(int[,] calendarArray, IEnumerable<Reminder> reminders, int year, int month)
        {
            Console.Clear();
            Console.WriteLine();

            string twoWhitespace = "  ";
            string threeWhitespace = "   ";
            string fourWhitespace = "    ";
            string fiveWhitespace = "     ";

            string monthName = new DateTime(year, month, 1).ToString("MMMM", CultureInfo.InvariantCulture);

            Console.WriteLine(twoWhitespace + monthName + " " + year);
            Console.WriteLine();

            Console.Write(twoWhitespace);
            foreach (var day in Enum.GetValues(typeof(WeekDays)))
                Console.Write(day.ToString().Substring(0, 3) + twoWhitespace);

            Console.WriteLine(Environment.NewLine);

            for (var i = 0; i < calendarArray.GetLength(0); i++)
            {
                Console.Write(threeWhitespace);

                for (var j = 0; j < calendarArray.GetLength(1); j++)
                {
                    if (calendarArray[i, j] != 0 && calendarArray[i, j] >= 10)
                    {
                        if (reminders.Any(r => r.ReminderDate.Day == calendarArray[i, j]))
                            Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write(calendarArray[i, j] + threeWhitespace);
                    }
                    else if (calendarArray[i, j] != 0 && calendarArray[i, j] < 10)
                    {
                        if (reminders.Any(r => r.ReminderDate.Day == calendarArray[i, j]))
                            Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write(calendarArray[i, j] + fourWhitespace);
                    }
                    else
                        Console.Write(fiveWhitespace);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine($"You've {reminders.Count()} reminder/s in {monthName}");
            Console.WriteLine();
            Console.Write($"The days you have reminders are marked in ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();
        }
    }
}
