using Microsoft.AspNetCore.Mvc;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class ReservasController : Controller
{
    private readonly LocadoraService _locadora;

    public ReservasController(LocadoraService locadora)
    {
        _locadora = locadora;
    }

    public IActionResult Index()
    {
        return View(_locadora.ListarReservas());
    }

    public IActionResult Create()
    {
        ViewBag.Clientes = _locadora.ListarClientes();
        ViewBag.Veiculos = _locadora.ListarVeiculosDisponiveis();
        return View();
    }

    [HttpPost]
    public IActionResult Create(int clienteId, int veiculoId, DateTime retirada, DateTime devolucao)
    {
        var resultado = _locadora.RegistrarReserva(clienteId, veiculoId, retirada, devolucao);

        if (!resultado.Sucesso)
        {
            TempData["Erro"] = resultado.Mensagem;
            return RedirectToAction(nameof(Create));
        }

        TempData["Mensagem"] = resultado.Mensagem;
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Cancelar(int id)
    {
        var resultado = _locadora.CancelarReserva(id);

        if (resultado.Sucesso)
        {
            TempData["Mensagem"] = resultado.Mensagem;
        }
        else
        {
            TempData["Erro"] = resultado.Mensagem;
        }

        return RedirectToAction(nameof(Index));
    }
}
