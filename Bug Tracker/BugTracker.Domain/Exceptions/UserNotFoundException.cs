using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Email;
        public UserNotFoundException(string email)
        {
            Email = email;
        }

        public UserNotFoundException(string? message, string email) : base(message)
        {
            Email = email;
        }

        public UserNotFoundException(string? message, Exception? innerException, string email) : base(message, innerException)
        {
            Email = email;
        }

    }
}
