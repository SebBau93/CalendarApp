using CalendarApp.Application.DTO;
using CalendarApp.Application.Interfaces;
using CalendarApp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CalendarApp.Application.Services
{
    public class FileService<TEntity> : IFileService<TEntity> where TEntity : BaseEntity
    {
        private readonly string entityName = typeof(TEntity).Name;

        public async Task<IEnumerable<TDeserializedType>> DeserializeDataFromCsvFileToIEnumerableOfDtos<TDeserializedType> (string filePath) where TDeserializedType : Dto
        {
            try
            {
                string[] csvContent = File.ReadAllLinesAsync(filePath).Result;

                IEnumerable<TDeserializedType> deserializedData;

                if (typeof(TEntity) == typeof(Reminder))
                {
                    deserializedData = (IEnumerable<TDeserializedType>) ConvertCsvTextToIEnumerableOfReminderDto(csvContent);
                }
                else
                    throw new ArgumentOutOfRangeException("Entity " + entityName + "is not supported in this service");

                return deserializedData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TDeserializedType>> DeserializeDataFromJsonFileToIEnumerableOfDtos<TDeserializedType> (string filePath) where TDeserializedType : Dto
        {
            try
            {
                string jsonText = await File.ReadAllTextAsync(filePath);

                IEnumerable<TDeserializedType> deserializedData = JsonConvert.DeserializeObject<IEnumerable<TDeserializedType>>(jsonText);

                return deserializedData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TDeserializedType> DeserializeDataFromXmlFileToIEnumerableOfDtos<TDeserializedType> (string filePath) where TDeserializedType : Dto
        {          
            try
            {
                XmlRootAttribute xmlRoot = new XmlRootAttribute();

                if (typeof(TEntity) == typeof(Reminder))
                    xmlRoot.ElementName = "Reminders";
                else
                    throw new ArgumentOutOfRangeException("Entity " + entityName + "is not supported in this service");

                XmlReaderSettings settings = new XmlReaderSettings
                {
                    Async = true
                };

                using XmlReader xmlReader = XmlReader.Create(filePath, settings);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TDeserializedType>), xmlRoot);
                IEnumerable<TDeserializedType> deserializedData = (IEnumerable<TDeserializedType>) xmlSerializer.Deserialize(xmlReader);
                return deserializedData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IEnumerable<ReminderDto> ConvertCsvTextToIEnumerableOfReminderDto(string[] csvContent)
        {
            try
            {
                List<ReminderDto> reminderDtos = new List<ReminderDto>();

                foreach (string csvLine in csvContent)
                {
                    string[] values = csvLine.Split(',');
                    ReminderDto reminderDto = new ReminderDto()
                    {
                        Title = values[0],
                        Description = values[1],
                        ReminderDate = values[2],
                        ReminderPeriodicity = values[3]
                    };

                    reminderDtos.Add(reminderDto);
                }

                return reminderDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
