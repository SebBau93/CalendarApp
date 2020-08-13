using System;
using System.Threading;

namespace CalendarApp.Utilities
{
    public static class ConsoleUtilities
    {
        public static void ShowMessage(string validationMessage, string menuMessage, int sleepMiliseconds)
        {
            Console.WriteLine(validationMessage);
            Console.WriteLine();
            Console.WriteLine(menuMessage);
            Thread.Sleep(sleepMiliseconds);
        }
    }
}
