namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class User : IUser
{
    public string Name { get; init; }

    public Guid Uid { get; init; }

    public User(string name)
    {
        Name = name;
        Uid = Guid.NewGuid();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || GetType() != obj.GetType()) return false;

        var other = (IUser)obj;
        return Name == other.Name && Uid.Equals(other.Uid);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Uid);
    }
}