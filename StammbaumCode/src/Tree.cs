using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Models;

/// <summary>
/// The class Tree
/// </summary>
public class Tree
{
    private Decedent _personfortree;

    public Decedent PersonForTree
    {
        get => _personfortree;
    }

 

    private Dictionary<Relative, Relationtypes> _relative = new Dictionary<Relative, Relationtypes>();

    public Tree(Decedent person)
    {
        _personfortree = person;
    }

    /// <summary>
    /// A Mehtod to Add a Relative to the Tree
    /// </summary>
    /// <param name="relative">A Relative</param>
    /// <param name="type">Type of Relationsship</param>
    public void AddRelative(Relative relative, string type)
    {
        if (Enum.TryParse(typeof(Relationtypes), type, true, out var result))
        {
            Relationtypes relationType = (Relationtypes)result;

            _relative.Add(relative, relationType);
        }
        else
        {
            Console.WriteLine($"Invalid relation type: {type}");
        }
    }



    public void Display()
    {
        Console.WriteLine("=== Stammbaum ===");
        Console.WriteLine($"Person: {_personfortree.Firstname} {_personfortree.Lastname} {_personfortree.DeathAge()} ✞");
        Console.WriteLine();

        foreach (var entry in _relative)
        {
            Relative relative = entry.Key;
            Relationtypes type = entry.Value;

            Console.WriteLine($"{type}: {relative.Firstname} {relative.Lastname}");
        }

        Console.WriteLine("===================");
    }

    /// <summary>
    /// A Method to return all relatives
    /// </summary>
    /// <returns>A list of relatives</returns>
    public List<Relative> GetAllRelatives()
    {
        return _relative.Keys.ToList();
    }

    /// <summary>
    /// A Method to remove a relative from the list
    /// </summary>
    /// <param name="relative">the relative person</param>
    /// <returns>the list without the removed relative</returns>
    public bool RemoveRelative(Relative relative)
    {
        return _relative.Remove(relative);
    }
}
