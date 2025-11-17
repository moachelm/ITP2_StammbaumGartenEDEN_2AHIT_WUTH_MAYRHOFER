namespace FamilyTreeApp;
using FamilyTree.Models;
class Program
{
    static void Main(string[] args)
    {

        FamilyTree tree = new FamilyTree(new Decedent("erwin","Mamut","eggersdorf",new DateOnly(1990,12,5),new DateOnly(2022,1,16)));
        tree.AddRelative(new Relative("mama","mamut","eggelsdorf",new DateOnly(2002,2,2)),"Mother");
        tree.AddRelative(new Relative("papa","mamut","eggelsdorf",new DateOnly(2002,2,2)),"Father");
        tree.AddRelative(new Relative("sabine","mamut","eggelsdorf",new DateOnly(2002,2,2)),"Daughter");
        tree.AddRelative(new Relative("bastian","mamut","eggelsdorf",new DateOnly(2002,2,2)),"Son");
        tree.AddRelative(new Relative("konrad","mamut","eggelsdorf",new DateOnly(2002,2,2)),"Son");


        tree.Display();
    }
}
