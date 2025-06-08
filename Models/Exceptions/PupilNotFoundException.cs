
namespace SchoolManager.Models.Exceptions;
public class PupilNotFoundException : Exception
{

    public PupilNotFoundException()
    {
    }

    public PupilNotFoundException(string? message) : base(message)
    {
    }

    public PupilNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
