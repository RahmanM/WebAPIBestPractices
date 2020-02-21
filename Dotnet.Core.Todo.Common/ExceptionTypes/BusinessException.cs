using System;
using System.Collections.Generic;
using System.Text;


namespace Dotnet.Core.Todos.Common.ExceptionTypes
{
    [Serializable]
    public class BusinessException : GeneralException
    {

        public BusinessException(string message) : base(message)
        {
        }
    }
}