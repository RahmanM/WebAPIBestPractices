using Dotnet.Core.Todos.Data;
using Sieve.Attributes;

namespace Dotnet.Core.Todos.Database
{
    public class Todo : IEntity
    {
        [Sieve(CanFilter = true, CanSort = true)]

        public int Id { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]

        public string Description { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]

        public bool Completed { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Sieve(CanFilter = true, CanSort = true)]

        public bool Active { get; set; }
    }

}
