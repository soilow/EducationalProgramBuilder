using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject;

public class Exam : SubjectBase
{
    public Exam(
        string name,
        Guid? parentUid,
        IUser author,
        int minPoints,
        int finalChallengePoints,
        HashSet<Guid>? lections,
        HashSet<Guid>? labWorks)
        : base(name, parentUid, author, minPoints, finalChallengePoints, lections, labWorks) { }

    protected override ISubject Clone(string cloneName, Guid parentUid)
    {
        var clonedLections = new HashSet<Guid>(Lections);
        var clonedLabWorks = new HashSet<Guid>(LabWorks);

        return new Exam(cloneName, parentUid, Author, FinalChallengePoints, MinPoints, clonedLections, clonedLabWorks);
    }
}