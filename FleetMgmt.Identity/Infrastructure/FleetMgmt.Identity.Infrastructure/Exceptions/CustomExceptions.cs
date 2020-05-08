using System;

namespace FleetMgmt.Identity.Infrastructure.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            Message = message;
        }

        public new string Message { get; set; }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
            Message = message;
        }

        public new string Message { get; set; }
    }
}
