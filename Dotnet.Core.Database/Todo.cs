namespace Dotnet.Core.Todos.Database
{
    public class Todo
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool Active { get; set; }
    }

}
