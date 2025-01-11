// // using Itmo.ObjectOrientedProgramming.Lab2.BusinessLogic.Users;
// // using Itmo.ObjectOrientedProgramming.Lab2.Services.IdGenerators;
// // using Itmo.ObjectOrientedProgramming.Lab2.BusinessLogic.Users.Repository;
//
// // using Itmo.ObjectOrientedProgramming.Lab2.EducationProgram.Repository;
// using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
// using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Builder;
// using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
// using Itmo.ObjectOrientedProgramming.Lab2.LabWorks.Builder;
// using Itmo.ObjectOrientedProgramming.Lab2.Lections;
// using Itmo.ObjectOrientedProgramming.Lab2.Lections.Builder;
// using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
// using Itmo.ObjectOrientedProgramming.Lab2.Subject;
// using Itmo.ObjectOrientedProgramming.Lab2.Subject.Factories;
// using Itmo.ObjectOrientedProgramming.Lab2.Users;

using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms;
using Itmo.ObjectOrientedProgramming.Lab2.EducationPrograms.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.LabWorks;
using Itmo.ObjectOrientedProgramming.Lab2.LabWorks.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Lections;
using Itmo.ObjectOrientedProgramming.Lab2.Lections.Builder;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;
using Itmo.ObjectOrientedProgramming.Lab2.Subject;
using Itmo.ObjectOrientedProgramming.Lab2.Subject.Factories;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Testings
{
    public static void Main()
    {
        var usersRepository = new Repository<IUser>();
        var epRepository = new Repository<IEducationProgram>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        var labworkRepository = new Repository<ILabWork>();

        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Khartanovich"));

        IUser currentUser = usersRepository.GetByName("Mayatin").Value;

        var epBuilder = new EducationProgramBuilder();
        epRepository.Add(epBuilder
            .SetName("IS")
            .SetAuthor(currentUser)
            .SetNumberOfSemesters(4)
            .SetMaxPoints(100)
            .SetLinkToLabWorkRepository(labworkRepository)
            .Build());

        var examFactory = new SubjectExamFactory();
        var passFactory = new SubjectPassFactory();
        subjectsRepository.Add(examFactory.Create()
            .SetName("Algo")
            .SetAuthor(currentUser)
            .SetMinPoints(60)
            .SetFinalChallengePoints(20).Build());
        subjectsRepository.Add(passFactory.Create()
            .SetName("Fizkultura")
            .SetAuthor(currentUser)
            .SetMinPoints(100)
            .SetFinalChallengePoints(0).Build());

        Result<ISubject> some = subjectsRepository.GetByName("Algo").Value.Clone("Discrete");
        subjectsRepository.Add(some.Value);
        subjectsRepository.GetByName("Discrete").Value.TryToChangeMinPoints(currentUser, 45, epRepository.GetByName("IS").Value);

        currentUser = usersRepository.GetByName("Khartanovich").Value;

        var algoLectionOne = new LectionBuilder();
        var algoLectionTwo = new LectionBuilder();
        lectionRepository.Add(algoLectionOne
            .SetName("Algorithm Lection One")
            .SetAuthor(currentUser)
            .SetContent("Something useful")
            .SetBriefInfo("Macnev")
            .Build());
        lectionRepository.Add(algoLectionTwo
            .SetName("Algorithm Lection Two")
            .SetAuthor(currentUser)
            .SetContent("Something useful")
            .SetBriefInfo("Shindarev")
            .Build());

        currentUser = usersRepository.GetByName("Mayatin").Value;

        subjectsRepository.GetByName("Algo").Value.AddLection(currentUser, lectionRepository.GetByName("Algorithm Lection One").Value);

        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(40)
            .Build());

        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(40)
            .Build());

        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba1").Value);
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba2").Value);

        Result<ISubject> somii = subjectsRepository.GetByName("Algo").Value.Clone("fignya");
        subjectsRepository.Add(somii.Value);
        ISubject somii2 = subjectsRepository.GetByName("fignya").Value;
    }
}