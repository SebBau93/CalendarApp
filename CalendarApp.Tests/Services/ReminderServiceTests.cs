using CalendarApp.Application.DTO;
using CalendarApp.Application.Interfaces;
using CalendarApp.Application.Services;
using CalendarApp.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalendarApp.Tests.Services
{
    public class ReminderServiceTests
    {
        [Fact]
        public void ShouldReturnOneForSelectedDateFromCountAllRemindersForDateMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title", Description = "Description", ReminderDate = "2020/08/18" };
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) = reminderService.Add(reminderDto);

            // Act
            int response = reminderService.CountAllRemindersForDate(new DateTime(2020, 8, 18));

            // Assert
            response.Should().NotBe(0);
            response.Should().BeOfType(typeof(int));
            response.Should().Equals(1);

            // Clean
            reminderService.Delete(reminder.Id);
        }

        [Fact]
        public void ShouldReturnZeroFromCountAllRemindersForDateMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();

            // Act
            int response = reminderService.CountAllRemindersForDate(new DateTime(2020, 8, 18));

            // Assert
            response.Should().BeOfType(typeof(int));
            response.Should().Equals(0);
        }


        [Fact]
        public void ShouldReturnListWithThreeElementsForSelectedYearAndMonthFromGetAllRemindersForMonthMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDtoOne = new ReminderDto { Title = "Title", Description = "Description", ReminderDate = "2020/08/18" };
            ReminderDto reminderDtoTwo = new ReminderDto { Title = "Title", Description = "Description", ReminderDate = "2020/08/19" };
            ReminderDto reminderDtoThree = new ReminderDto { Title = "Title", Description = "Description", ReminderDate = "2020/08/20" };
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponseOne = reminderService.Add(reminderDtoOne);
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponseTwo = reminderService.Add(reminderDtoTwo);
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponseThree = reminderService.Add(reminderDtoThree);


            // Act
            IEnumerable<Reminder> response = reminderService.GetAllRemindersForMonth(2020, 8);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(3);

            // Clean
            reminderService.Delete(reminderServiceAddResponseOne.reminder.Id);
            reminderService.Delete(reminderServiceAddResponseTwo.reminder.Id);
            reminderService.Delete(reminderServiceAddResponseThree.reminder.Id);
        }

        [Fact]
        public void ShouldReturnEmptyListForSelectedYearAndMonthFromGetAllRemindersForMonthMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();

            // Act
            IEnumerable<Reminder> response = reminderService.GetAllRemindersForMonth(2020, 8);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnListWithTwoElementsForSelectedYearAndMonthAndDayFromGetAllRemindersForDayMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDtoOne = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/08/18" };
            ReminderDto reminderDtoTwo = new ReminderDto { Title = "Title 2", Description = "Description", ReminderDate = "2020/08/18" };
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponseOne = reminderService.Add(reminderDtoOne);
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponseTwo = reminderService.Add(reminderDtoTwo);


            // Act
            IEnumerable<Reminder> response = reminderService.GetAllRemindersForDay(2020, 8, 18);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(2);

            // Clean
            reminderService.Delete(reminderServiceAddResponseOne.reminder.Id);
            reminderService.Delete(reminderServiceAddResponseTwo.reminder.Id);
        }

        [Fact]
        public void ShouldReturnEmptyListForSelectedYearAndMonthAndDayFromGetAllRemindersForDayMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();

            // Act
            IEnumerable<Reminder> response = reminderService.GetAllRemindersForDay(2020, 8, 1);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnNullFromGetDetailsMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();

            // Act
            Reminder response = reminderService.GetDetails(1);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnReminderWithExpectedDataFromGetDetailsMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/08/18" };
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponse = reminderService.Add(reminderDto);

            // Act
            Reminder response = reminderService.GetDetails(reminderServiceAddResponse.reminder.Id);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(Reminder));
            response.Id.Should().Equals(reminderServiceAddResponse.reminder.Id);
            response.Title.Should().Equals(reminderDto.Title);
            response.Description.Should().Equals(reminderDto.Description);
            response.ReminderDate.Should().Equals(DateTime.Parse(reminderDto.ReminderDate));
            response.Periodicity.Should().Equals(0);

            // Clean
            reminderService.Delete(reminderServiceAddResponse.reminder.Id);
        }

        [Fact]
        public void ShouldReturnTupleWithValidationMessageForInvalidReminderDateValueForAddMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/00/00" };

            // Act
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) response = reminderService.Add(reminderDto);

            // Assert
            response.Should().NotBeNull();
            response.isAdded.Should().BeFalse();
            response.message.Should().Equals("Invalid Reminder Date value");
            response.reminder.Should().BeNull();
            response.countAddedReminders.Should().Be(0);
        }

        [Fact]
        public void ShouldReturnTupleWithValidationMessageForInvalidReminderPeriodicityEnumValueForAddMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/08/18", ReminderPeriodicity = "2000" };

            // Act
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) response = reminderService.Add(reminderDto);

            // Assert
            response.Should().NotBeNull();
            response.isAdded.Should().BeFalse();
            response.message.Should().Equals("Invalid Reminder Periodicity value");
            response.reminder.Should().BeNull();
            response.countAddedReminders.Should().Be(0);
        }

        [Fact]
        public void ShouldReturnTupleCorrectlyAddedReminderWithoutReminderPeriodicityForAddMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/08/18" };

            // Act
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) response = reminderService.Add(reminderDto);

            // Assert
            response.Should().NotBeNull();
            response.isAdded.Should().BeTrue();
            response.message.Should().NotBeNull();
            response.message.Should().BeEmpty();
            response.reminder.Should().NotBeNull();
            response.reminder.Should().BeOfType(typeof(Reminder));
            response.reminder.Title.Should().Equals(reminderDto.Title);
            response.reminder.Description.Should().Equals(reminderDto.Description);
            response.reminder.ReminderDate.Equals(DateTime.Parse(reminderDto.ReminderDate));
            response.countAddedReminders.Should().Be(0);

            // Clean
            reminderService.Delete(response.reminder.Id);
        }

        [Fact]
        public void shouldReturnFalseFromDeleteMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();

            // Act
            bool response = reminderService.Delete(1);

            // Assert
            response.Should().BeFalse();
        }

        [Fact]
        public void shouldReturnTrueFromDeleteMethod()
        {
            // Arrange
            IReminderService reminderService = new ReminderService();
            ReminderDto reminderDto = new ReminderDto { Title = "Title 1", Description = "Description", ReminderDate = "2020/08/18" };
            (bool isAdded, string message, Reminder reminder, int countAddedReminders) reminderServiceAddResponse = reminderService.Add(reminderDto);

            // Act
            bool response = reminderService.Delete(reminderServiceAddResponse.reminder.Id);

            // Assert
            response.Should().BeTrue();
        }
    }
}
