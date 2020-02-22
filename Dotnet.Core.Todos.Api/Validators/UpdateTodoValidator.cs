using Dotnet.Core.Todos.Api.Models;
using FluentValidation;

namespace Dotnet.Core.Todos.Api.Validators
{
    /// <summary>
    /// Validations for update todo model
    /// </summary>
    public class UpdateTodoValidator : AbstractValidator<UpdateTodoViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public UpdateTodoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
