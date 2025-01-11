namespace Itmo.ObjectOrientedProgramming.Lab2;

public interface IIdentifiable
{
    string Name { get; init; }

    Guid Uid { get; init; }
}