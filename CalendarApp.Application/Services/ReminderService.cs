using CalendarApp.Application.DTO;
using CalendarApp.Application.Interfaces;
using CalendarApp.Domain.Entities;
using CalendarApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarApp.Application.Services
{
    public class ReminderService : IReminderService
    {
        private readonly List<Reminder> _reminders;

        public ReminderService()
        {
            _reminders = new List<Reminder>();
        }

        public int CountAllRemindersForDate(DateTime date)
        {
            return _reminders.Where(r => r.ReminderDate.Date == date).Count();
        }

        public IEnumerable<Reminder> GetAllRemindersForMonth(int year, int month)
        {
            return _reminders.Where(r => r.ReminderDate.Year == year && r.ReminderDate.Month == month);
        }

        public IEnumerable<Reminder> GetAllRemindersForDay(int year, int month, int day)
        {
            return _reminders.Where(r => r.ReminderDate.Year == year && r.ReminderDate.Month == month && r.ReminderDate.Day == day);
        }

        public Reminder GetDetails(int id)
        {
            return _reminders.FirstOrDefault(r => r.Id == id);
        }

        public (bool isAdded, string message, Reminder reminder, int countAddedReminders) Add(ReminderDto reminderDto)
        {
            Tuple<bool, string> validate = ValidateReminderDto(reminderDto);

            if (validate.Item1 == false)
            {
                return (false, validate.Item2, null, 0);
            }

            Reminder reminder = new Reminder
            {
                Id = _reminders.Count + 1,
                Title = reminderDto.Title,
                Description = reminderDto.Description,
                ReminderDate = DateTime.Parse(reminderDto.ReminderDate)
            };

            if (!String.IsNullOrEmpty(reminderDto.ReminderPeriodicity))
                reminder.Periodicity = (RemindersPeriodicity)Enum.Parse(typeof(RemindersPeriodicity), reminderDto.ReminderPeriodicity);

            _reminders.Add(reminder);

            int countAddedReminders = 0;

            if (!String.IsNullOrEmpty(reminderDto.ReminderPeriodicity))
                countAddedReminders = CreateNewRemindersFromPeriodicity(reminder.Periodicity, reminder);

            return (true, String.Empty, reminder, countAddedReminders);
        }

        public bool Delete(int id)
        {
            Reminder reminder = _reminders.FirstOrDefault(r => r.Id == id);

            if (reminder == null)
                return false;
            else
                _reminders.Remove(reminder);

            return true;
        }

        private Tuple<bool, string> ValidateReminderDto(ReminderDto reminderDto)
        {
            bool isDateTime = DateTime.TryParse(reminderDto.ReminderDate, out DateTime dateTime);

            if (!isDateTime)
                return new Tuple<bool, string>(false, "Invalid Reminder Date value");

            if (!String.IsNullOrEmpty(reminderDto.ReminderPeriodicity))
            {
                bool isEnum = Enum.IsDefined(typeof(RemindersPeriodicity), reminderDto.ReminderPeriodicity);

                if (!isEnum)
                    return new Tuple<bool, string>(false, "Invalid Reminder Periodicity value");
            }

            return new Tuple<bool, string>(true, String.Empty);
        }

        private int CreateNewRemindersFromPeriodicity(RemindersPeriodicity periodicity, Reminder reminder)
        {


            int countAddedReminders = periodicity switch
            {
                RemindersPeriodicity.Daily => CreateNewRemindersForDailyPeriodicity(reminder),
                RemindersPeriodicity.Weekly => CreateNewRemindersForWeeklyPeriodicity(reminder),
                RemindersPeriodicity.Monthly => CreateNewRemindersForMonthlyPeriodicity(reminder),
                RemindersPeriodicity.Quarterly => CreateNewRemindersForQuarterlyPeriodicity(reminder)
            };

            return countAddedReminders;
        }

        private int CreateNewRemindersForDailyPeriodicity(Reminder reminder)
        {
            int countAddedReminders = 0;
            DateTime endOfyear = new DateTime(reminder.ReminderDate.Year, 12, 31);
            TimeSpan span = endOfyear.Subtract(reminder.ReminderDate);
            int days = span.Days;
            DateTime nextDate = reminder.ReminderDate.AddDays(1);

            for (int i = 0; i < days; i++)
            {
                _reminders.Add(new Reminder
                {
                    Id = _reminders.Count + 1,
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = nextDate,
                    Periodicity = reminder.Periodicity
                });

                nextDate = nextDate.AddDays(1);
                countAddedReminders++;
            }

            return countAddedReminders;
        }

        private int CreateNewRemindersForWeeklyPeriodicity(Reminder reminder)
        {
            int countAddedReminders = 0;
            DateTime endOfyear = new DateTime(reminder.ReminderDate.Year, 12, 31);
            TimeSpan span = endOfyear.Subtract(reminder.ReminderDate);
            int weeks = span.Days / 7;
            DateTime nextDate = reminder.ReminderDate.AddDays(7);

            for (int i = 0; i < weeks; i++)
            {
                _reminders.Add(new Reminder
                {
                    Id = _reminders.Count + 1,
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = nextDate,
                    Periodicity = reminder.Periodicity
                });

                nextDate = nextDate.AddDays(7);
                countAddedReminders++;
            }

            return countAddedReminders;
        }

        private int CreateNewRemindersForMonthlyPeriodicity(Reminder reminder)
        {
            int countAddedReminders = 0;
            DateTime endOfyear = new DateTime(reminder.ReminderDate.Year, 12, 31);
            TimeSpan span = endOfyear.Subtract(reminder.ReminderDate);
            int months = span.Days / 30;
            DateTime nextDate = reminder.ReminderDate.AddMonths(1);

            for (int i = 0; i < months; i++)
            {
                _reminders.Add(new Reminder
                {
                    Id = _reminders.Count + 1,
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = nextDate,
                    Periodicity = reminder.Periodicity
                });

                nextDate = nextDate.AddMonths(1);
                countAddedReminders++;
            }

            return countAddedReminders;
        }

        private int CreateNewRemindersForQuarterlyPeriodicity(Reminder reminder)
        {
            int countAddedReminders = 0;
            DateTime endOfyear = new DateTime(reminder.ReminderDate.Year, 12, 31);
            TimeSpan span = endOfyear.Subtract(reminder.ReminderDate);
            int quarters = span.Days / 90;
            DateTime nextDate = reminder.ReminderDate.AddMonths(3);

            for (int i = 0; i < quarters; i++)
            {
                _reminders.Add(new Reminder
                {
                    Id = _reminders.Count + 1,
                    Title = reminder.Title,
                    Description = reminder.Description,
                    ReminderDate = nextDate,
                    Periodicity = reminder.Periodicity
                });

                nextDate = nextDate.AddMonths(3);
                countAddedReminders++;
            }

            return countAddedReminders;
        }
    }
}
