namespace FamilyTree.Models;

public class Person
{
    protected string _firstname;
    protected string _lastname;
    protected string _placeOfResidence;
    protected DateOnly _birthday;
    protected string _infos;
    protected string _diseases;
    protected bool _livingstate; // true == alive, false == passed away

    protected Person(string firstname, string lastname, string placeOfResidence, DateOnly birthday)
    {
      _firstname = firstname;
      _lastname = lastname;
      _placeOfResidence = placeOfResidence;
      _birthday = birthday; 
    }

    protected string Firstname
    {
        get => _firstname;
        set => _firstname = value;
    }


    protected string Lastname
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

    protected string Infos
    {
        get => _infos;
        set => _infos = value;
    }

    protected string Diseases
    {
        get => _diseases;
        set => _diseases = value;
    }

} 