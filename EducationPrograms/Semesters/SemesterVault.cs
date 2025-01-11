using Itmo.ObjectOrientedProgramming.Lab2.Subject;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Semesters;

public class SemesterVault
{
    private readonly Collection<ISet<Guid>> _semesters;

    public SemesterVault(int numberOfSemesters)
    {
        _semesters = new Collection<ISet<Guid>>(new List<ISet<Guid>>(numberOfSemesters));

        for (int i = 0; i < numberOfSemesters; ++i)
        {
            _semesters.Add(new HashSet<Guid>());
        }
    }

    public bool AddSubject(ISubject subject, int semester)
    {
        return _semesters[semester].Add(subject.Uid);
    }
}