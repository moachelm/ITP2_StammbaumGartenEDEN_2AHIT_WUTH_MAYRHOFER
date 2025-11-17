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

    private Dictionary<Relationtypes, Relative> _relative = new Dictionary<Relationtypes, Relative>();

    public FamilyTree(Decedent person)
    {
        _personfortree = person;
    }

    public void AddRelative(Relative elative, string type)
    {

        if (Enum.TryParse(typeof(Relationtypes), type, true, out var result))
        {
            Relationtypes relationType = (Relationtypes)result;

            _relative.Add(relationType, elative);
        }
        else
        {
            Console.WriteLine($"Invalid relation type: {type}");
        }
    }


    public void Dissplay()
    {
        

        
    }

}


