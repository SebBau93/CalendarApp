using CalendarApp.Application.DTO;
using CalendarApp.Application.Interfaces;
using CalendarApp.Application.Services;
using CalendarApp.Domain.Constants;
using CalendarApp.Domain.Entities;
using CalendarApp.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarApp.Controllers
{
    public class ReminderController
    {
        private readonly IReminderService _reminderService;
        private readonly IMenuActionService _menuActionService;
        private readonly IFileService<Reminder> _fileService;

        public ReminderController(IReminderService reminderService, IMenuActionService menuActionService)
        {
            _reminderService = reminderService;
            _menuActionService = menuActionService;
            _fileService = new FileService<Reminder>();
        }

        public void Index()
        {
            while (true)
            {
                Console.Clear();

                if (TemporaryVariables.selectedAction == 0)
                {
                    IEnumerable<MenuAction> menuActions = _menuActionService.ReturnMenuActionsByType(MenuActionTypes.Reminder);

                    MenuActionUtilities.ShowAvailableActionsAndReturnSelectedAction(menuActions);
                }

                switch (TemporaryVariables.selectedAction)
                {
                    case 1:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        GetAllForSelectedMonth();
                        break;
                    case 2:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        GetAllForSelectedDay();
                        break;
                    case 3:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        GetDetails();
                        break;
                    case 4:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        Add();
                        break;
                    case 5:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        Delete();
                        break;
                    case 6:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        AddRemindersFromFile();
                        break;
                    case 7:
                        TemporaryVariables.ClearSelectedActionValue();
                        return;
                    case 100:
                        Console.Clear();
                        TemporaryVariables.ClearSelectedActionValue();
                        GetAllForSelectedMonth();
                        break;
                    default:
                        Console.Clear();
                        ConsoleUtilities.ShowMessage("Invalid value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                        break;
                }
            }
        }

        public void GetAllForSelectedMonth()
        {
            IEnumerable<Reminder> reminders;
            int year;
            int month;

            if (TemporaryVariables.year == 0 && TemporaryVariables.month == 0)
            {
                Console.WriteLine("Enter year and month:");
                Console.WriteLine("Year:");

                bool isYear = Int32.TryParse(Console.ReadLine(), out year);

                if (!isYear || year < 0)
                {
                    ConsoleUtilities.ShowMessage("Invalid year value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                    return;
                }

                Console.WriteLine("Month:");

                bool isMonth = Int32.TryParse(Console.ReadLine(), out month);

                if (!isMonth || month < 0)
                {
                    ConsoleUtilities.ShowMessage("Invalid month value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                    return;
                }

                 reminders = _reminderService.GetAllRemindersForMonth(year, month);
            }
            else
            {
                reminders = _reminderService.GetAllRemindersForMonth(TemporaryVariables.year, TemporaryVariables.month);
                year = TemporaryVariables.year;
                month = TemporaryVariables.month;
                TemporaryVariables.ClearYearAndMonthValue();
            }
            

            Console.Clear();

            Console.WriteLine($"Reminders for {new DateTime(year, month, 1).ToString("MMMM", CultureInfo.InvariantCulture)} {year}");
            Console.WriteLine();

            foreach (Reminder reminder in reminders)
            {
                Console.WriteLine($"Id: {reminder.Id}, Title: {reminder.Title}, Reminder Date: {reminder.ReminderDate:yyyy-MM-dd}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to Reminders menu.");

            Console.ReadKey();

            return;
        }

        public void GetAllForSelectedDay()
        {
            Console.WriteLine("Enter year and month:");
            Console.WriteLine("Year:");

            bool isYear = Int32.TryParse(Console.ReadLine(), out int year);

            if (!isYear || year < 0)
            {
                ConsoleUtilities.ShowMessage("Invalid year value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            Console.WriteLine("Month:");

            bool isMonth = Int32.TryParse(Console.ReadLine(), out int month);

            if (!isMonth || month < 0 || month > 12)
            {
                ConsoleUtilities.ShowMessage("Invalid month value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            Console.WriteLine("Day:");

            bool isDay = Int32.TryParse(Console.ReadLine(), out int day);

            if (!isDay || day < 0 || day > 31)
            {
                ConsoleUtilities.ShowMessage("Invalid day value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            IEnumerable<Reminder> reminders = _reminderService.GetAllRemindersForDay(year, month, day);

            Console.Clear();

            Console.WriteLine($"Reminders for {day} {new DateTime(year, month, day).ToString("MMMM", CultureInfo.InvariantCulture)} {year}");
            Console.WriteLine();

            foreach (Reminder reminder in reminders)
            {
                Console.WriteLine($"Id: {reminder.Id}, Title: {reminder.Title}, Reminder Date: {reminder.ReminderDate:yyyy-MM-dd}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to Reminders menu.");

            Console.ReadKey();

            return;
        }

        public void GetDetails()
        {
            Console.WriteLine("Enter Reminder Id:");

            bool isIdInt = Int32.TryParse(Console.ReadLine(), out int id);

            if (!isIdInt)
            {
                Console.Clear();
                ConsoleUtilities.ShowMessage("Invalid value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            };

            Reminder reminder = _reminderService.GetDetails(id);

            if (reminder == null)
            {
                Console.Clear();
                ConsoleUtilities.ShowMessage("Reminder with selected Id doesn't exist", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Reminder Details:");
                Console.WriteLine($"Id: {reminder.Id}");
                Console.WriteLine($"Title: {reminder.Title}");
                Console.WriteLine($"Description: {reminder.Description}");
                Console.WriteLine($"Reminder Date: {reminder.ReminderDate:yyyy-MM-dd}");
                Console.WriteLine($"Periodicity: {reminder.Periodicity}");

                Console.WriteLine();
                Console.WriteLine("Press any key to return to Reminders menu.");

                Console.ReadKey();

                return;
            }
        }

        public void Add()
        {
            Console.WriteLine("Add Reminder");
            Console.WriteLine();
            Console.WriteLine("Enter Title value:");

            string title = Console.ReadLine();

            Console.WriteLine("Enter Description value:");

            string description = Console.ReadLine();

            Console.WriteLine("Enter Reminder Date in YYYY/MM/DD format:");

            string reminderDate = Console.ReadLine();

            Console.WriteLine("Do you want to set Periodicity? Y/N");

            string boolPeriodicity = Console.ReadKey().KeyChar.ToString();

            string reminderPeriodicity = String.Empty;

            if (boolPeriodicity.Equals("Y"))
            {
                EnumUtilities.DisplayAllRemindersPeriodicityOptions();
                reminderPeriodicity = Console.ReadKey().KeyChar.ToString();
            } 
            else if (!boolPeriodicity.Equals("Y") && !boolPeriodicity.Equals("N"))
            {
                Console.WriteLine("Invalid value");
                Console.WriteLine("Returning to Reminder Menu...");
                Thread.Sleep(3000);
                return;
            }

            ReminderDto reminderDto = new ReminderDto
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                ReminderPeriodicity = reminderPeriodicity
            };

            (bool, string, Reminder, int) reminder = _reminderService.Add(reminderDto);

            Console.Clear();

            if (reminder.Item1 == false)
            {
                ConsoleUtilities.ShowMessage(reminder.Item2, "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }
            else if (reminder.Item1 == true && reminder.Item4 == 0)
            {
                ConsoleUtilities.ShowMessage($"Created new reminder with Id: {reminder.Item3.Id}", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }
            else
            {
                ConsoleUtilities.ShowMessage($"Created new reminder with Id: {reminder.Item3.Id} and {reminder.Item4} have been created by the end of the year indicated for the reminder", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }
        }

        public void Delete()
        {
            Console.WriteLine("Delete Reminder");
            Console.WriteLine();
            Console.WriteLine("Enter Id:");

            bool isInt = Int32.TryParse(Console.ReadLine(), out int id);

            if (!isInt)
            {
                Console.Clear();
                ConsoleUtilities.ShowMessage("Invalid value", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            bool delete = _reminderService.Delete(id);

            if (!delete)
            {
                Console.Clear();
                ConsoleUtilities.ShowMessage("Reminder with given Id doesn't exist", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }

            Console.Clear();

            ConsoleUtilities.ShowMessage($"Reminder with Id: {id} was deleted", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);

            return;
        }

        public async Task AddRemindersFromFile()
        {
            try
            {
                Console.WriteLine("Enter path file:");

                string filePath = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine("Enter type of data in file:");
                Console.WriteLine("1. XML");
                Console.WriteLine("2. JSON");
                Console.WriteLine("3. CSV");

                bool isInt = Int32.TryParse(Console.ReadKey().KeyChar.ToString(), out int dataTypeInt);

                Console.Clear();

                if (!isInt || dataTypeInt > 3)
                {
                    ConsoleUtilities.ShowMessage("Invalid data type", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                    return;
                }

                IEnumerable<ReminderDto> reminderDtos = dataTypeInt switch
                {
                    1 => _fileService.DeserializeDataFromXmlFileToIEnumerableOfDtos<ReminderDto>(filePath),
                    2 => await _fileService.DeserializeDataFromJsonFileToIEnumerableOfDtos<ReminderDto>(filePath),
                    3 => await _fileService.DeserializeDataFromCsvFileToIEnumerableOfDtos<ReminderDto>(filePath)
                };

                (int, int, int, IEnumerable<ReminderDto>) addReminders = _reminderService.AddReminders(reminderDtos);

                if (addReminders.Item1 > 0)
                {
                    Console.WriteLine($"{addReminders.Item1} reminders were added correclty.");
                }
                if (addReminders.Item2 > 0)
                {
                    Console.WriteLine($"{addReminders.Item2} reminders were added based on periodicity given in main reminders");
                }
                if (addReminders.Item3 > 0)
                {
                    Console.WriteLine($"{addReminders.Item3} reminders were invalid");
                    Console.WriteLine("List of invalid reminders (by Title):");

                    int counter = 1;

                    foreach (ReminderDto reminder in addReminders.Item4)
                        Console.WriteLine($"{counter}. {reminder.Title}");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to Reminders menu.");

                Console.ReadKey();

                return;
            }
            catch (Exception exception)
            {
                if (
                    exception is ArgumentOutOfRangeException || 
                    exception is FileNotFoundException || 
                    exception is InvalidOperationException ||
                    exception is JsonReaderException                 
                    )
                {
                    ConsoleUtilities.ShowMessage(exception.Message, "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                    return;
                }

                ConsoleUtilities.ShowMessage("Something gone wrong", "Returning to Reminder Menu...", GlobalVariables.GlobalThreadSleepMiliseconds);
                return;
            }
        }
    }
}
