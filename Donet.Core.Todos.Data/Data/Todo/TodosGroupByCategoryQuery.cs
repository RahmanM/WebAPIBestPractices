using Dotnet.Core.Todos.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Core.Todos.Data
{
    public class TodosGroupByCategoryQuery : IDtoQuery<TodoByCategory>
    {
        private TodoContext _Context;

        public TodosGroupByCategoryQuery(TodoContext context)
        {
            _Context = context;
        }

        public IEnumerable<TodoByCategory> Execute()
        {
           

            //var query = (
            //                from t in _Context.Todos
            //                group t by new { t.CategoryId, t.Category } into grp
            //                select new TodoByCategory
            //                {
            //                    CategoryId = grp.Key.CategoryId,
            //                    Count = grp.Count(),
            //                    Category = grp.Key.Category.Description
            //                }
            //            ).ToList();


            return ExecuteAsync().Result;
        }

        public async Task<IEnumerable<TodoByCategory>> ExecuteAsync()
        {
            var query = await _Context.Todos.GroupBy(g => g.CategoryId).Select(grp => new TodoByCategory
            {
                CategoryId = grp.Key,
                Count = grp.Count(),
            }).ToListAsync();

            return query;
        }
    }
}
