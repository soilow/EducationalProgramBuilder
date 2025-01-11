using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

public class ExamSubjectBuilder : SubjectBuilderBase
{
    protected override ISubject Build(
        string name,
        IUser user,
        int finalChallengePoints,
        int minPoints)
    {
        return new Exam(name, null, user, finalChallengePoints, minPoints, null, null);
    }
}