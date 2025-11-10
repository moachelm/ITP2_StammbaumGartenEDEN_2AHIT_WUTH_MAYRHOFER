namespace FamilyTree.Models;

public class Decedent : Person
{
     private string _firstname;
    private string _middleName;
    private string _lastname;
    private string _placeOfResidence;
    private DateOnly _birthday;
    private DateOnly _deathDate;
    private string _infos;
    private string _diseases;

    private bool _livingstate; // true == alive, false == passed away

    public Person(string firstname, string middlename, string lastname, string placeOfResidence, DateOnly deathDate, DateOnly birthday, bool livingstate, string infos, string diseases)
    {
      _firstname = firstname;
      _middleName = middlename;
      _lastname = lastname;
      _deathAge = deathAge;
      _placeOfResidence = placeOfResidence;
      _deathDate = deathDate;
      _birthday = birthday;
      _livingstate = livingstate;  
      _infos = infos;
      _diseases = diseases;
    }

    public string Firstname
    {
        get => _firstname;
        set => _firstname = value;
    }

    public string Middlename
    {
        get => _middlename;
        set => _middleName = value;
    }

    public string Lastname
    {
        get => _lastname;
        set => _lastname = value;
    }

    public string PlaceOfResidence
    {
        get => _placeOfResidence;
        set => _placeOfResidence = value;
    }

    public DateOnly Birthday
    {
        get => _birthday;
    }

    public DateOnly DeathDate
    {
        get => _deathDate;
    }

    public bool Livingstate
    {
        get => _livingstate;
        set => _livingstate = value;
    }

    if(ConnectionState == )

    public string Infos
    {
        get => _infos;
        set => _infos = value;
    }

    public string Diseases
    {
        get => _diseases;
        set => _diseases = value;
    }

    public int DeathAge(DateOnly birthday, DateOnly deathDate)
    {
    int deathAge = deathDate.Year - birthday.Year;

    if (deathDate < birthday.AddYears(age)) // Ob Person in diesem jahr schon Geburtstag hatte
        age--;

    return deathAge;
    }
}