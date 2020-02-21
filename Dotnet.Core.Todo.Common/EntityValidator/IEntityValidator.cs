using Dotnet.Core.Todos.Common.ExceptionTypes;

namespace Dotnet.Core.Todos.Common.EntityValidator
{
    public interface IEntityValidator
    {
        BusinessException Validate<TEntity>(TEntity entityToValidate, bool throwException = false);
        BusinessException Validate<TEntity>(TEntity entityToValidate, bool validateAllProperties, bool throwException);
    }
}
