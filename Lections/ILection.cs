using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections;

public interface ILection : IIdentifiable, IAuthorable, ICloneable<ILection>
{
    string BriefInfo { get; }

    string Content { get; }

    bool TryToChangeBriefInfo(IUser user, string newBriefInfo);

    bool TryToChangeContent(IUser user, string newContent);
}