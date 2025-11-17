using System.ComponentModel.DataAnnotations;

namespace FamilyTree.Models;

public class FamilyTree
{
    private Decedent _personfortree;

    enum Relationtypes
    {
        Son,
        Daughter,
        Mother,
        Father,

    }

    private Dictionary<Relative, Relationtypes> _relative = new Dictionary<Relative,Relationtypes>();

    public FamilyTree(Decedent person)
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
    Console.WriteLine("=== FAMILY TREE ===");
    Console.WriteLine($"Person: {_personfortree.Firstname} {_personfortree.Lastname}");
    Console.WriteLine();

    foreach (var entry in _relative)
    {
        Relative relative = entry.Key;
        Relationtypes type = entry.Value;

        Console.WriteLine($"{type}: {relative.Firstname} {relative.Lastname}");
    }

    Console.WriteLine("===================");
}


}
