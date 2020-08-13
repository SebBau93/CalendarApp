namespace CalendarApp.Application.Interfaces
{
    public interface ICalendarService
    {
        int[,] GetCalendarArrayForMonth(int year, int month);
    }
}
