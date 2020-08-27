using CalendarApp.Domain.Enums;
using System;
using System.Xml.Serialization;

namespace CalendarApp.Domain.Entities
{
    public class Reminder : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReminderDate { get; set; }

        public RemindersPeriodicity Periodicity { get; set; }


    }
}
