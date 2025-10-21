using Microsoft.Extensions.Configuration;
using Schurko.Foundation.Azure;
using Schurko.Foundation.Tests.Models;
using Task = System.Threading.Tasks.Task;

namespace Schurko.Foundation.Tests;

[TestClass]
public class AzureNoSqlServiceTests
{
    private IConfiguration _configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()) // or use AppContext.BaseDirectory
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();


    //private  string _cosmosUrl = _configuration["CosmosUrrl"];
    //private  string _cosmosKey = _configuration["CosmosKey"];
    //private  string _databaseName = _configuration["DatabaseName"];
    //private  string _containerName = _configuration["ContainerName"];
    //private  string _partitionKeyPath = _configuration["PartitionKeyPath"]; 
    //private  string _partitionKey = _configuration["PartitionKey"];      

    //private AzureNoSqlService<TaskDto> service = new(_cosmosUrl, _cosmosKey, _databaseName,
    //    _containerName, _partitionKeyPath, _partitionKey);
    private AzureNoSqlService<TaskDto> service;
    public AzureNoSqlServiceTests()
    {
    
         string _cosmosUrl = _configuration["CosmosUrrl"];
         string _cosmosKey = _configuration["CosmosKey"];
         string _databaseName = _configuration["DatabaseName"];
         string _containerName = _configuration["ContainerName"];
         string _partitionKeyPath = _configuration["PartitionKeyPath"];
         string _partitionKey = _configuration["PartitionKey"];

          service = new(_cosmosUrl, _cosmosKey, _databaseName,
            _containerName, _partitionKeyPath, _partitionKey);
        }

    [TestMethod]
    public async Task Processes()
    {
        TaskDto entity = new TaskDto
        {
            CreatedDate = DateTime.Now,
            Id = new Random().Next(0,99999).ToString(),
            IsCompleted = false,
            Label = "This is a generic label",
            Text = "This is the title",
            partitionKey = _configuration["PartitionKey"],
            schurko = _configuration["PartitionKeyPath"]
        };
        
        Console.WriteLine("Begining to create AzureNoSqlService object..");
        
        TaskDto addedEntity = await service.Add<TaskDto>(entity);
        entity.CreatedDate = DateTime.Now;
        entity.Label = "This is updated label";
        entity.Text = "Updated Text";
        TaskDto updatedEntity = await service.Update(entity, entity.Id);
        
        TaskDto singleEntity = await service.GetById<TaskDto>(entity.Id);
        var listEntities = await service.GetAll<TaskDto>();
        TaskDto lookupEntity = listEntities.FirstOrDefault(cc => cc.id == entity.id);
        await service.Delete(lookupEntity, lookupEntity.id);

    }
}
