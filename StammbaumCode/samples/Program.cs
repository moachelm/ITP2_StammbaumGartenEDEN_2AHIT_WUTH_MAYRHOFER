using FamilyTree.Models;

namespace FamilyTreeApp;

public class Program
{
    static Tree? tree;
    

    public static void Main(string[] args)
    {
        List<Tree> trees = new List<Tree>();

        Console.WriteLine("=== Garten Eden - Stammbaum Programm ===");
        bool running = true;
        bool mode = true;

        while (running)
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("1) Stammbaum erstellen");
            Console.WriteLine("2) Person hinzufügen");
            Console.WriteLine("3) Person bearbeiten");
            Console.WriteLine("4) Person entfernen");
            Console.WriteLine("5) Stammbaum anzeigen");
            Console.WriteLine("6) Programm beenden");
            Console.WriteLine("===============================");
            Console.Write("Auswahl: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateTree(); break;
                case "2": AddPerson(); break;
                case "3": EditPerson(); break;
                case "4": RemovePerson(); break;
                case "5": DisplayTree(); break;
                case "6": running = false; break;
                default: Console.WriteLine("Ungültige Eingabe!"); break;
            }
        }

        Console.WriteLine("Programm beendet.");
    }

    static void CreateTree()
    {
        Console.WriteLine("\n--- Stammbaum erstellen ---");
        Console.Write("Vorname der Hauptperson (verstorben): ");
        string firstname = Console.ReadLine() ?? "";
        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";
        Console.Write("Wohnort: ");
        string place = Console.ReadLine() ?? "";
        DateOnly birth = ReadDate("Geburtsdatum (YYYY-MM-DD): ");
        DateOnly death;

        while (true)
        {
            death = ReadDate("Sterbedatum (YYYY-MM-DD): ");
            if (death >= birth) break;
            Console.WriteLine("Sterbedatum muss nach dem Geburtsdatum liegen!");
        }

        var decedent = new Decedent(firstname, lastname, place, birth, death);
        tree = new Tree(decedent);
        Console.WriteLine("Stammbaum wurde erstellt!");
    }

    static void AddPerson()
    {
        if (!CheckTree()) return;

        Console.WriteLine("\n--- Person hinzufügen ---");
        Console.Write("Vorname: ");
        string firstname = Console.ReadLine() ?? "";
        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";

        Console.WriteLine("\nVerwandtschafts-Typ:");
        Console.WriteLine("Sohn / Tochter / Vater / Mutter");
        Console.Write("Typ: ");
        string type = Console.ReadLine() ?? "";

        var r = new Relative(firstname, lastname);
        tree!.AddRelative(r, type);
    }

    static void EditPerson()
    {
        if (!CheckTree()) return;

        var list = tree!.GetAllRelatives().ToList();
        if (list.Count == 0) { Console.WriteLine("Keine Personen vorhanden."); return; }

        PrintRelativeList(list);
        Console.Write("Wähle Person (Nummer): ");
        if (!int.TryParse(Console.ReadLine(),out int index)) { Console.WriteLine("Ungültige Eingabe."); return; }
        index--;

        if (index < 0 || index >= list.Count) { Console.WriteLine("Ungültige Auswahl!"); return; }

        var relative = list[index];
        Console.Write("Neuer Vorname (leer lassen = gleich): ");
        string newFirst = Console.ReadLine()!;
        Console.Write("Neuer Nachname (leer lassen = gleich): ");
        string newLast = Console.ReadLine()!;

        if (!string.IsNullOrWhiteSpace(newFirst)) relative.Firstname = newFirst;
        if (!string.IsNullOrWhiteSpace(newLast)) relative.Lastname = newLast;

        Console.WriteLine("Person wurde aktualisiert.");
    }

    static void RemovePerson()
    {
        if (!CheckTree()) return;

        var list = tree!.GetAllRelatives().ToList();
        if (list.Count == 0) { Console.WriteLine("Keine Personen vorhanden."); return; }

        PrintRelativeList(list);
        Console.Write("Wähle Person (Nummer): ");
        if (!int.TryParse(Console.ReadLine(), out int index)) { Console.WriteLine("Ungültige Eingabe."); return; }
        index--;

        if (index < 0 || index >= list.Count) { Console.WriteLine("Ungültige Auswahl!"); return; }

        var relative = list[index];
        tree.RemoveRelative(relative);
        Console.WriteLine("Person wurde entfernt.");
    }

    static void DisplayTree()
    {
        if (!CheckTree()) return;
        tree!.Display();
    }

    static DateOnly ReadDate(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            string? input = Console.ReadLine();
            if (TryParseDate(input!, out var date)) return date;
            Console.WriteLine("Ungültiges Datum!");
        }
    }

    static bool TryParseDate(string input, out DateOnly date)
    {
        try { date = DateOnly.Parse(input); return true; }
        catch { date = default; return false; }
    }

    static bool CheckTree()
    {
        if (tree == null) { Console.WriteLine("Bitte zuerst einen Stammbaum erstellen!"); return false; }
        return true;
    }

    static void PrintRelativeList(List<Relative> relatives)
    {
        Console.WriteLine("\nPersonen:");
        for (int i = 0; i < relatives.Count; i++)
        {
            var r = relatives[i];
            Console.WriteLine($"{i + 1}) {r.Firstname} {r.Lastname}");
        }
    }
}
