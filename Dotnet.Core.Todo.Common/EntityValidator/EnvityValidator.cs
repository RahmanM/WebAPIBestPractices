using Dotnet.Core.Todos.Common.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dotnet.Core.Todos.Common.EntityValidator
{
    public class EntityValidator : IEntityValidator
    {
        public BusinessException Validate<TEntity>(TEntity entityToValidate, bool throwException)
        {
            var validations =
                ValidateEntityWithBusinessException(entityToValidate);

            if (validations != null && throwException)
            {
                throw validations;
            }

            return validations;
        }

        public BusinessException Validate<TEntity>(TEntity entityToValidate, bool validateAllProperties, bool throwException)
        {
            var validations =
                ValidateEntityWithBusinessException(entityToValidate, validateAllProperties);

            if (validations != null && throwException)
            {
                throw validations;
            }

            return validations;

        }

        private BusinessException ValidateEntityWithBusinessException<TEntity>(
            TEntity entityToValidate,
            bool validateAllProperties = true)
        {
            BusinessException businessException = null;
            var validationContext = new ValidationContext(entityToValidate);
            var validationResults = new List<ValidationResult>();

            var isValid =
                Validator.TryValidateObject(entityToValidate, validationContext,
                                            validationResults, validateAllProperties);

            if (!isValid)
            {
                var messaageBuilder = new StringBuilder();
                foreach (var validationResult in validationResults)
                {
                    messaageBuilder.AppendLine(validationResult.ErrorMessage);
                }

                businessException = new BusinessException(messaageBuilder.ToString());
            }

            return businessException;
        }
    }
}
