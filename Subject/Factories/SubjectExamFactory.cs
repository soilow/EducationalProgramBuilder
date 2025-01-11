using Itmo.ObjectOrientedProgramming.Lab2.Subject.Builders;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject.Factories;

public class SubjectExamFactory : ISubjectFactory
{
    public ISubjectBuilder Create()
    {
        return new ExamSubjectBuilder();
    }
}