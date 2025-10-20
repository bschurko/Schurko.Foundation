using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Schurko.Foundation.MongoDb
{
    public class MongoDbService<T> : IRepository<T> where T : class
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private string _connectionString;
        private string _databaseName;
        private string _collectionName;

        public MongoDbService(string connectionString, string databaseName, string collectionName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
            _collectionName = collectionName;

            _client = new MongoClient(_connectionString);
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<T>(_collectionName);
        }


        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<T> Update(T entity, string idFieldName)
        {
            var value = typeof(T).GetProperty(idFieldName)?.GetValue(entity);
            var filterById = Builders<T>.Filter.Eq(idFieldName, value);
            await _collection.ReplaceOneAsync(filterById, entity);

            var filter = Builders<T>.Filter.Eq(idFieldName, value);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Delete(T entity, string idFieldName)
        {
            var value = typeof(T).GetProperty(idFieldName)?.GetValue(entity);
            FilterDefinition<T> deleteFilter = Builders<T>.Filter.Eq(idFieldName, value);
            await _collection.DeleteOneAsync(deleteFilter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> GetById(int id, string idFieldName)
        {
            var filter = Builders<T>.Filter.Eq(idFieldName, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


    }

    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task<TEntity> Update(TEntity entity, string idFieldName);

        Task Delete(TEntity entity, string idFieldName);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id, string idFieldName);

    }
}
