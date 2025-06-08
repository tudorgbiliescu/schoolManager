using SchoolManager.Models;
using SchoolManager.Models.Db;
using SchoolManager.Models.Diff;

namespace SchoolManager.Services;

/**
 * This class manages the division of pupils into classes.
 *
 * It's your job to implement the UpdatePupilClassDivision method and the Diff method.
 */
public class PupilClassManager
{
    public State UpdatePupilClassDivision(State state, Request request)
    {
        var newState = new State(state);
        
        foreach (var assignment in request.Assignments)
        {
            
            var pupilEntity = newState.Pupils.First(x => x.Id == assignment.PupilId);

            if (!string.IsNullOrWhiteSpace(pupilEntity.ClassName)) 
            {
                var previousPupilClass = newState.Classes.First(x=> x.ClassName  == pupilEntity.ClassName);
                previousPupilClass.AmountOfPupils -= 1;
            }
            var classEntity = newState.Classes.First(x => x.Id == assignment.ClassId);

            if (classEntity.AmountOfPupils + 1 <= classEntity.MaxAmountOfPupils)
            {
                pupilEntity.ClassName = classEntity.ClassName;
                classEntity.AmountOfPupils += 1;
            }

        }

        foreach (var classItem in newState.Classes)
        {
            var classPupils = newState.Pupils.Where(x => x.ClassName == classItem.ClassName).OrderBy(x => x.Name).ToList();
            for (int i = 1; i <= classPupils.Count(); i++)
            {
                var pupil = classPupils[i-1];
                pupil.FollowUpNumber = i;
            }
        }
        return newState;
    }

    public (List<UpdatedPupil>, List<UpdatedClass>) Diff(State oldState, State newState)
    {
        var oldPupils = oldState.Pupils;
        var newPupils = newState.Pupils;
        var updatedPupils = new List<UpdatedPupil>();

        foreach (var item in oldPupils)
        {
            var shouldAddUpdatedPupil = false;
            var updatedPupil = new UpdatedPupil();
            var newPupil = newPupils.First(x => x.Id == item.Id);
            if (newPupil.ClassName != item.ClassName)
            {
                shouldAddUpdatedPupil = true;
                updatedPupil.PupilId = item.Id;
                updatedPupil.ClassName = newPupil.ClassName;
            }

            if (newPupil.FollowUpNumber != item.FollowUpNumber)
            {
                shouldAddUpdatedPupil = true;
                updatedPupil.FollowUpNumber = newPupil.FollowUpNumber;
            }

            if (shouldAddUpdatedPupil)
            {
                updatedPupils.Add(updatedPupil);
            }
        }

        var oldClasses = oldState.Classes;
        var newClasses = newState.Classes;
        var updatedClasses = new List<UpdatedClass>();

        foreach (var item in oldClasses)
        {
            var shouldAddUpdatedClass = false;
            var updatedClass = new UpdatedClass();
            var newClass = newClasses.First(x => x.Id == item.Id);
            if (newClass.AmountOfPupils != item.AmountOfPupils)
            {
                shouldAddUpdatedClass = true;
                updatedClass.ClassId = item.Id;
                updatedClass.AmountOfPupils = newClass.AmountOfPupils;
            }
                        
            if (shouldAddUpdatedClass)
            {
                updatedClasses.Add(updatedClass);
            }
        }

        return (updatedPupils, updatedClasses);
    }
}
