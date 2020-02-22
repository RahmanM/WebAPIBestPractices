using Dotnet.Core.Todos.Api.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Core.Todos.Api.Validators
{
    // #FluentValidaton

    /// <summary>
    /// Validations for create todo model
    /// </summary>
    public class CreateTodoValidator : AbstractValidator<TodoViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CreateTodoValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}
