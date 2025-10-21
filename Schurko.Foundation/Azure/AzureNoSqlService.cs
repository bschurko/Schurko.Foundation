using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;

namespace Schurko.Foundation.Azure
{
    public class AzureNoSqlService<T> : IRepository<T> where T : class
    {
        private string _cosmosUrl = "";
        private string _cosmosKey = "";
        private string _databaseName = "";
        private string _containerName = "";
        private string _partitionKeyPath = "";
        private string _partitionKey = "";
        private CosmosClient _client;
        private Database _database;
        private Container _container;
        
        

        public AzureNoSqlService(string cosmosUrl, 
            string cosmosKey, 
            string databaseName, 
            string containerName,
            string partitionKeyPath,
            string partitionKey)
        {
            _cosmosUrl = cosmosUrl;
            _cosmosKey = cosmosKey;
            _databaseName = databaseName;
            _containerName = containerName;
            _partitionKeyPath = partitionKeyPath;
            _partitionKey = partitionKey;

            _client = new CosmosClient(_cosmosUrl, _cosmosKey);
            _database = _client.GetDatabase(_databaseName);
            _container = _database.GetContainer(_containerName);
        }
 
        public async Task<T> Add<T>(T entity)
        {
            var createdItem = await _container.CreateItemAsync<T>
            (   
                item: entity,
                partitionKey: new PartitionKey(_partitionKey)
            );

            return createdItem.Resource;
        }

        public async Task<T> Update<T>(T entity, string id)
        {
            {
                ItemResponse<T> replacedItem = await _container.ReplaceItemAsync<T>(
                    item: entity,
                    id: id,
                    partitionKey: new PartitionKey(_partitionKey)
                );

                return replacedItem.Resource;
            }
        }
 

        public async Task Delete<T>(T entity, string id)
        {
            var partitionKey = new PartitionKey(_partitionKey);

            var result = await _container.DeleteItemAsync<T>(
                id: id,
                partitionKey: partitionKey
            );
        }

       

       

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            List<T> list = new List<T>();
            
            try
            {
                
                 
                var queryable = _container.GetItemLinqQueryable<T>(true);
                
                using FeedIterator<T> linqFeed = queryable.ToFeedIterator();

                while (linqFeed.HasMoreResults)
                {
                     FeedResponse<T> response = await linqFeed.ReadNextAsync();

                     list.AddRange(response.Resource.ToList());
                }

                linqFeed.Dispose();
                
                return list;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return list;
        }

        public async Task<T> GetById<T>(string id)
        {
            var partitionKey = new PartitionKey(_partitionKey);

            try
            {
                // Perform a point read operation
                ItemResponse<T> response = await _container.ReadItemAsync<T>(
                    id: id,
                    partitionKey: partitionKey
                );

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Handle item not found scenario
                return default(T);
            }
        }
 
    }

    public interface IRepository<TEntity> where TEntity : class
    {
        Task<T> Add<T>(T entity);

        Task<T> Update<T>(T entity, string id);

        Task Delete<T>(T entity, string id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<T> GetById<T>(string id);
         
    }
}
