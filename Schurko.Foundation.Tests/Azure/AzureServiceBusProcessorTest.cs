using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schurko.Foundation.Azure;
using Schurko.Foundation.Tests.Models;
using Task = Schurko.Foundation.Tests.Models.Task;

namespace Schurko.Foundation.Tests;

[TestClass]
public class AzureServiceBusProcessorTest
{
    const string connStr = "Endpoint=sb://bschurko.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5NJasGHsIv1z3VIHMJpC4h1UjU+BVoHHi+ASbGY5Ues=";
    const string queueName = "schurkoqueue";
    
    [TestMethod]
    public async System.Threading.Tasks.Task Produce_Message_To_Azure_Service_Bus_Test()
    {
        AzureServiceBusProcessor processor = new AzureServiceBusProcessor(connStr, queueName);
        Task task = new Task(1, "Title", "Description", DateTime.Now, false); 
        processor.PublishdMessage(task).Wait();

    }

    [TestMethod]
    public async System.Threading.Tasks.Task  Consume_Message_From_Azure_Service_Bus_Test()
    {
        AzureServiceBusProcessor processor = new AzureServiceBusProcessor(connStr, queueName);
        Task task = new Task(1, "Title", "Description", DateTime.Now, false);
        task = await processor.ConsumeMessage<Task>();
        Assert.IsNotNull(task);
    }
}
