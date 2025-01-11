using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LabWorks.Builder;

public class LabWorkBuilder : ILabWorkBuilder
{
    private string? _name;
    private string? _briefInfo;
    private string? _criteria;
    private int? _points;
    private IUser? _author;

    public ILabWorkBuilder SetName(string name)
    {
        _name = name;

        return this;
    }

    public ILabWorkBuilder SetBriefInfo(string briefInfo)
    {
        _briefInfo = briefInfo;

        return this;
    }

    public ILabWorkBuilder SetCriteria(string criteria)
    {
        _criteria = criteria;

        return this;
    }

    public ILabWorkBuilder SetPoints(int points)
    {
        _points = points;

        return this;
    }

    public ILabWorkBuilder SetAuthor(IUser author)
    {
        _author = author;

        return this;
    }

    public LabWork Build()
    {
        return new LabWork(
            _name ?? throw new ArgumentNullException(),
            _author ?? throw new ArgumentNullException(),
            _briefInfo ?? throw new ArgumentNullException(),
            _criteria ?? throw new ArgumentNullException(),
            _points ?? throw new ArgumentNullException());
    }
}