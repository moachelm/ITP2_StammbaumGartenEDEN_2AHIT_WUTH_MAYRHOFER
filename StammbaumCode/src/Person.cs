namespace FamilyTree.Models;

public class Person
{
    protected string _firstname;
    protected string _lastname;
    protected string _placeOfResidence;
    protected DateOnly _birthday;

    protected bool _livingstate; // true == alive, false == passed away

    protected Person(string firstname, string lastname)
    {
        _firstname = firstname;
        _lastname = lastname;
    }

    public string Firstname
    {
        get => _firstname;
        set => _firstname = value;
    }


    public string Lastname
    {
        get => _lastname;
        set => _lastname = value;
    }

    protected string PlaceOfResidence
    {
        get => _placeOfResidence;
        set => _placeOfResidence = value;
    }

    protected DateOnly Birthday
    {
        get => _birthday;
    }


    protected bool Livingstate
    {
        get => _livingstate;
        set => _livingstate = value;
    }

}