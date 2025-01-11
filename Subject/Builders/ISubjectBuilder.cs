using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

public interface ISubjectBuilder
{
    ISubjectBuilder SetName(string name);

    ISubjectBuilder SetAuthor(IUser author);

    ISubjectBuilder SetFinalChallengePoints(int points);

    ISubjectBuilder SetMinPoints(int points);

    ISubject Build();
}