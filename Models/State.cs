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

    public Pupil GetPupilById(int id)
    {
        var pupil = Pupils.FirstOrDefault(x => x.Id == id);
        if (pupil is null)
        {
            throw new PupilNotFoundException(String.Format("Pupil with id {0} does not exist.", id));
        }

        return pupil;
    }

    public void RemovePupilFromPreviousClass(Pupil pupil)
    {
        var previousPupilClass = Classes.First(x => x.ClassName == pupil.ClassName);
        previousPupilClass.AmountOfPupils -= 1;
    }
}
