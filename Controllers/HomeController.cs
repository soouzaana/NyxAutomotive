using Microsoft.AspNetCore.Mvc;
using NyxAutomotive.Data;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class HomeController : Controller
{
    private readonly NyxContext _db;
    private readonly LocadoraService _locadora;
    private readonly AlgoritmosService _algoritmos;

    public HomeController(NyxContext db, LocadoraService locadora, AlgoritmosService algoritmos)
    {
        _db = db;
        _locadora = locadora;
        _algoritmos = algoritmos;
    }

    public IActionResult Index()
    {
        var veiculos = _locadora.ListarVeiculos();
        int disponiveis = _algoritmos.SelecionarDisponiveis(veiculos).Count;

        ViewBag.TotalVeiculos = veiculos.Count;
        ViewBag.Disponiveis = disponiveis;
        ViewBag.Alugados = veiculos.Count - disponiveis;
        ViewBag.TotalClientes = _locadora.ListarClientes().Count;
        ViewBag.TotalReservas = _db.Reservas.Count();

        return View();
    }

    public IActionResult Erro()
    {
        return View();
    }
}
