using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Models.Exceptions;
public class ClassNotFoundException : Exception
{
    public ClassNotFoundException()
    {
    }

    public ClassNotFoundException(string? message) : base(message)
    {
    }

    public ClassNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
