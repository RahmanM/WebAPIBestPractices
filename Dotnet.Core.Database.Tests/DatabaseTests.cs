using Dotnet.Core.Todos.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dotnet.Core.Database.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        //[Ignore]
        [TestMethod]
        public void RefreshDatabaseAndData()
        {
            var context = new TodoContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            SeedData(context);

            var todos = context.Todos.ToList();

            Assert.IsTrue(todos.Count == 2);
        }

        private void SeedData(TodoContext context)
        {
            context.Categories.Add(new Category { Active = true, Description = "Category 1" });
            context.Categories.Add(new Category { Active = true, Description = "Category 2" });

            context.Todos.Add(new Todo { Active = true, CategoryId = 1, Completed = false, Description = "Todo 1" });
            context.Todos.Add(new Todo { Active = true, CategoryId = 2, Completed = false, Description = "Todo 2" });

            context.SaveChanges();

        }

        //[TestMethod]
        //public void TestTodoByCategory()
        //{
        //    var context = new TodoContext();

        //    var query = (
        //                    from t in context.Todos
        //                    group t by new { t.CategoryId, t.Category } into grp
        //                    select new 
        //                    {
        //                        CategoryId = grp.Key.CategoryId,
        //                        Count = grp.Count(),
        //                        Category = grp.Key.Category.Description
        //                    }
        //                ).ToList();

        //    Assert.IsTrue(1 == 1);
        //}
    }
}
