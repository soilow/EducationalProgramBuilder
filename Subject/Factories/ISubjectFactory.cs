using Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Factories;

public interface ISubjectFactory
{
    ISubjectBuilder Create();
}