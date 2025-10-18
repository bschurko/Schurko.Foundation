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
}