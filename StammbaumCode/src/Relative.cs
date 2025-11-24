using System.IO.Compression;

namespace FamilyTree.Models;

public class Relative : Person
{
    public Relative(string firstname, string lastname) : base(firstname, lastname)
    {
      
    }

    public void SetLivingState(bool b)
    {
    _livingstate = b;    
    }




}
