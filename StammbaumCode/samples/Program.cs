namespace FamilyTreeApp;

using FamilyTree.Models;
using FamilyTree.Models;

public class Program
{
    static FamilyTree? tree;

    public static void Main(string[] args)
    {
        Console.WriteLine("=== Garten Eden – Stammbaum Programm ===");

        bool running = true;

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
                case "1":
                    CreateTree();
                    break;

                case "2":
                    AddPerson();
                    break;

                case "3":
                    EditPerson();
                    break;

                case "4":
                    RemovePerson();
                    break;

                case "5":
                    DisplayTree();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ungültige Eingabe!");
                    break;
            }
        }

        Console.WriteLine("Programm beendet.");
    }


    // ---------------------- Create Tree ----------------------

    static void CreateTree()
    {
        Console.WriteLine("\n--- Stammbaum erstellen ---");

        Console.Write("Vorname der Hauptperson (verstorben): ");
        string firstname = Console.ReadLine() ?? "";

        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";

        Console.Write("Wohnort: ");
        string place = Console.ReadLine() ?? "";

        Console.Write("Geburtsdatum (YYYY-MM-DD): ");
        DateOnly birth = DateOnly.Parse(Console.ReadLine()!);


        DateOnly death;
        while (true)
        {
            Console.Write("Sterbedatum (YYYY-MM-DD): ");
            death = DateOnly.Parse(Console.ReadLine()!);

            if (death >= birth)
            {
                break;
            }
            else
            {
                Console.WriteLine(" Das Sterbedatum muss NACH dem Geburtsdatum liegen! Bitte erneut eingeben.");
            }
        }


        var decedent = new Decedent(firstname, lastname, place, birth, death);
        tree = new FamilyTree(decedent);

        Console.WriteLine("Stammbaum wurde erstellt!");
    }


    // ---------------------- Add Person ----------------------

    static void AddPerson()
    {
        if (!CheckTree()) return;

        Console.WriteLine("\n--- Person hinzufügen ---");

        Console.Write("Vorname: ");
        string firstname = Console.ReadLine() ?? "";

        Console.Write("Nachname: ");
        string lastname = Console.ReadLine() ?? "";

        Console.Write("Wohnort: ");
        string place = Console.ReadLine() ?? "";

        Console.Write("Geburtsdatum (YYYY-MM-DD): ");
        DateOnly birth = DateOnly.Parse(Console.ReadLine()!);

        Console.WriteLine("\nVerwandtschafts-Typ:");
        Console.WriteLine("Son / Daughter / Father / Mother");
        Console.Write("Typ: ");
        string type = Console.ReadLine() ?? "";

        var r = new Relative(firstname, lastname, place, birth);

        tree!.AddRelative(r, type);
    }


    // ---------------------- Edit Person ----------------------

    static void EditPerson()
    {
        if (!CheckTree()) return;

        Console.WriteLine("\n--- Person bearbeiten ---");

        var list = tree!.GetAllRelatives().ToList();
        if (list.Count == 0)
        {
            Console.WriteLine("Keine Personen vorhanden.");
            return;
        }

        PrintRelativeList(list);

        Console.Write("Wähle Person (Nummer): ");
        int index = int.Parse(Console.ReadLine()!) - 1;

        if (index < 0 || index >= list.Count)
        {
            Console.WriteLine("Ungültige Auswahl!");
            return;
        }

        var relative = list[index];

        Console.Write("Neuer Vorname (leer lassen = gleich): ");
        string newFirst = Console.ReadLine()!;
        if (newFirst != "") relative.Firstname = newFirst;

        Console.Write("Neuer Nachname (leer lassen = gleich): ");
        string newLast = Console.ReadLine()!;
        if (newLast != "") relative.Lastname = newLast;

        Console.WriteLine("Person wurde aktualisiert.");
    }


    // ---------------------- Remove Person ----------------------

    static void RemovePerson()
    {
        if (!CheckTree()) return;

        Console.WriteLine("\n--- Person entfernen ---");

        var list = tree!.GetAllRelatives().ToList();
        if (list.Count == 0)
        {
            Console.WriteLine("Keine Personen vorhanden.");
            return;
        }

        PrintRelativeList(list);

        Console.Write("Wähle Person (Nummer): ");
        int index = int.Parse(Console.ReadLine()!) - 1;

        if (index < 0 || index >= list.Count)
        {
            Console.WriteLine("Ungültige Auswahl!");
            return;
        }

        var relative = list[index];

        tree.RemoveRelative(relative);
        Console.WriteLine("Person wurde entfernt.");
    }


    // ---------------------- Display Tree ----------------------

    static void DisplayTree()
    {
        if (!CheckTree()) return;
        tree!.Display();
    }


    // ---------------------- Helper ----------------------

    static bool CheckTree()
    {
        if (tree == null)
        {
            Console.WriteLine("Bitte zuerst einen Stammbaum erstellen!");
            return false;
        }
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