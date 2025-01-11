using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Builder;

public class EducationProgramBuilder : IEducationProgramBuilder
{
    private string? _name;
    private IUser? _author;
    private int? _numberOfSemesters;
    private int? _maxPoints;
    private Repository<ILabWork>? _labWorkRepository;

    public IEducationProgramBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IEducationProgramBuilder SetAuthor(IUser author)
    {
        _author = author;
        return this;
    }

    public IEducationProgramBuilder SetNumberOfSemesters(int numberOfSemesters)
    {
        _numberOfSemesters = numberOfSemesters;
        return this;
    }

    public IEducationProgramBuilder SetMaxPoints(int maxPoints)
    {
        _maxPoints = maxPoints;
        return this;
    }

    public IEducationProgramBuilder SetLinkToLabWorkRepository(Repository<ILabWork> labWorkRepository)
    {
        _labWorkRepository = labWorkRepository;
        return this;
    }

    public EducationProgram Build()
    {
        return new EducationProgram(
            _name ?? throw new ArgumentException(),
            _numberOfSemesters ?? throw new ArgumentException(),
            _maxPoints ?? throw new ArgumentException(),
            _author ?? throw new ArgumentException(),
            _labWorkRepository ?? throw new ArgumentException());
    }
}