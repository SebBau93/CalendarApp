using CalendarApp.Application.DTO;
using CalendarApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Application.Interfaces
{
    public interface IFileService<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TDeserializedType> DeserializeDataFromXmlFileToIEnumerableOfDtos<TDeserializedType>(string filePath) where TDeserializedType : Dto;

        Task<IEnumerable<TDeserializedType>> DeserializeDataFromJsonFileToIEnumerableOfDtos<TDeserializedType>(string filePath) where TDeserializedType : Dto;

        Task<IEnumerable<TDeserializedType>> DeserializeDataFromCsvFileToIEnumerableOfDtos<TDeserializedType>(string filePath) where TDeserializedType : Dto;
    }
}
