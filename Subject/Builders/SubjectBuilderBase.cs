using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

public abstract class SubjectBuilderBase : ISubjectBuilder
{
    private string? _name;
    private IUser? _author;
    private int? _finalChallengePoints;
    private int? _minPoints;

    public ISubjectBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ISubjectBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public ISubjectBuilder SetFinalChallengePoints(int points)
    {
        _finalChallengePoints = points;
        return this;
    }

    public ISubjectBuilder SetMinPoints(int points)
    {
        _minPoints = points;
        return this;
    }

    public ISubject Build()
    {
        return Build(
            _name ?? throw new ArgumentNullException(),
            _author ?? throw new ArgumentNullException(),
            _finalChallengePoints ?? throw new ArgumentNullException(),
            _minPoints ?? throw new ArgumentNullException());
    }

    protected abstract ISubject Build(
        string name,
        IUser user,
        int finalChallengePoints,
        int minPoints);
}