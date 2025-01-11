using Itmo.ObjectOrientedProgramming.Lab2.Subject;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

public interface IEducationProgram : IIdentifiable, IAuthorable
{
    int MaxPoints { get; init; }

    bool AddSubjectToSemester(ISubject subject, int semester);
}