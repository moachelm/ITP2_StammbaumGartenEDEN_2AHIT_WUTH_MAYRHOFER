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

    private List<Relation> _relations = new List<Relation>();

    /// <summary>
    /// Constructor of Tree
    /// </summary>
    /// <param name="person">the decendent person</param>
    public Tree(Decedent person)
    {
        _personfortree = person;
    }

    /// <summary>
    /// A Method to Add a Relative (possibly multiple relation types can exist for the same person)
    /// </summary>
    /// <param name="relative">A Relative</param>
    /// <param name="type">Type of Relationsship as string</param>
    public void AddRelative(Relative relative, string type)
    {
        if (Enum.TryParse(typeof(Relationtypes), type, true, out var result))
        {
            Relationtypes relationType = (Relationtypes)result;

            // prevent adding exact duplicate (same person + same type)
            if (!_relations.Any(r => ReferenceEquals(r.Person, relative) && r.Type == relationType))
            {
                _relations.Add(new Relation(relative, relationType));
            }
        }
        else
        {
            Console.WriteLine($"Invalid relation type: {type}");
        }
    }

    /// <summary>
    /// Method to display the tree
    /// </summary>
    public void Display()
    {
        Console.WriteLine("=== Stammbaum ===");
        Console.WriteLine($"Person: {_personfortree.Firstname} {_personfortree.Lastname} {_personfortree.DeathAge()} ✞");
        Console.WriteLine();

        foreach (var entry in _relations)
        {
            Relative relative = entry.Person;
            Relationtypes type = entry.Type;

            Console.WriteLine($"{type}: {relative.Firstname} {relative.Lastname}");
        }

        Console.WriteLine("===================");
    }

    /// <summary>
    /// A Method to return all relations
    /// </summary>
    /// <returns>A list of relations</returns>
    public List<Relation> GetAllRelations()
    {
        return _relations.ToList();
    }

    /// <summary>
    /// Backwards-compatible method: return a list of relatives (without relation types)
    /// </summary>
    public List<Relative> GetAllRelatives()
    {
        return _relations.Select(r => r.Person).ToList();
    }

    /// <summary>
    /// Remove a relation by index (as shown in UI list)
    /// </summary>
    /// <param name="index">index in the relation list</param>
    public bool RemoveRelationAt(int index)
    {
        if (index < 0 || index >= _relations.Count)
            return false;

        _relations.RemoveAt(index);
        return true;
    }
}
