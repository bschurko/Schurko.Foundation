using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schurko.Foundation.Tests.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool IsCompleted { get; set; }

        public Task(int id, string? title, string? desc, DateTime? dateCreated, bool isCompleted)
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
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; } // True for positive sentiment, False for negative sentiment
        public string Label { get; set; } // Not used in this example, but required for ML.NET
        public DateTime? CreatedDate { get; set; }
    }
}