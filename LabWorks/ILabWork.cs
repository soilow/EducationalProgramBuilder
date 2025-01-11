using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LabWorks;

public interface ILabWork : IIdentifiable, IAuthorable, ICloneable<ILabWork>
{
    int Points { get; init; }

    string BriefInfo { get; }

    string Criteria { get; }

    bool TryToChangeBriefInfo(IUser user, string briefInfo);

    bool TryToChangeCriteria(IUser user, string criteria);
}