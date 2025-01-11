using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections;

public class Lection : ILection
{
    public string Name { get; init; }

    public Guid Uid { get; init; }

    public IUser Author { get; init; }

    public Guid? ParentUid { get; init; }

    public string BriefInfo { get; private set; }

    public string Content { get; private set; }

    public Lection(string name, IUser author, string briefInfo, string content)
    {
        Name = name;
        Uid = Guid.NewGuid();
        Author = author;
        BriefInfo = briefInfo;
        Content = content;
    }

    private Lection(string name, IUser author, string briefInfo, string content, Guid? parentUid)
    : this(name, author, briefInfo, content)
    {
        ParentUid = parentUid;
    }

    public Result<ILection> Clone(string cloneName)
    {
        if (cloneName == Name)
        {
            return new Result<ILection>.Failure("Clone name and original name cannot be the same!");
        }

        var clone = new Lection(cloneName, Author, BriefInfo, Content, Author.Uid);

        return new Result<ILection>.Success(clone);
    }

    public bool TryToChangeBriefInfo(IUser user, string newBriefInfo)
    {
        if (!Author.Equals(user))
            return false;

        BriefInfo = newBriefInfo;

        return true;
    }

    public bool TryToChangeContent(IUser user, string newContent)
    {
        if (!Author.Equals(user))
            return false;

        Content = newContent;

        return true;
    }
}