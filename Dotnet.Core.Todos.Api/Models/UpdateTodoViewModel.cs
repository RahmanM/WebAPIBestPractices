using System.ComponentModel.DataAnnotations;

namespace Dotnet.Core.Todos.Api.Models
{
    /// <summary>
    /// A class representing a Todo
    /// </summary>
    public class UpdateTodoViewModel
    {

        /// <summary>
        /// Unique identifier of the Todo
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Indicates if Todo is completed
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Category of the Todo
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Is Todo active
        /// </summary>
        public bool Active { get; set; }

    }
}
