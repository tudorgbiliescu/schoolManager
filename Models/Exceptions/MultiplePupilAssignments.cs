
namespace SchoolManager.Models.Exceptions;
public class MultiplePupilAssignments : Exception
{
    public MultiplePupilAssignments()
    {
    }

    public MultiplePupilAssignments(string? message) : base(message)
    {
    }

    public MultiplePupilAssignments(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
