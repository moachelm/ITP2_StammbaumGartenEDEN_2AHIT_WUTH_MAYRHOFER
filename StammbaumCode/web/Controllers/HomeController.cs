using Microsoft.AspNetCore.Mvc;
using FamilyTree.Models;

namespace FamilyTreeWeb.Controllers;

public class HomeController : Controller
{
    private readonly List<Tree> _trees;

    public HomeController(List<Tree> trees)
    {
        _trees = trees;
    }

    public IActionResult Index()
    {
        return View(_trees);
    }

    [HttpGet]
    public IActionResult CreateTree()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateTree(string firstname, string lastname, string place, string birthday, string deathdate)
    {
        try
        {
            if (DateOnly.TryParse(birthday, out var birth) && DateOnly.TryParse(deathdate, out var death) && death >= birth)
            {
                var decedent = new Decedent(firstname, lastname, place, birth, death);
                var tree = new Tree(decedent);
                _trees.Add(tree);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Error = "Ungültige Daten!";
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
        }
    }

    public IActionResult ViewTree(int id)
    {
        if (id < 0 || id >= _trees.Count)
            return NotFound();

        ViewBag.Id = id;
        return View(_trees[id]);
    }

    [HttpGet]
    public IActionResult EditTree(int id)
    {
        if (id < 0 || id >= _trees.Count)
            return NotFound();

        ViewBag.Id = id;
        var person = _trees[id].PersonForTree;
        var model = new { person.Firstname, person.Lastname, person.Birthday, person.Deathdate };
        return View(model);
    }

    [HttpPost]
    public IActionResult EditTree(int id, string firstname, string lastname, string birthday, string deathdate)
    {
        try
        {
            if (id < 0 || id >= _trees.Count)
                return NotFound();

            if (DateOnly.TryParse(birthday, out var birth) && DateOnly.TryParse(deathdate, out var death) && death >= birth)
            {
                var person = _trees[id].PersonForTree;
                person.Firstname = firstname;
                person.Lastname = lastname;
                return RedirectToAction(nameof(ViewTree), new { id });
            }
            ViewBag.Error = "Ungültige Daten!";
            ViewBag.Id = id;
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Id = id;
            return View();
        }
    }

    [HttpGet]
    public IActionResult AddRelative(int id)
    {
        if (id < 0 || id >= _trees.Count)
            return NotFound();

        ViewBag.Id = id;
        return View();
    }

    [HttpPost]
    public IActionResult AddRelative(int id, string firstname, string lastname, string[] relationtypes)
    {
        try
        {
            if (id < 0 || id >= _trees.Count)
                return NotFound();

            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname))
            {
                ViewBag.Error = "Vorname und Nachname sind erforderlich!";
                ViewBag.Id = id;
                return View();
            }

            if (relationtypes == null || relationtypes.Length == 0)
            {
                ViewBag.Error = "Wählen Sie mindestens einen Verwandtschafts-Typ aus!";
                ViewBag.Id = id;
                return View();
            }

            var relative = new Relative(firstname, lastname);

            foreach (var rtype in relationtypes)
            {
                _trees[id].AddRelative(relative, rtype);
            }

            return RedirectToAction(nameof(ViewTree), new { id });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Id = id;
            return View();
        }
    }

    [HttpGet]
    public IActionResult RemoveRelative(int treeId, int relativeId)
    {
        if (treeId < 0 || treeId >= _trees.Count)
            return NotFound();

        var relations = _trees[treeId].GetAllRelations();
        if (relativeId < 0 || relativeId >= relations.Count)
            return NotFound();

        _trees[treeId].RemoveRelationAt(relativeId);

        return RedirectToAction(nameof(ViewTree), new { id = treeId });
    }
}
