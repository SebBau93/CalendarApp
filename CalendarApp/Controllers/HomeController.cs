using CalendarApp.Application.Interfaces;
using CalendarApp.Domain.Constants;
using CalendarApp.Domain.Entities;
using CalendarApp.Utilities;
using System;
using System.Collections.Generic;

namespace CalendarApp.Controllers
{
    public class HomeController
    {
        private readonly IReminderService _reminderService;
        private readonly IMenuActionService _menuActionService;

        public HomeController(IReminderService reminderService, IMenuActionService menuActionService)
        {
            _reminderService = reminderService;
            _menuActionService = menuActionService;
        }

        public void Index()
        {
            int todayRemindersCount = _reminderService.CountAllRemindersForDate(DateTime.Today);
            IEnumerable<MenuAction> menuActions = _menuActionService.ReturnMenuActionsByType(MenuActionTypes.Main);

            Console.WriteLine("Welcome in CalendarApp!");
            Console.WriteLine();

            Console.WriteLine($"Today is {DateTime.Today.Day}/{DateTime.Today.Month}/{DateTime.Today.Year} - {EnumUtilities.GetTodayDayOfWeekName()}");
            Console.WriteLine();
            Console.WriteLine($"You've {todayRemindersCount} reminder/s today!");
            Console.WriteLine();
            Console.WriteLine("Go to:");

            foreach (MenuAction menuAction in menuActions)
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
        }
    }
}
