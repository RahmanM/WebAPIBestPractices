using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Core.Todos.Data
{
    public class TodoDto : IDto
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Completed { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool Active { get; set; }

        public string Category { get; set; }
    }

    public class TodoByCategory : IDto
    {
        public int CategoryId { get; set; }

        public int Count { get; set; }

        public string Category { get; set; }

        public int Id { get ; set ; }
    }
}
