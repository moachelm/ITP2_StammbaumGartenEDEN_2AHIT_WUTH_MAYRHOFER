using System.IO.Compression;

namespace FamilyTree.Models;

/// <summary>
/// The Relative and inheritancs from Person
/// </summary>
public class Relative : Person
{
    public Relative(string firstname, string lastname) : base(firstname, lastname)
    {
      _livingstate = true;
    }

  /// <summary>
  /// A Method to set if the relative is living or not, true = alive, false = passed away
  /// </summary>
  /// <param name="b">the bool</param>
    public void SetLivingState(bool b)
    {
    _livingstate = b;    
    }

}
