using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventService.BL.Exceptions
{
    public class EventManagerException : Exception
    {
        public EventManagerException(string? message) : base(message)
        {
        }

        public EventManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
