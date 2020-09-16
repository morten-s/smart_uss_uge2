using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using smart_uss_uge2.Models;

namespace smart_uss_uge2.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Covid19>> GetItemsAsync(string query);
        Task<Covid19> GetItemAsync(string id);
        Task AddItemAsync(Covid19 item);
        Task UpdateItemAsync(string id, Covid19 item);
        Task DeleteItemAsync(string id);
    }
}
