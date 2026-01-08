namespace FamilyTree.Models;

/// <summary>
/// A Relation pairs a Relative with a Relationtypes enum value
/// </summary>
public class Relation
{
    public Relative Person { get; set; }
    public Relationtypes Type { get; set; }

    public Relation(Relative person, Relationtypes type)
    {
        Person = person;
        Type = type;
    }
}