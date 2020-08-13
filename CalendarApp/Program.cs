using CalendarApp.Application.Interfaces;
using CalendarApp.Application.Services;
using CalendarApp.Controllers;
using System;
using System.Threading;

namespace CalendarApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenuActionService menuActionService = new MenuActionService();
            IReminderService reminderService = new ReminderService();
            ICalendarService calendarService = new CalendarService();
            HomeController homeController = new HomeController(reminderService, menuActionService);
            ReminderController reminderController = new ReminderController(reminderService, menuActionService);
            CalendarController calendarController = new CalendarController(reminderService, calendarService, menuActionService);

            while (true)
            {
                if (TemporaryVariables.selectedAction == 0)
                {
                    Console.Clear();
                    homeController.Index();

                    byte.TryParse(Console.ReadKey().KeyChar.ToString(), out TemporaryVariables.selectedAction);
                }

                switch (TemporaryVariables.selectedAction)
                {
                    case 1:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        calendarController.Index();
                        break;
                    case 2:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        reminderController.Index();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Closing application...");
                        Thread.Sleep(3000);
                        return;
                    case 100:
                        Console.Clear();
                        reminderController.Index();
                        break;
                    default:
                        Console.WriteLine("Invalid value.");
                        break;
                }
            }     
        }
    }
}
