using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Builder;

public interface IEducationProgramBuilder
{
    IEducationProgramBuilder SetName(string name);

    IEducationProgramBuilder SetAuthor(IUser author);

    IEducationProgramBuilder SetNumberOfSemesters(int numberOfSemesters);

    IEducationProgramBuilder SetMaxPoints(int maxPoints);

    IEducationProgramBuilder SetLinkToLabWorkRepository(Repository<ILabWork> labWorkRepository);

    EducationProgram Build();
}