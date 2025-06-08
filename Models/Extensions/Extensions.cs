using SchoolManager.Models.Db;

namespace SchoolManager.Models.Extensions;
public static class Extensions
{    public static bool IsInClass(this Pupil pupil)
    {
        return !string.IsNullOrWhiteSpace(pupil.ClassName);
    }

    public static bool CanAddPupil(this Class @class)
    {
        return @class.AmountOfPupils + 1 <= @class.MaxAmountOfPupils;
    }

    public static void AssignToClass(this Pupil pupil, Class @class)
    {
        pupil.ClassName = @class.ClassName;
    }
}
