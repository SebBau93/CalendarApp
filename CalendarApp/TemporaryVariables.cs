namespace CalendarApp
{
    public static class TemporaryVariables
    {
        public static byte selectedAction = 0;
        public static int year = 0;
        public static int month = 0;

        public static void ClearSelectedActionValue()
        {
            selectedAction = 0;
        }

        public static void ClearYearAndMonthValue()
        {
            year = 0;
            month = 0;
        }
    }
}
