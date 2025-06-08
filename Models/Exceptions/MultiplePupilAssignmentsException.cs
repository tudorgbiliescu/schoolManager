
namespace SchoolManager.Models.Exceptions;
public class MultiplePupilAssignmentsException : Exception
{
    public MultiplePupilAssignmentsException()
    {
    }

    public MultiplePupilAssignmentsException(string? message) : base(message)
    {
    }

    public MultiplePupilAssignmentsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
