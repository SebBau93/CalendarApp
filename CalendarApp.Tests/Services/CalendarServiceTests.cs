using CalendarApp.Application.Interfaces;
using CalendarApp.Application.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CalendarApp.Tests.Services
{
    public class CalendarServiceTests
    {
        [Fact]
        public void ShouldReturnNullInGetCalendarArrayForMonthMethod() 
        {
            // Arrange
            ICalendarService calendarService = new CalendarService();

            // Act
            int[,] response = calendarService.GetCalendarArrayForMonth(1111, 0);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnTwoDimmensionalArrayWithDaysForJanuary2020()
        {
            // Arrange
            ICalendarService calendarService = new CalendarService();

            // Act
            int[,] response = calendarService.GetCalendarArrayForMonth(2020, 1);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().BeOfType(typeof(int[,]));
            response.GetLength(0).Should().Equals(5);
            response.GetLength(0).Should().Equals(7);
            response[0, 0].Should().Equals(0);
            response[0, 2].Should().Equals(1);
            response[4, 4].Should().Equals(31);
        }

        [Fact]
        public void ShouldReturnTwoDimmensionalArrayWithDaysForJuly2018()
        {
            // Arrange
            ICalendarService calendarService = new CalendarService();

            // Act
            int[,] response = calendarService.GetCalendarArrayForMonth(2018, 7);

            // Assert
            response.Should().NotBeNullOrEmpty();
            response.Should().BeOfType(typeof(int[,]));
            response.GetLength(0).Should().Equals(6);
            response.GetLength(0).Should().Equals(7);
            response[0, 5].Should().Equals(0);
            response[0, 6].Should().Equals(1);
            response[5, 1].Should().Equals(31);
        }
    }
}
