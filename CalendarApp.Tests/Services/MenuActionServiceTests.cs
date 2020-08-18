using CalendarApp.Application.Interfaces;
using CalendarApp.Application.Services;
using CalendarApp.Domain.Constants;
using CalendarApp.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CalendarApp.Tests.Services
{
    public class MenuActionServiceTests
    {
        [Fact]
        public void ShouldReturnEmptyListForNonExistMenuActionTypeInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(5);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEmpty();
        }

        [Fact]
        public void ShouldNotBeNullOrEmptyAndReturnListWithThreeElementsForMenuActionTypeMainInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(1);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(3);
        }

        [Fact]
        public void ShouldReturnListWithContainsAllThreeElementsFromArrangeForMenuActionTypeMainInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();
            MenuAction menuActionOne = new MenuAction { Id = 1, Name = "Calendar", Type = MenuActionTypes.Main };
            MenuAction menuActionTwo = new MenuAction { Id = 2, Name = "Reminders", Type = MenuActionTypes.Main };
            MenuAction menuActionThree = new MenuAction { Id = 3, Name = "Close application", Type = MenuActionTypes.Main };

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(1);

            // Assert
            response.Should().ContainEquivalentOf(menuActionOne);
            response.Should().ContainEquivalentOf(menuActionTwo);
            response.Should().ContainEquivalentOf(menuActionThree);
        }

        [Fact]
        public void ShouldNotBeNullOrEmptyAndReturnListWithTwoElementsForMenuActionTypeCalendarInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(2);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldReturnListWithContainsAllTwoElementsFromArrangeForMenuActionTypeCalendarInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();
            MenuAction menuActionOne = new MenuAction { Id = 1, Name = "Calendar for selected month", Type = MenuActionTypes.Calendar };
            MenuAction menuActionTwo = new MenuAction { Id = 2, Name = "Main menu", Type = MenuActionTypes.Calendar };

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(2);

            // Assert
            response.Should().ContainEquivalentOf(menuActionOne);
            response.Should().ContainEquivalentOf(menuActionTwo);
        }

        [Fact]
        public void ShouldNotBeNullOrEmptyAndReturnListWithSixElementsForMenuActionTypeReminderInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(3);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(6);
        }

        [Fact]
        public void ShouldReturnListWithContainsAllSixElementsFromArrangeForMenuActionTypeReminderInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();
            MenuAction menuActionOne = new MenuAction { Id = 1, Name = "Reminders for a selected month", Type = MenuActionTypes.Reminder };
            MenuAction menuActionTwo = new MenuAction { Id = 2, Name = "Reminders for a selected day", Type = MenuActionTypes.Reminder };
            MenuAction menuActionThree = new MenuAction { Id = 3, Name = "Reminder details", Type = MenuActionTypes.Reminder };
            MenuAction menuActionFour = new MenuAction { Id = 4, Name = "Add new reminder", Type = MenuActionTypes.Reminder };
            MenuAction menuActionFive = new MenuAction { Id = 5, Name = "Delete reminder", Type = MenuActionTypes.Reminder };
            MenuAction menuActionSix = new MenuAction { Id = 6, Name = "Main menu", Type = MenuActionTypes.Reminder };

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(3);

            // Assert
            response.Should().ContainEquivalentOf(menuActionOne);
            response.Should().ContainEquivalentOf(menuActionTwo);
            response.Should().ContainEquivalentOf(menuActionThree);
            response.Should().ContainEquivalentOf(menuActionFour);
            response.Should().ContainEquivalentOf(menuActionFive);
            response.Should().ContainEquivalentOf(menuActionSix);
        }

        [Fact]
        public void ShouldNotBeNullOrEmptyAndReturnListWithTwoElementsForMenuActionTypeCalendarMonthInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(4);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().HaveCount(2);
        }

        [Fact]
        public void ShouldReturnListWithContainsAllTwoElementsFromArrangeForMenuActionTypeCalendarMonthInReturnMenuActionsByTypeMethod()
        {
            // Arrange
            IMenuActionService menuActionService = new MenuActionService();
            MenuAction menuActionOne = new MenuAction { Id = 1, Name = "Calendar", Type = MenuActionTypes.CalendarMonth };
            MenuAction menuActionTwo = new MenuAction { Id = 2, Name = "Reminders for a selected month", Type = MenuActionTypes.CalendarMonth };

            // Act
            IEnumerable<MenuAction> response = menuActionService.ReturnMenuActionsByType(4);

            // Assert
            response.Should().ContainEquivalentOf(menuActionOne);
            response.Should().ContainEquivalentOf(menuActionTwo);
        }
    }
}
