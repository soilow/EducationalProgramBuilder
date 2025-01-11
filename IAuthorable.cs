using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public interface IAuthorable
{
    IUser Author { get; init; }
}