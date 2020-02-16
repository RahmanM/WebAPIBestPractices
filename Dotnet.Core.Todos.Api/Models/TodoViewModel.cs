using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Core.Todos.Api.Models
{
    /// <summary>
    /// A class representing a Todo
    /// </summary>
    public class TodoViewModel
    {

        /// <summary>
        /// Unique identifier
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        [Required]
        [Sieve(CanFilter = true)]
        public string Description { get; set; }

        /// <summary>
        /// Indicates if Todo is completed
        /// </summary>
        [Sieve(CanFilter = true, CanSort = true)]
        public bool Completed { get; set; }

        /// <summary>
        /// Category of the Todo
        /// </summary>
        [Required]
        [Sieve(CanFilter = true, CanSort = true)]
        public int CategoryId { get; set; }

        /// <summary>
        /// Is Todo active
        /// </summary>
        public bool Active { get; set; }

    }
}
