using System.Xml.Serialization;

namespace CalendarApp.Domain.Entities
{
    public class BaseEntity
    {
        [XmlElement]
        public int Id { get; set; }
    }
}
