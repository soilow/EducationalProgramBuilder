using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Semesters;
using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Validators;
using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Subject;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;

public class EducationProgram : IEducationProgram
{
    public string Name { get; init; }

    public Guid Uid { get; init; }

    public IUser Author { get; init; }

    public int MaxPoints { get; init; }

    private readonly int _numberOfSemesters;
    private readonly SemesterVault _semesters;
    private readonly Repository<ILabWork> _labWorkRepository;

    public EducationProgram(
        string name,
        int numberOfSemesters,
        int maxPoints,
        IUser author,
        Repository<ILabWork> labWorkRepository)
    {
        Name = name;
        Uid = Guid.NewGuid();
        Author = author;
        MaxPoints = maxPoints;
        _numberOfSemesters = numberOfSemesters;
        _labWorkRepository = labWorkRepository;
        _semesters = new SemesterVault(numberOfSemesters);
    }

    public bool AddSubjectToSemester(ISubject subject, int semester)
    {
        if (!EducationProgramValidator.IsSemesterNumberValid(--semester, _numberOfSemesters)) return false;
        if (!EducationProgramValidator.IsSufficientPoints(subject, _labWorkRepository, MaxPoints)) return false;

        return _semesters.AddSubject(subject, semester);
    }
}