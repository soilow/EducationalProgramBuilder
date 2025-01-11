using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LabWorks;

public class LabWork : ILabWork
{
    public string Name { get; init; }

    public Guid Uid { get; init; }

    public IUser Author { get; init; }

    public Guid? ParentUid { get; private set; }

    public string BriefInfo { get; private set; }

    public string Criteria { get; private set; }

    public int Points { get; init; }

    public LabWork(string name, IUser author, string briefInfo, string criteria, int points)
    {
        Name = name;
        Uid = Guid.NewGuid();
        Author = author;
        Points = points;
        BriefInfo = briefInfo;
        Criteria = criteria;
    }

    private LabWork(string name, IUser author, string briefInfo, string criteria, int points, Guid parentUid)
        : this(name, author, briefInfo, criteria, points)
    {
        ParentUid = parentUid;
    }

    public Result<ILabWork> Clone(string cloneName)
    {
        if (cloneName == Name)
        {
            return new Result<ILabWork>.Failure("Clone name and original name cannot be the same!");
        }

        var clone = new LabWork(cloneName, Author, BriefInfo, Criteria, Points, Uid);

        return new Result<ILabWork>.Success(clone);
    }

    public bool TryToChangeBriefInfo(IUser user, string briefInfo)
    {
        if (!Author.Equals(user))
            return false;

        BriefInfo = briefInfo;

        return true;
    }

    public bool TryToChangeCriteria(IUser user, string criteria)
    {
        if (!Author.Equals(user))
            return false;

        Criteria = criteria;

        return true;
    }
}