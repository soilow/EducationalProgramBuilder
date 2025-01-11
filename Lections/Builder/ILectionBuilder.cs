using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections.Builder;

public interface ILectionBuilder
{
    ILectionBuilder SetName(string name);

    ILectionBuilder SetBriefInfo(string briefInfo);

    ILectionBuilder SetContent(string content);

    ILectionBuilder SetAuthor(IUser author);

    Lection Build();
}