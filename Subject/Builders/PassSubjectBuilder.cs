using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

public class PassSubjectBuilder : SubjectBuilderBase
{
    protected override ISubject Build(
        string name,
        IUser user,
        int finalChallengePoints,
        int minPoints)
    {
        return new Pass(name, null, user, finalChallengePoints, minPoints, null, null);
    }
}