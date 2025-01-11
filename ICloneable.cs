using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public interface ICloneable<T>
{
    Guid? ParentUid { get; }

    Result<T> Clone(string cloneName);
}