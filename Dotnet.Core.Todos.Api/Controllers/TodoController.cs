using AutoMapper;
using Donet.Core.Todos.Data.Repositories.TodoRepository;
using Dotnet.Core.Todos.Api.Action_Filters;
using Dotnet.Core.Todos.Api.Models;
using Dotnet.Core.Todos.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IMapper _mapper;
        private readonly SieveProcessor _sieveProcessor;
        private readonly ITodoRepository _todoRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="sieveProcessor">Query, sorting, pating processor</param>
        /// <param name="todoRepository">Todo Repo helper</param>
        public TodoController(ILogger<TodoController> logger, IMapper mapper, SieveProcessor sieveProcessor, ITodoRepository todoRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
            _todoRepository = todoRepository;

            logger.LogInformation("TodoController-> DI completed successfuly!");
        }

        /// <summary>
        /// Get Cached Todo - Cached Todos. Todos are cached until a new todo is added, updated, or removed.
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
        [HttpGet("all-v2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<TodoViewModel> GetV2()
        {
            var todos = _todoRepository.GetAllList();
            var todosViewModel = _mapper.Map<List<TodoViewModel>>(todos);
            return todosViewModel;
        }

        /// <summary>
        /// Get a Todo with sorting, filtering and paging 
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// 
        /// CategoryId==2,Sort=Id,page=1,pageSize=3
        ///
        /// api/v2/Todo/GetWithFilerAndSortV2?Filters=CategoryId%3D%3D2&Sorts=Id&Page=1&PageSize=3
        ///
        /// </remarks>
        /// <returns>Returns a list of Todos</returns>
        /// <response code="201">Returns a list of todos</response>
        /// <response code="500">If there is any errors</response> 
        [MapToApiVersion("2")]
        [HttpGet("all-paged-filered-sorted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IEnumerable<TodoViewModel> GetV2([FromQuery] SieveModel queryStrings)
        {
            var todos = _todoRepository.GetAll(); // IQueryable that will go through processor for sorting, filtering etc

            var result = _sieveProcessor.Apply(queryStrings, todos).ToList();

            var todosViewModel = _mapper.Map<IEnumerable<TodoViewModel>>(result);

            return todosViewModel;
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
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Caching("TodoConroller_Get")]
        public IEnumerable<TodoViewModel> Get()
        {
            var todos = _todoRepository.GetAll().ToList();
            var todosViewModel = _mapper.Map<IEnumerable<TodoViewModel>>(todos);
            return todosViewModel;
        }

        /// <summary>
        /// Create new Todo
        /// </summary>
        /// <remarks>
        /// Sample:
        /// 
        /// {
        ///  "description": "updated",
        ///  "completed": true,
        ///  "categoryId": 2,
        ///  "active": true
        /// }
        /// </remarks>
        /// <param name="todoViewModel">Todo ViewModel</param>
        /// <returns>Updated todo with new ID created</returns>
        [HttpPost("create-todo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(TodoViewModel todoViewModel)
        {
            var todo = _mapper.Map<Todo>(todoViewModel);
            var id = await _todoRepository.CreateAsync(todo);
            todoViewModel.Id = id;
            return Created("/todo/" + id, todoViewModel);
        }

        /// <summary>
        /// Update existing Todo
        /// </summary>
        /// <remarks>
        /// Sample:
        /// 
        /// {
        ///  "id": 1,
        ///  "description": "updated",
        ///  "completed": true,
        ///  "categoryId": 2,
        ///  "active": true
        /// }
        /// </remarks>
        /// <param name="todoViewModel">Todo view Model</param>
        /// <returns>HTTP 200</returns>
        [HttpPut("update-todo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(UpdateTodoViewModel todoViewModel)
        {
            var todo = _mapper.Map<Todo>(todoViewModel);
            await _todoRepository.UpdateAsync(todo);
            return Ok();
        }

        /// <summary>
        /// Delelte a todo by Id
        /// </summary>
        /// <remarks>
        /// Sample:
        /// 
        /// Get api/v1/Todo
        /// </remarks>
        /// <param name="id">Todo Id</param>
        [ModelValidation]
        [HttpDelete( "delete-todo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([Required] int id)
        {
            await _todoRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
