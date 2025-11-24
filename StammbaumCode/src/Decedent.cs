namespace FamilyTree.Models;

public class Decedent : Person
{
    private DateOnly _deathDate;
    private string _causeOfDeath;

    public DateOnly Birthday
    {
        get{return _birthday;}
    }

        public DateOnly Deathdate
    {
        get{return _deathDate;}
    }

    public Decedent(string firstname, string lastname, string placeOfResidence, DateOnly birthday, DateOnly Deathdate) : base(firstname, lastname)
    {

        _deathDate = Deathdate;
        _livingstate = false;
        _deathDate = Deathdate;
        _birthday = birthday;
    }

    public string CauseOfDeath
    {
        get => _causeOfDeath;
        set => _causeOfDeath = value;
    }

    public int DeathAge()
    {
        return (_deathDate.DayNumber - _birthday.DayNumber) / 365;
    }


}
