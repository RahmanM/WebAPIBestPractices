using Dotnet.Core.Todos.Data;

namespace Dotnet.Core.Todos.Database
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }
    }

}
