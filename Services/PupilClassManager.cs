using SchoolManager.Models;
using SchoolManager.Models.Db;
using SchoolManager.Models.Diff;
using SchoolManager.Models.Exceptions;
using SchoolManager.Models.Extensions;
using SchoolManager.Services.Validators;

namespace SchoolManager.Services;

/**
 * This class manages the division of pupils into classes.
 *
 * It's your job to implement the UpdatePupilClassDivision method and the Diff method.
 */
public class PupilClassManager
{
    private AssignmentValidator _assignmentValidator = new AssignmentValidator();
    public State UpdatePupilClassDivision(State state, Request request)
    {
        var newState = new State(state);

        foreach (var assignment in request.Assignments)
        {

            _assignmentValidator.ValidateAssignment(assignment);    

            var pupilEntity = newState.GetPupilForAssignment(assignment);

            if (pupilEntity.IsInClass()) 
            {
                newState.RemovePupilFromPreviousClass(pupilEntity);
            }

            var classEntity = newState.GetClassForPupilAssignment(assignment);

            if (classEntity.CanAddPupil())
            {
                pupilEntity.AssignToClass(classEntity);
                classEntity.AmountOfPupils++;
            }
            else
            {
                throw new ClassCapacityExceededException(String.Format("Class with id {0} has too many pupils assigned.", classEntity.Id));            
            }

        }

        newState.CheckIfAllPupilsAreAssigned();

        newState.UpdateStudentOrderForClasses();

        return newState;
    }

    public (List<UpdatedPupil>, List<UpdatedClass>) Diff(State oldState, State newState)
    {
        List<UpdatedPupil> updatedPupils = GenerateUpdatedPupils(oldState, newState);
        List<UpdatedClass> updatedClasses = GenerateUpdatedClasses(oldState, newState);

        return (updatedPupils, updatedClasses);
    }

    private List<UpdatedClass> GenerateUpdatedClasses(State oldState, State newState)
    {
        var oldClasses = oldState.Classes;
        var newClasses = newState.Classes;
        var updatedClasses = new List<UpdatedClass>();

        foreach (var item in oldClasses)
        {

            var shouldAddUpdatedClass = TryGetUpdatedClass(newClasses, item, out var updatedClass);

            if (shouldAddUpdatedClass)
            {
                updatedClasses.Add(updatedClass);
            }
        }

        return updatedClasses;
    }

    private List<UpdatedPupil> GenerateUpdatedPupils(State oldState, State newState)
    {
        var oldPupils = oldState.Pupils;
        var newPupils = newState.Pupils;
        var updatedPupils = new List<UpdatedPupil>();

        foreach (var item in oldPupils)
        {

            var shouldAddUpdatedPupil = TryGetUpdatedPupil(newPupils, item, out var updatedPupil);

            if (shouldAddUpdatedPupil)
            {
                updatedPupils.Add(updatedPupil);
            }
        }

        return updatedPupils;
    }

    private bool TryGetUpdatedClass(List<Class> newClasses, Class item, out UpdatedClass updatedClass)
    {
        var shouldAddUpdatedClass = false;
        updatedClass = new UpdatedClass();
        var newClass = newClasses.First(x => x.Id == item.Id);
        if (newClass.AmountOfPupils != item.AmountOfPupils)
        {
            shouldAddUpdatedClass = true;
            updatedClass.ClassId = item.Id;
            updatedClass.AmountOfPupils = newClass.AmountOfPupils;
        }

        return shouldAddUpdatedClass;
    }

    private bool TryGetUpdatedPupil(List<Pupil> newPupils, Pupil item, out UpdatedPupil updatedPupil)
    {
        var shouldAddUpdatedPupil = false;
        updatedPupil = new UpdatedPupil();
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

        return shouldAddUpdatedPupil;
    }
}
