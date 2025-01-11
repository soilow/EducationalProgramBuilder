using Itmo.ObjectOrientedProgramming.Lab2.Services.Results;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class Repository<T> where T : IIdentifiable
{
    private readonly Collection<T> _repo;

    public Repository()
    {
        _repo = new Collection<T>(new List<T>());
    }

    public void Add(T item)
    {
         _repo.Add(item);
    }

    public Result<T> GetByName(string itemName)
    {
        try
        {
            T item = _repo.Single(x => x.Name == itemName);

            return new Result<T>.Success(item);
        }
        catch
        {
            return new Result<T>.Failure("Something went wrong");
        }
    }

    public Result<T> GetById(Guid uid)
    {
        try
        {
            T item = _repo.Single(x => x.Uid.Equals(uid));

            return new Result<T>.Success(item);
        }
        catch
        {
            return new Result<T>.Failure("Something went wrong");
        }
    }
}