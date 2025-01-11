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
using Xunit;

namespace Lab2.Tests;

public class Test
{
    [Fact]
    public void GetUserByName_ShouldReturnSuccess_WhenUserExists()
    {
        var usersRepository = new Repository<IUser>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));

        Result<IUser> info = usersRepository.GetByName("Mayatin");

        Assert.IsType<Result<IUser>.Success>(info);
    }

    [Fact]
    public void GetUserByName_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        var usersRepository = new Repository<IUser>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));

        Result<IUser> info = usersRepository.GetByName("Pratusevich");

        Assert.IsType<Result<IUser>.Failure>(info);
    }

    [Fact]
    public void AddEducationProgram_ShouldReturnSuccess_WhenProgramIsValid()
    {
        var usersRepository = new Repository<IUser>();
        var epRepository = new Repository<IEducationProgram>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
        var epBuilder = new EducationProgramBuilder();

        epRepository.Add(epBuilder
            .SetName("IS")
            .SetAuthor(currentUser)
            .SetNumberOfSemesters(4)
            .SetMaxPoints(100)
            .SetLinkToLabWorkRepository(labworkRepository)
            .Build());

        Assert.True(epRepository.GetByName("IS").Value.Author.Equals(usersRepository.GetByName("Mayatin").Value));
        Assert.True(epRepository.GetByName("IS").Value.MaxPoints == 100);
    }

    [Fact]
    public void AddSubjects_ShouldReturnSuccess_ForValidSubjects()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;

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

        Assert.True(subjectsRepository.GetByName("Algo").Value.MinPoints == 60);
        Assert.False(subjectsRepository.GetByName("Fizkultura").Value.MinPoints == 60);
    }

    [Fact]
    public void CloneAndAddSubject_ShouldReturnSuccess_WhenSubjectIsCloned()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
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

        Result<ISubject> clone = subjectsRepository.GetByName("Algo").Value.Clone("Discrete");
        subjectsRepository.Add(clone.Value);

        Assert.IsType<Result<ISubject>.Success>(subjectsRepository.GetByName("Discrete"));
        Assert.True(subjectsRepository.GetByName("Discrete").Value.MinPoints == 60);
    }

    [Fact]
    public void TryToChangeMinPoints_ShouldReturnTrue_WhenAuthorChangesPoints()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var epRepository = new Repository<IEducationProgram>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        Result<ISubject> clone = subjectsRepository.GetByName("Algo").Value.Clone("Discrete");
        subjectsRepository.Add(clone.Value);

        bool result = subjectsRepository.GetByName("Discrete").Value.TryToChangeMinPoints(currentUser, 60, epRepository.GetByName("IS").Value);

        Assert.True(result);
    }

    [Fact]
    public void TryToChangeMinPoints_ShouldReturnFalse_WhenPointsExceedProgramMax()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var epRepository = new Repository<IEducationProgram>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        Result<ISubject> clone = subjectsRepository.GetByName("Algo").Value.Clone("Discrete");
        subjectsRepository.Add(clone.Value);

        bool result = subjectsRepository.GetByName("Discrete").Value.TryToChangeMinPoints(currentUser, 239, epRepository.GetByName("IS").Value);

        Assert.False(result);
    }

    [Fact]
    public void TryToChangeMinPoints_ShouldReturnFalse_WhenNotAuthorAttemptsChange()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var epRepository = new Repository<IEducationProgram>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        Result<ISubject> clone = subjectsRepository.GetByName("Algo").Value.Clone("Discrete");
        subjectsRepository.Add(clone.Value);

        currentUser = usersRepository.GetByName("Stankevich").Value;
        bool result = subjectsRepository.GetByName("Discrete").Value.TryToChangeMinPoints(currentUser, 60, epRepository.GetByName("IS").Value);

        Assert.False(result);
    }

    [Fact]
    public void TryToChangeLectionBriefInfo_ShouldReturnTrue_WhenAuthorChangesBriefInfo()
    {
        var usersRepository = new Repository<IUser>();
        var lectionRepository = new Repository<ILection>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
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

        bool result = lectionRepository.GetByName("Algorithm Lection One").Value.TryToChangeBriefInfo(currentUser, "Papikyan");

        Assert.True(result);
    }

    [Fact]
    public void TryToChangeLectionBriefInfo_ShouldReturnFalse_WhenNonAuthorAttemptsToChange()
    {
        var usersRepository = new Repository<IUser>();
        var lectionRepository = new Repository<ILection>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
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

        currentUser = usersRepository.GetByName("Stankevich").Value;
        bool result = lectionRepository.GetByName("Algorithm Lection One").Value.TryToChangeBriefInfo(currentUser, "Papikyan");

        Assert.False(result);
    }

    [Fact]
    public void AddLectionToSubject_ShouldReturnTrue_WhenAuthorAddsLection()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
        var examFactory = new SubjectExamFactory();
        subjectsRepository.Add(examFactory.Create()
            .SetName("Algo")
            .SetAuthor(currentUser)
            .SetMinPoints(60)
            .SetFinalChallengePoints(20).Build());
        var algoLectionOne = new LectionBuilder();
        lectionRepository.Add(algoLectionOne
            .SetName("Algorithm Lection One")
            .SetAuthor(currentUser)
            .SetContent("Something useful")
            .SetBriefInfo("Macnev")
            .Build());

        bool result = subjectsRepository.GetByName("Algo").Value.AddLection(currentUser, lectionRepository.GetByName("Algorithm Lection One").Value);

        Assert.True(result);
    }

    [Fact]
    public void AddLectionToSubject_ShouldReturnFalse_WhenNonAuthorAttemptsToAdd()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
        var examFactory = new SubjectExamFactory();
        subjectsRepository.Add(examFactory.Create()
            .SetName("Algo")
            .SetAuthor(currentUser)
            .SetMinPoints(60)
            .SetFinalChallengePoints(20).Build());
        var algoLectionOne = new LectionBuilder();
        lectionRepository.Add(algoLectionOne
            .SetName("Algorithm Lection One")
            .SetAuthor(currentUser)
            .SetContent("Something useful")
            .SetBriefInfo("Macnev")
            .Build());

        currentUser = usersRepository.GetByName("Stankevich").Value;
        bool result = subjectsRepository.GetByName("Algo").Value.AddLection(currentUser, lectionRepository.GetByName("Algorithm Lection One").Value);

        Assert.False(result);
    }

    [Fact]
    public void GetLabWorkByName_ShouldReturnSuccess_WhenLabWorkExists()
    {
        var usersRepository = new Repository<IUser>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(30)
            .Build());
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(50)
            .Build());

        Result<ILabWork> result = labworkRepository.GetByName("Laba1");

        Assert.IsType<Result<ILabWork>.Success>(result);
    }

    [Fact]
    public void AddLabWorkToSubject_ShouldReturnTrue_WhenAuthorAddsLabWork()
    {
        var usersRepository = new Repository<IUser>();
        var subjectsRepository = new Repository<ISubject>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
        IUser currentUser = usersRepository.GetByName("Mayatin").Value;
        var examFactory = new SubjectExamFactory();
        subjectsRepository.Add(examFactory.Create()
            .SetName("Algo")
            .SetAuthor(currentUser)
            .SetMinPoints(60)
            .SetFinalChallengePoints(20).Build());
        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(30)
            .Build());
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(50)
            .Build());

        bool result1 = subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba1").Value);
        bool result2 = subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba2").Value);

        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void AddSubjectToSemester_ShouldReturnTrue_WhenSubjectIsValidAndAuthorAddsIt()
    {
        var usersRepository = new Repository<IUser>();
        var epRepository = new Repository<IEducationProgram>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(30)
            .Build());
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(50)
            .Build());
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba1").Value);
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba2").Value);

        bool result = epRepository.GetByName("IS").Value.AddSubjectToSemester(subjectsRepository.GetByName("Algo").Value, 1);

        Assert.True(result);
    }

    [Fact]
    public void AddSubjectToSemester_ShouldReturnFalse_WhenSubjectAlreadyExistsInSemester()
    {
        var usersRepository = new Repository<IUser>();
        var epRepository = new Repository<IEducationProgram>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(30)
            .Build());
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(50)
            .Build());
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba1").Value);
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba2").Value);

        bool addToRepo = epRepository.GetByName("IS").Value.AddSubjectToSemester(subjectsRepository.GetByName("Algo").Value, 1);
        bool cloneAdding = epRepository.GetByName("IS").Value.AddSubjectToSemester(subjectsRepository.GetByName("Algo").Value, 1);

        Assert.False(cloneAdding);
    }

    [Fact]
    public void AddSubjectToSemester_ShouldReturnFalse_WhenTotalLabPointsAndExamAreNotEqualTo100()
    {
        var usersRepository = new Repository<IUser>();
        var epRepository = new Repository<IEducationProgram>();
        var subjectsRepository = new Repository<ISubject>();
        var lectionRepository = new Repository<ILection>();
        var labworkRepository = new Repository<ILabWork>();
        usersRepository.Add(new User("Mayatin"));
        usersRepository.Add(new User("Stankevich"));
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
        var algoLabWorkOne = new LabWorkBuilder();
        var algoLabWorkTwo = new LabWorkBuilder();
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba1")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead")
            .SetCriteria("Some criteria")
            .SetPoints(30)
            .Build());
        labworkRepository.Add(algoLabWorkOne
            .SetName("Laba2")
            .SetAuthor(currentUser)
            .SetBriefInfo("Sortme is dead again")
            .SetCriteria("Some criteria")
            .SetPoints(10)
            .Build());
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba1").Value);
        subjectsRepository.GetByName("Algo").Value.AddLabWork(currentUser, labworkRepository.GetByName("Laba2").Value);

        bool result = epRepository.GetByName("IS").Value.AddSubjectToSemester(subjectsRepository.GetByName("Algo").Value, 1);

        Assert.False(result);
    }
}