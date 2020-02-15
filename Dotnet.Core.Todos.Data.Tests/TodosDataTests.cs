using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dotnet.Core.Todos.Data.Tests
{

    [TestClass]
    public class TodosDataTests : TodoContextTestsbase
    {
 
        /*
         * Testing the AllTodosQuery query without actually connecting to the Database by mocking
         * the TodoContext and replacing it with InMemory context.
         * 
         * Then we seed data to InMemory context and running text against it but the logic of the 
         * AllTodosQuery is tested against the fake data!!
         */
        [TestMethod]
        public void TestAllTodosQuery()
        {
            // Arrange
            SeedTodos(Context, 3);

            // Act
            var todoQuery = new AllTodosQuery(Context);
            var todos = todoQuery.Execute();

            // Assert
            Assert.AreEqual(3, todos.Count());
        }

        [TestMethod]
        public void TestTodosSortedById()
        {
            SeedTodos(Context,3);

            var todoQuery = new AllTodosQuery(Context);
            var todos = todoQuery.Execute();

            Assert.AreEqual("Todo 3", todos.First().Description);
        }


        [TestMethod]
        public void TestTodosByCategory()
        {
            SeedTodos(Context, 3);

            var todoQuery = new TodosGroupByCategoryQuery(Context);
            var todos = todoQuery.Execute();

            Assert.AreEqual(3, todos.FirstOrDefault().Count);
        }

        //DbContextOptions<TodoContext> options;
        //TodoContext context;

        //[TestInitialize()]
        //public void Initialize() {
        //    // NB: Need to create a unique name for the database
        //    string databaseName = Guid.NewGuid().ToString();
        //    options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: databaseName).Options;
        //    context = new TodoContext(options);
        //}

        //[TestCleanup()]
        //public void Cleanup() {
        //    context.Database.EnsureDeleted();
        //}


    }
}
