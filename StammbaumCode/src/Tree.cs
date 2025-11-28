using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Models;

public class Tree
{
    private Decedent _personfortree;

    public Decedent PersonForTree
    {
        get => _personfortree;
    }

    enum Relationtypes
    {
        Sohn,
        Tochter,
        Mutter,
        Vater,
        Oma,
        Opa,
        Enkeltochter,
        Enkelsohn,
        Cousine,
        Cousin
    }

    private Dictionary<Relative, Relationtypes> _relative = new Dictionary<Relative, Relationtypes>();

    public Tree(Decedent person)
    {
        _personfortree = person;
    }

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

    public List<Relative> GetAllRelatives()
    {
        return _relative.Keys.ToList();
    }

    public bool RemoveRelative(Relative relative)
    {
        return _relative.Remove(relative);
    }
}
