namespace smart_uss_uge2.Services
{
        using System.Collections.Generic;
        using System.Linq;
        using System.Threading.Tasks;
        using smart_uss_uge2.Models;
        using Microsoft.Azure.Cosmos;
        using Microsoft.Azure.Cosmos.Fluent;
        using Microsoft.Extensions.Configuration;

        public class CosmosDbService : ICosmosDbService
        {
            private Container _container;

            public CosmosDbService(
                CosmosClient dbClient,
                string databaseName,
                string containerName)
            {
                this._container = dbClient.GetContainer(databaseName, containerName);
            }

            public async Task AddItemAsync(Covid19 item)
            {
                await this._container.CreateItemAsync<Covid19>(item, new PartitionKey(item.Id));
            }

            public async Task DeleteItemAsync(string id)
            {
                await this._container.DeleteItemAsync<Covid19>(id, new PartitionKey(id));
            }

            public async Task<Covid19> GetItemAsync(string id)
            {
                try
                {
                    ItemResponse<Covid19> response = await this._container.ReadItemAsync<Covid19>(id, new PartitionKey(id));
                    return response.Resource;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

            }

            public async Task<IEnumerable<Covid19>> GetItemsAsync(string queryString)
            {
                var query = this._container.GetItemQueryIterator<Covid19>(new QueryDefinition(queryString));
                List<Covid19> results = new List<Covid19>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    results.AddRange(response.ToList());
                }

                return results;
            }

            public async Task UpdateItemAsync(string id, Covid19 item)
            {
                await this._container.UpsertItemAsync<Covid19>(item, new PartitionKey(id));
            }
        }
    
}
