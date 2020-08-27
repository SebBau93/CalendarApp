using CalendarApp.Domain.Entities;
using System;
using System.Xml.Serialization;

namespace CalendarApp.Application.DTO
{
    [XmlType("Reminder")]
    public class ReminderDto : Dto
    {
        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public string ReminderDate { get; set; }

        [XmlElement]
        public string ReminderPeriodicity { get; set; }
    }
}
