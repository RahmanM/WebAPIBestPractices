using Dotnet.Core.Todos.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Dotnet.Core.Todos.Data.Tests
{
    public class TodoContextTestsbase : IDisposable
    {
        DbContextOptions<TodoContext> options;
        TodoContext context;

        public TodoContext Context
        {
            get { return context; }
        }

        public TodoContextTestsbase()
        {
            // NB: Need to create a unique name for the database
            string databaseName = Guid.NewGuid().ToString();
            options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: databaseName).Options;
            context = new TodoContext(options);
        }

        public void SeedTodos(TodoContext context, int noOfTodos)
        {
            var todos = new List<Todo>();

            for (int i = 0; i < noOfTodos; i++)
            {
                todos.Add(new Todo()
                {
                    Active = true,
                    CategoryId = 1,
                    Completed = false,
                    Description = "Todo " + (i + 1),
                    Id = i + 1
                });
            };

            var categories = new List<Category>();

            for (int i = 0; i < noOfTodos; i++)
            {
                categories.Add(new Category { Description = "Category "  +(i + 1), Id = i+1, Active = true });
            }


            context.Todos.AddRange(todos);
            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
