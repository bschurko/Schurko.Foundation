using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Schurko.Foundation.Tests.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        public Task(int id, string? title, string? desc, DateTime? dateCreated, bool isCompleted )
        {
            Id = id;
            Title = title;
            Description = desc;
            DateCreated = dateCreated;
            IsCompleted = isCompleted;
      
        }

       
    }

    public class TaskDto
    {
        
        public string Id { get; set; }
        [JsonPropertyName("id")]
        public string id => Id;
        [JsonPropertyName("partitionKey")]
        public string partitionKey { get; set; }
        [JsonPropertyName("schurko")]
        public string schurko { get; set; }
        //[JsonPropertyName("id")]
        //public string id =>  Id.ToString();  // Read-only alias

        public string Text { get; set; }
        public bool IsCompleted { get; set; } // True for positive sentiment, False for negative sentiment
        public string Label { get; set; } // Not used in this example, but required for ML.NET
        public DateTime? CreatedDate { get; set; }
    }
}