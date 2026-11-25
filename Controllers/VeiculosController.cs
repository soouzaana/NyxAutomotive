using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NyxAutomotive.Data;
using NyxAutomotive.Models;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class VeiculosController : Controller
{
    private readonly NyxContext _db;
    private readonly LocadoraService _locadora;

    public VeiculosController(NyxContext db, LocadoraService locadora)
    {
        _db = db;
        _locadora = locadora;
    }

    public IActionResult Index()
    {
        return View(_locadora.ListarVeiculos());
    }

    public IActionResult Details(int id)
    {
        Veiculo veiculo = _db.Veiculos.Include(v => v.Categoria).FirstOrDefault(v => v.Id == id);
        if (veiculo == null)
        {
            return NotFound();
        }

        return View(veiculo);
    }

    public IActionResult Create()
    {
        CarregarCategorias(0);
        return View(new Veiculo { Ano = DateTime.Today.Year });
    }

    [HttpPost]
    public IActionResult Create(Veiculo veiculo)
    {
        veiculo.Placa = LimparPlaca(veiculo.Placa);
        ValidarPlacaUnica(veiculo);

        if (!ModelState.IsValid)
        {
            CarregarCategorias(veiculo.CategoriaId);
            return View(veiculo);
        }

        _db.Veiculos.Add(veiculo);
        _db.SaveChanges();

        TempData["Mensagem"] = "Veículo cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        Veiculo veiculo = _db.Veiculos.Find(id);
        if (veiculo == null)
        {
            return NotFound();
        }

        CarregarCategorias(veiculo.CategoriaId);
        return View(veiculo);
    }

    [HttpPost]
    public IActionResult Edit(int id, Veiculo veiculo)
    {
        Veiculo existente = _db.Veiculos.Find(id);
        if (existente == null)
        {
            return NotFound();
        }

        veiculo.Id = id;
        veiculo.Placa = LimparPlaca(veiculo.Placa);
        ValidarPlacaUnica(veiculo);

        if (!ModelState.IsValid)
        {
            CarregarCategorias(veiculo.CategoriaId);
            return View(veiculo);
        }

        existente.Marca = veiculo.Marca;
        existente.Modelo = veiculo.Modelo;
        existente.Ano = veiculo.Ano;
        existente.Placa = veiculo.Placa;
        existente.CategoriaId = veiculo.CategoriaId;
        existente.ValorDiaria = veiculo.ValorDiaria;
        _db.SaveChanges();

        TempData["Mensagem"] = "Veículo atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        Veiculo veiculo = _db.Veiculos.Find(id);
        if (veiculo == null)
        {
            return NotFound();
        }

        if (_db.Reservas.Any(r => r.VeiculoId == id))
        {
            TempData["Erro"] = "Não é possível excluir: o veículo possui reservas registradas.";
            return RedirectToAction(nameof(Index));
        }

        _db.Veiculos.Remove(veiculo);
        _db.SaveChanges();

        TempData["Mensagem"] = "Veículo excluído.";
        return RedirectToAction(nameof(Index));
    }

    private void CarregarCategorias(int selecionada)
    {
        ViewBag.Categorias = new SelectList(_locadora.ListarCategorias(), "Id", "Nome", selecionada);
    }

    private static string LimparPlaca(string placa)
    {
        if (placa == null)
        {
            return null;
        }

        return placa.Replace("-", "").Trim().ToUpperInvariant();
    }

    private void ValidarPlacaUnica(Veiculo veiculo)
    {
        if (veiculo.Placa != null && _db.Veiculos.Any(v => v.Placa == veiculo.Placa && v.Id != veiculo.Id))
        {
            ModelState.AddModelError(nameof(Veiculo.Placa), "Já existe um veículo com esta placa.");
        }
    }
}
