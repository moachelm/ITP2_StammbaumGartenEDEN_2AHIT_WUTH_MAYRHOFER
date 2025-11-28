using FamilyTree.Models;

namespace FamilyTreeApp;

public class Program
{
    static List<Tree> trees = new List<Tree>();
    static Tree? currentTree;

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Garten Eden - Stammbaum Programm ===");

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== HAUPTMENÜ =====");
            Console.WriteLine("1) Stammbaum auswählen");
            Console.WriteLine("2) Neuen Stammbaum erstellen");
            Console.WriteLine("3) Aktuellen Stammbaum bearbeiten");
            Console.WriteLine("4) Programm beenden");
            Console.Write("Auswahl: ");

            string? mainInput = Console.ReadLine();

            switch (mainInput)
            {
                case "1":
                    SelectTree();
                    break;
                case "2":
                    CreateTree();
                    break;
                case "3":
                    EditCurrentTree();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe!");
                    break;
            }
        }

        Console.WriteLine("Programm beendet.");
    }

    // ----------------------------
    // Stammbaum auswählen
    // ----------------------------
    static void SelectTree()
    {
        if (trees.Count == 0)
        {
            Console.WriteLine("Es existieren noch keine Stammbäume.");
            return;
        }

        Console.WriteLine("\n--- Stammbaum auswählen ---");
        for (int i = 0; i < trees.Count; i++)
        {
            var person = trees[i].PersonForTree;
            Console.WriteLine($"{i + 1}) {person.Firstname} {person.Lastname}");
        }

        Console.Write("Nummer: ");
        if (!int.TryParse(Console.ReadLine(), out int index))
        {
            Console.WriteLine("Ungültige Eingabe.");
            return;
        }

        index--;

        if (index < 0 || index >= trees.Count)
        {
            Console.WriteLine("Ungültige Auswahl.");
            return;
        }

        currentTree = trees[index];
        Console.WriteLine("Stammbaum ausgewählt!");
    }

    // ----------------------------
    // Stammbaum erstellen
    // ----------------------------
    static void CreateTree()
    {
        Console.WriteLine("\n--- Stammbaum erstellen ---");
        Console.Write("Vorname der Hauptperson (verstorben): ");
        string firstname = Console.ReadLine() ?? "";
        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";
        Console.Write("Wohnort: ");
        string place = Console.ReadLine() ?? "";

        DateOnly birth;
        while (true)
        {
            Console.Write("Geburtsdatum (YYYY-MM-DD): ");
            if (DateOnly.TryParse(Console.ReadLine(), out birth))
                break;
            Console.WriteLine("Ungültiges Datum!");
        }

        DateOnly death;
        while (true)
        {
            Console.Write("Sterbedatum (YYYY-MM-DD): ");
            if (DateOnly.TryParse(Console.ReadLine(), out death) && death >= birth)
                break;
            Console.WriteLine("Sterbedatum muss nach dem Geburtsdatum liegen!");
        }

        var decedent = new Decedent(firstname, lastname, place, birth, death);
        Tree tree = new Tree(decedent);

        trees.Add(tree);
        currentTree = tree;

        Console.WriteLine("Stammbaum erstellt und ausgewählt!");
    }

    // ----------------------------
    // Stammbaum bearbeiten
    // ----------------------------
    static void EditCurrentTree()
    {
        if (currentTree == null)
        {
            Console.WriteLine("Bitte zuerst einen Stammbaum auswählen oder erstellen!");
            return;
        }

        bool editing = true;
        while (editing)
        {
            Console.WriteLine("\n--- Stammbaum bearbeiten ---");
            Console.WriteLine("0) Hauptperson bearbeiten");
            Console.WriteLine("1) Person hinzufügen");
            Console.WriteLine("2) Person bearbeiten");
            Console.WriteLine("3) Person entfernen");
            Console.WriteLine("4) Stammbaum anzeigen");
            Console.WriteLine("5) Zurück zum Hauptmenü");
            Console.Write("Auswahl: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    EditMainPerson();
                    break;
                case "1":
                    AddPerson();
                    break;
                case "2":
                    EditPerson();
                    break;
                case "3":
                    RemovePerson();
                    break;
                case "4":
                    currentTree.Display();
                    break;
                case "5":
                    editing = false;
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe!");
                    break;
            }
        }
    }

    // ----------------------------
    // Hauptperson bearbeiten (Vorname, Nachname, Geburtstag, Sterbedatum)
    // ----------------------------
    static void EditMainPerson()
    {
        if (currentTree == null) return;

        var person = currentTree.PersonForTree;

        Console.WriteLine("\n--- Hauptperson bearbeiten ---");

        // Vorname & Nachname
        Console.Write($"Neuer Vorname (leer lassen = {person.Firstname}): ");
        string newFirst = Console.ReadLine()!;
        Console.Write($"Neuer Nachname (leer lassen = {person.Lastname}): ");
        string newLast = Console.ReadLine()!;

        if (!string.IsNullOrWhiteSpace(newFirst)) person.Firstname = newFirst;
        if (!string.IsNullOrWhiteSpace(newLast)) person.Lastname = newLast;

        // Geburtsdatum
        Console.Write($"Neues Geburtsdatum (YYYY-MM-DD, leer lassen = {person.Birthday}): ");
        string? birthInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(birthInput) && DateOnly.TryParse(birthInput, out var newBirth))
            person.Birthday = newBirth;

        // Sterbedatum
        Console.Write($"Neues Sterbedatum (YYYY-MM-DD, leer lassen = {person.Deathdate}): ");
        string? deathInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(deathInput) && DateOnly.TryParse(deathInput, out var newDeath))
        {
            if (newDeath >= person.Birthday)
                person.Deathdate = newDeath;
            else
                Console.WriteLine("Sterbedatum muss nach dem Geburtsdatum liegen!");
        }

        Console.WriteLine("Hauptperson aktualisiert!");
    }

    // ----------------------------
    // Person hinzufügen
    // ----------------------------
    static void AddPerson()
    {
        if (currentTree == null) return;

        Console.WriteLine("\n--- Person hinzufügen ---");
        Console.Write("Vorname: ");
        string firstname = Console.ReadLine() ?? "";
        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";

        Console.WriteLine("Verwandtschafts-Typ (Sohn / Tochter / Vater / Mutter):");
        Console.Write("Typ: ");
        string type = Console.ReadLine() ?? "";

        var relative = new Relative(firstname, lastname);
        currentTree.AddRelative(relative, type);
    }

    // ----------------------------
    // Person bearbeiten
    // ----------------------------
    static void EditPerson()
    {
        if (currentTree == null) return;

        var relatives = currentTree.GetAllRelatives();
        if (relatives.Count == 0)
        {
            Console.WriteLine("Keine Personen vorhanden.");
            return;
        }

        Console.WriteLine("\nPersonen:");
        for (int i = 0; i < relatives.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {relatives[i].Firstname} {relatives[i].Lastname}");
        }

        Console.Write("Nummer der Person: ");
        if (!int.TryParse(Console.ReadLine(), out int index)) return;
        index--;

        if (index < 0 || index >= relatives.Count)
        {
            Console.WriteLine("Ungültige Auswahl!");
            return;
        }

        var person = relatives[index];

        Console.Write("Neuer Vorname (leer lassen = gleich): ");
        string newFirst = Console.ReadLine()!;
        Console.Write("Neuer Nachname (leer lassen = gleich): ");
        string newLast = Console.ReadLine()!;

        if (!string.IsNullOrWhiteSpace(newFirst)) person.Firstname = newFirst;
        if (!string.IsNullOrWhiteSpace(newLast)) person.Lastname = newLast;

        Console.WriteLine("Person aktualisiert.");
    }

    // ----------------------------
    // Person entfernen
    // ----------------------------
    static void RemovePerson()
    {
        if (currentTree == null) return;

        var relatives = currentTree.GetAllRelatives();
        if (relatives.Count == 0)
        {
            Console.WriteLine("Keine Personen vorhanden.");
            return;
        }

        Console.WriteLine("\nPersonen:");
        for (int i = 0; i < relatives.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {relatives[i].Firstname} {relatives[i].Lastname}");
        }

        Console.Write("Nummer der Person: ");
        if (!int.TryParse(Console.ReadLine(), out int index)) return;
        index--;

        if (index < 0 || index >= relatives.Count)
        {
            Console.WriteLine("Ungültige Auswahl!");
            return;
        }

        var person = relatives[index];
        currentTree.RemoveRelative(person);
        Console.WriteLine("Person entfernt.");
    }
}
