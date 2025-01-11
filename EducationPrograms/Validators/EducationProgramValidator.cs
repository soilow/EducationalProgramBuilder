using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Subject;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Validators;

public sealed class EducationProgramValidator
{
    private EducationProgramValidator() { }

    public static bool IsSemesterNumberValid(int semester, int numberOfSemesters)
    {
        return semester >= 0 && semester < numberOfSemesters;
    }

    public static bool IsSufficientPoints(
        ISubject subject,
        Repository<ILabWork> labWorkRepository,
        int maxPoints)
    {
        var labs = subject.LabWorks
            .Select(id => labWorkRepository.GetById(id).Value)
            .ToList();

        int totalPoints = labs.Sum(lab => lab.Points);

        return totalPoints + subject.FinalChallengePoints == 100;
    }
}