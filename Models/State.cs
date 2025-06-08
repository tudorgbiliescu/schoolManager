using SchoolManager.Models.Db;

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
}
