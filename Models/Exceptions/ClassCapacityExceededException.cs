using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Models.Exceptions;
public class ClassCapacityExceededException : Exception
{
    public ClassCapacityExceededException()
    {
    }

    public ClassCapacityExceededException(string? message) : base(message)
    {
    }

    public ClassCapacityExceededException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
