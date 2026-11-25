using Microsoft.AspNetCore.Mvc;
using NyxAutomotive.Data;
using NyxAutomotive.Models;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class CategoriasController : Controller
{
    private readonly NyxContext _db;
    private readonly LocadoraService _locadora;
    private readonly AlgoritmosService _algoritmos;

    public CategoriasController(NyxContext db, LocadoraService locadora, AlgoritmosService algoritmos)
    {
        _db = db;
        _locadora = locadora;
        _algoritmos = algoritmos;
    }

    public IActionResult Index()
    {
        var categorias = _locadora.ListarCategorias();
        var veiculos = _locadora.ListarVeiculos();

        var quantidades = new Dictionary<int, int>();
        foreach (Categoria categoria in categorias)
        {
            quantidades[categoria.Id] = _algoritmos.SelecionarPorCategoria(veiculos, categoria.Id).Count;
        }

        ViewBag.Quantidades = quantidades;
        return View(categorias);
    }

    public IActionResult Create()
    {
        return View(new Categoria());
    }

    [HttpPost]
    public IActionResult Create(Categoria categoria)
    {
        if (categoria.Nome != null)
        {
            categoria.Nome = categoria.Nome.Trim();
        }

        if (categoria.Nome != null && _db.Categorias.Any(c => c.Nome == categoria.Nome))
        {
            ModelState.AddModelError(nameof(Categoria.Nome), "Já existe uma categoria com este nome.");
        }

        if (!ModelState.IsValid)
        {
            return View(categoria);
        }

        _db.Categorias.Add(categoria);
        _db.SaveChanges();

        TempData["Mensagem"] = "Categoria cadastrada com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
