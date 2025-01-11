using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Lections.Builder;

public class LectionBuilder : ILectionBuilder
{
    private string? _name;
    private string? _briefInfo;
    private string? _content;
    private IUser? _author;

    public ILectionBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public ILectionBuilder SetBriefInfo(string briefInfo)
    {
        _briefInfo = briefInfo;
        return this;
    }

    public ILectionBuilder SetContent(string content)
    {
        _content = content;
        return this;
    }

    public ILectionBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public Lection Build()
    {
        return new Lection(
            _name ?? throw new ArgumentException(),
            _author ?? throw new ArgumentException(),
            _briefInfo ?? throw new ArgumentException(),
            _content ?? throw new ArgumentException());
    }
}