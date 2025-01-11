using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LabWorks.Builder;

public interface ILabWorkBuilder
{
    ILabWorkBuilder SetName(string name);

    ILabWorkBuilder SetBriefInfo(string briefInfo);

    ILabWorkBuilder SetCriteria(string criteria);

    ILabWorkBuilder SetPoints(int points);

    ILabWorkBuilder SetAuthor(IUser author);

    LabWork Build();
}