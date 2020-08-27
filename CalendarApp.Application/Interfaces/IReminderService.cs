using CalendarApp.Application.DTO;
using CalendarApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CalendarApp.Application.Interfaces
{
    public interface IReminderService
    {
        IEnumerable<Reminder> GetAllRemindersForDay(int year, int month, int day);

        int CountAllRemindersForDate(DateTime date);

        IEnumerable<Reminder> GetAllRemindersForMonth(int year, int month);

        Reminder GetDetails(int id);

        (bool isAdded, string message, Reminder reminder, int countAddedReminders) Add(ReminderDto reminderDto);

        (int countRemindersAddedCorrectly, int countRemindersBasedPeriodicity, int countInvalidReminders, IEnumerable<ReminderDto> invalidReminders) AddReminders(IEnumerable<ReminderDto> reminderDtos);

        bool Delete(int id);
    }
}
