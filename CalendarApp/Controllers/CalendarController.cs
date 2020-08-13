using CalendarApp.Application.Interfaces;
using CalendarApp.Domain.Constants;
using CalendarApp.Domain.Entities;
using CalendarApp.Utilities;
using System;
using System.Collections.Generic;

namespace CalendarApp.Controllers
{
    public class CalendarController
    {
        private readonly ICalendarService _calendarService;
        private readonly IReminderService _reminderService;
        private readonly IMenuActionService _menuActionService;

        public CalendarController(
            IReminderService reminderService, 
            ICalendarService calendarService,
            IMenuActionService menuActionService
            )
        {
            _calendarService = calendarService;
            _reminderService = reminderService;
            _menuActionService = menuActionService;
        }

        public void Index()
        {
            while (true)
            {
                if (TemporaryVariables.selectedAction == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Today is {DateTime.Today.Day}/{DateTime.Today.Month}/{DateTime.Today.Year} - {EnumUtilities.GetTodayDayOfWeekName()}");
                    Console.WriteLine();

                    IEnumerable<MenuAction> menuActions = _menuActionService.ReturnMenuActionsByType(MenuActionTypes.Calendar);

                    MenuActionUtilities.ShowAvailableActionsAndReturnSelectedAction(menuActions);
                }

                switch (TemporaryVariables.selectedAction)
                {
                    case 1:
                        Console.Clear();
                        GetCalendarForMonth();
                        break;
                    case 2:
                        TemporaryVariables.ClearSelectedActionValue();
                        return;
                    case 100:
                        return;
                    default:
                        ConsoleUtilities.ShowMessage("Invalid year value", "Returning to Calendar Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                        break;
                }
            }
        }

        public void GetCalendarForMonth()
        {
            Console.WriteLine("Enter year and month:");
            Console.WriteLine("Year:");

            bool isYear = Int32.TryParse(Console.ReadLine(), out int year);

            if (!isYear && year < 0)
            {
                ConsoleUtilities.ShowMessage("Invalid year value", "Returning to Calendar Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            Console.WriteLine("Month:");

            bool isMonth = Int32.TryParse(Console.ReadLine(), out int month);

            if (!isMonth && month < 0)
            {
                ConsoleUtilities.ShowMessage("Invalid month value", "Returning to Calendar Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            int[,] calendarArray = _calendarService.GetCalendarArrayForMonth(year, month);

            if (calendarArray == null)
            {
                ConsoleUtilities.ShowMessage("Invalid date", "Returning to Calendar Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            IEnumerable<Reminder> reminders = _reminderService.GetAllRemindersForMonth(year, month);

            CalendarUtilities.DisplayCalendarWithRemindersForMonth(calendarArray, reminders, year, month);

            IEnumerable<MenuAction> menuActions = _menuActionService.ReturnMenuActionsByType(MenuActionTypes.CalendarMonth);

            MenuActionUtilities.ShowAvailableActionsAndReturnSelectedAction(menuActions);

            switch (TemporaryVariables.selectedAction)
            {
                case 1:
                    TemporaryVariables.ClearSelectedActionValue();
                    return;
                case 2:
                    TemporaryVariables.selectedAction = 100;
                    TemporaryVariables.year = year;
                    TemporaryVariables.month = month;
                    return;
                default:
                    TemporaryVariables.ClearSelectedActionValue();
                    ConsoleUtilities.ShowMessage("Invalid value", "Returning to Calendar Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                    return;
            }
        }
    }
}
