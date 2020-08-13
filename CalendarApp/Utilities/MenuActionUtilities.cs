using CalendarApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CalendarApp.Utilities
{
    public static class MenuActionUtilities
    {
        public static void ShowAvailableActionsAndReturnSelectedAction(IEnumerable<MenuAction> menuActions)
        {
            Console.WriteLine("Choose Action:");

            foreach (MenuAction menuAction in menuActions)
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");

            byte.TryParse(Console.ReadKey().KeyChar.ToString(), out TemporaryVariables.selectedAction);
        }
    }
}
