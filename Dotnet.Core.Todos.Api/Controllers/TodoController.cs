using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet.Core.Todos.Api.Action_Filters;
using Dotnet.Core.Todos.Data;
using Dotnet.Core.Todos.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dotnet.Core.Todos.Api.Controllers
{
    /// <summary>
    /// Todo API Controller
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        
        private readonly ILogger<TodoController> _logger;
        private readonly TodoContext _context;

        public TodoController(ILogger<TodoController> logger, TodoContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get a Todo - Asynchronous
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get api/v2/Todo
        ///
        /// </remarks>
        /// <returns>Returns a list of Todos</returns>
        /// <response code="201">Returns a list of todos</response>
        /// <response code="500">If there is any errors</response> 
        [MapToApiVersion("2")]
        [HttpGet("GetV2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<TodoDto>> GetV2()
        {
            var query = new AllTodosQuery(_context);
            var todos = await query.ExecuteAsync();
            return todos;
        }

        /// <summary>
        /// Get a Todo - synchronous
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get api/v1/Todo
        ///
        /// </remarks>
        /// <returns>Returns a list of Todos</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If there is any errors</response>
        [MapToApiVersion("1")]
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<TodoDto> Get()
        {
            var query = new AllTodosQuery(_context);
            var todos = query.ExecuteAsync().Result;
            return todos;
        }

        [ModelValidation]
        [HttpPost]
        public async Task Post(TodoDto todo)
        {
           
        }
    }
}
