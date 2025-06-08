using SchoolManager.Models.Db;
using SchoolManager.Models.Exceptions;

namespace SchoolManager.Models;

/**
 * This object reflects the fetched state from the Pupil Service and the Class Service.
 */
public class State
{
    public State()
    {
            
    }
    public State(State state)
    {
      Pupils = state.Pupils.Select(x => new Pupil(x)).ToList(); 
      Classes = state.Classes.Select(x => new Class(x)).ToList();
     
    }
    public List<Pupil> Pupils { get; set; } = new();

    public List<Class> Classes { get; set; } = new();

    public Pupil GetPupilForAssignment(Assignment assignemnt)
    {
        var pupil = Pupils.FirstOrDefault(x => x.Id == assignemnt.PupilId);
        if (pupil is null)
        {
            throw new PupilNotFoundException(String.Format("Pupil with id {0} does not exist.", assignemnt.PupilId));
        }

        return pupil;
    }

    public void RemovePupilFromPreviousClass(Pupil pupil)
    {
        var previousPupilClass = Classes.First(x => x.ClassName == pupil.ClassName);
        previousPupilClass.AmountOfPupils -= 1;
    }

    public Class GetClassForPupilAssignment(Assignment assignment)
    {
        var classEntity = Classes.FirstOrDefault(x => x.Id == assignment.ClassId);
        if (classEntity is null)
        {
            throw new ClassNotFoundException(String.Format("Class with id {0} does not exist.", assignment.ClassId));
        }
        return classEntity;
    }

    public void CheckIfAllPupilsAreAssigned()
    {
        foreach (var pupilItem in Pupils)
        {
            if (string.IsNullOrEmpty(pupilItem.ClassName))
            {
                throw new UnassignedPupilException(String.Format("Pupil with id {0} is not assigned to a class.", pupilItem.Id));
            }
        }
    }

    public void UpdateStudentOrderForClasses() {
        foreach (var classItem in Classes)
        {
            OrderStudentsAlphabetically(classItem);
        }
    }

    private void OrderStudentsAlphabetically(Class classItem)
    {
        var classPupils = Pupils.Where(x => x.ClassName == classItem.ClassName).OrderBy(x => x.Name).ToList();
        for (int i = 1; i <= classPupils.Count(); i++)
        {
            var pupil = classPupils[i - 1];
            pupil.FollowUpNumber = i;
        }
    }
}
