namespace FamilyTree.Models;

public class Relative : Person
{
    public Relative(string firstname, string lastname, string placeOfResidence, DateOnly birthday, string diseases) : base(firstname, lastname, placeOfResidence, birthday)
    {
      _livingstate = true;  
    }






}
