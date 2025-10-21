using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schurko.Foundation.MongoDb;
using Schurko.Foundation.Tests.Models;
using Task = System.Threading.Tasks.Task;

namespace Schurko.Foundation.Tests.MongoDb
{
    internal class MongoDbTests
    {
        [TestMethod]
        public async Task MongoDbServiceTest()
        {
            MongoDbService<TaskDto> service = GetService();
            TaskDto entity = new()
            {
                Id = new Random().Next(999, 999999).ToString(), 
                Text = "Simple Title",
                Label = "A description",
                CreatedDate = DateTime.Now,
                IsCompleted = false
            };

            await service.Add(entity);
            await Task.Delay(200);
            await service.Delete(entity, "Id");
            await service.Add(entity);
            entity.CreatedDate = DateTime.Now;
            entity.Text = "Updated in Test Class";
            entity.Label = "Updated in Test Class";
            await service.Update(entity, "Id");
            var newEntity = await service.GetById(int.Parse(entity.Id), "Id");
            Assert.IsNotNull(newEntity);
        }

        private MongoDbService<TaskDto> GetService()
        {
            // Do not store secrets in source code. Read connection string from environment variables
            // Set environment variable MONGO_CONNECTION_STRING in your CI/local environment instead of hard-coding it.
            string? _connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                // Fallback to empty string to avoid accidentally committing secrets. Tests should set the env var.
                _connectionString = string.Empty;
            }

            string _databaseName = "SchurkoDb";
            string _collectionName = "schurko";

            MongoDbService<TaskDto> service = new(_connectionString, _databaseName, _collectionName);

            return service;
        }
    }
}
