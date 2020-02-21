using System;
using System.Runtime.Serialization;

namespace Dotnet.Core.Todos.Common.ExceptionTypes
{
    [Serializable]
    public class GeneralException : Exception
    {
        public GeneralException()
        {
            this.TicketNumber = GetTicketNumber();
        }

        private string GetTicketNumber()
        {
            return DateTime.Now.ToString("yyyyMMdd-hhmmss");
        }

        public string TicketNumber { get; set; }

        public GeneralException(string message) : base(message)
        {
            this.TicketNumber = GetTicketNumber();
        }

        public GeneralException(string message, Exception exception)
            : base(message, exception)
        {
            this.TicketNumber = GetTicketNumber();
        }

        protected GeneralException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.TicketNumber = info.GetString("TicketNumber");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("TicketNumber", this.TicketNumber);
        }
    }
}
