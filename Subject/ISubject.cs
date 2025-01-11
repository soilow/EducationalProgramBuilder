using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Subject;

public interface ISubject : IIdentifiable, IAuthorable, ICloneable<ISubject>
{
    int MinPoints { get; }

    int FinalChallengePoints { get; }

    HashSet<Guid> Lections { get; }

    HashSet<Guid> LabWorks { get; }

    bool AddLection(IUser user, ILection lection);

    bool DeleteLection(IUser user, ILection lection);

    bool AddLabWork(IUser user, ILabWork labWork);

    bool IsSubjectContainsLection(string lectionName);

    bool IsSubjectContainsLabWork(string labWorkName);

    bool TryToChangeMinPoints(IUser user, int newMinPoints, IEducationProgram educationProgram);
}