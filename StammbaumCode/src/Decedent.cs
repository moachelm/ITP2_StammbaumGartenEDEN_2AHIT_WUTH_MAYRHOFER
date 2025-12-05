namespace FamilyTree.Models;

/// <summary>
/// The Decented and inheritancs from Person
/// </summary>
public class Decedent : Person
{
    private DateOnly _deathDate;
    private string _causeOfDeath;

    public DateOnly Birthday
    {
        get => _birthday;
        set => _birthday = value;
    }

        public DateOnly Deathdate
    {
        get => _deathDate;
        set => _deathDate = value;
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int DeathAge()
    {
        return (_deathDate.DayNumber - _birthday.DayNumber) / 365;
    }


}
