using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Models.Exceptions;
public class UnassignedPupilException : Exception
{
    public UnassignedPupilException()
    {
    }

    public UnassignedPupilException(string? message) : base(message)
    {
    }

    public UnassignedPupilException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
