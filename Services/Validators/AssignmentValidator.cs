using SchoolManager.Models;
using SchoolManager.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Services.Validators;
public class AssignmentValidator
{
    private Dictionary<int, int> _assignedPupilIds = new Dictionary<int, int>();

    public void ValidateAssignment(Assignment assignment)
    {
        if (_assignedPupilIds.ContainsKey(assignment.PupilId))
        {
            throw new MultiplePupilAssignmentsException(String.Format("Duplicate pupil IDs provided: {0}.", assignment.PupilId));
        }
        else
        {
            _assignedPupilIds[assignment.PupilId] = assignment.PupilId;
        }
    }
}
