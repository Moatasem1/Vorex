using System.Reflection;

namespace Vorex.Domain.Common;

public class Enumeration : IEquatable<Enumeration>
{
    
    public int Id { get; private set; }
    public string Name { get; private set; }

    protected Enumeration()
    {
        Name = string.Empty;
    }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration) return false;
        return Equals(obj);
    }

    public bool Equals(Enumeration? other)
    {
        if (other == null) return false;
        return GetType() == other.GetType() && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(v => v.Id == value);
        if (matchingItem == null)
            throw new InvalidOperationException($"No matching value found for {value}");
        return matchingItem;
    }
}