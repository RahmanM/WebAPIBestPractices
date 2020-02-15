using Microsoft.EntityFrameworkCore;

namespace Dotnet.Core.Todos.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        {

        }

        // Constructor for testing e.g. passing the InMemoryContext if needed
        public TodoContext(DbContextOptions<TodoContext> options)
       : base(options)
        { }

        public virtual DbSet<Todo> Todos { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql("server=localhost;user id=todoUser;password=todoUser;port=3306;database=todos;");
            //}

            // #lazyloading
            // Enable lazy loading so that category is available when referencing todo.Category for example
            optionsBuilder.UseLazyLoadingProxies();
        }
    }

}
