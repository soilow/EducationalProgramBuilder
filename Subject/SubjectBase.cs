using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject;

public abstract class SubjectBase : ISubject
{
    public string Name { get; init; }

    public Guid Uid { get; init; }

    public Guid? ParentUid { get; init; }

    public IUser Author { get; init; }

    public int FinalChallengePoints { get; init; }

    public int MinPoints { get; private set; }

    public HashSet<Guid> Lections { get; private set; }

    public HashSet<Guid> LabWorks { get; init; }

    protected SubjectBase(
        string name,
        Guid? parentUid,
        IUser author,
        int finalChallengePoints,
        int minPoints,
        HashSet<Guid>? lections,
        HashSet<Guid>? labWorks)
    {
        Name = name;
        Uid = Guid.NewGuid();
        ParentUid = parentUid ?? null;
        Author = author;
        MinPoints = minPoints;
        FinalChallengePoints = finalChallengePoints;
        Lections = lections ?? new HashSet<Guid>();
        LabWorks = labWorks ?? new HashSet<Guid>();
    }

    public bool AddLection(IUser user, ILection lection)
    {
        if (!Author.Equals(user))
            return false;

        Lections.Add(lection.Uid);

        return true;
    }

    public bool DeleteLection(IUser user, ILection lection)
    {
        if (!Author.Equals(user))
            return false;

        Lections.Remove(lection.Uid);

        return true;
    }

    public bool AddLabWork(IUser user, ILabWork labWork)
    {
        if (!Author.Equals(user))
            return false;

        LabWorks.Add(labWork.Uid);

        return true;
    }

    public bool IsSubjectContainsLection(string lectionName)
    {
        return Lections.Contains(new Guid(lectionName));
    }

    public bool IsSubjectContainsLabWork(string labWorkName)
    {
        return LabWorks.Contains(new Guid(labWorkName));
    }

    public bool TryToChangeMinPoints(IUser user, int newMinPoints, IEducationProgram educationProgram)
    {
        if (!Author.Equals(user))
            return false;

        if (newMinPoints < 0 || newMinPoints > educationProgram.MaxPoints)
            return false;

        MinPoints = newMinPoints;

        return true;
    }

    public Result<ISubject> Clone(string cloneName)
    {
        if (cloneName == Name)
        {
            return new Result<ISubject>.Failure("Clone name and original name cannot be the same!");
        }

        ISubject clone = Clone(cloneName, Uid);

        return new Result<ISubject>.Success(clone);
    }

    protected abstract ISubject Clone(string cloneName, Guid parentUid);
}