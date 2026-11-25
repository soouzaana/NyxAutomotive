using Microsoft.AspNetCore.Mvc;
using NyxAutomotive.Data;
using NyxAutomotive.Models;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class ClientesController : Controller
{
    private readonly NyxContext _db;
    private readonly LocadoraService _locadora;

    public ClientesController(NyxContext db, LocadoraService locadora)
    {
        _db = db;
        _locadora = locadora;
    }

    public IActionResult Index()
    {
        return View(_locadora.ListarClientes());
    }

    public IActionResult Create()
    {
        return View(new Cliente());
    }

    [HttpPost]
    public IActionResult Create(Cliente cliente)
    {
        ValidarCpfUnico(cliente);

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        _db.Clientes.Add(cliente);
        _db.SaveChanges();

        TempData["Mensagem"] = "Cliente cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        Cliente cliente = _db.Clientes.Find(id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    [HttpPost]
    public IActionResult Edit(int id, Cliente cliente)
    {
        Cliente existente = _db.Clientes.Find(id);
        if (existente == null)
        {
            return NotFound();
        }

        cliente.Id = id;
        ValidarCpfUnico(cliente);

        if (!ModelState.IsValid)
        {
            return View(cliente);
        }

        existente.Nome = cliente.Nome;
        existente.Cpf = cliente.Cpf;
        existente.Telefone = cliente.Telefone;
        existente.Email = cliente.Email;
        _db.SaveChanges();

        TempData["Mensagem"] = "Cliente atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        Cliente cliente = _db.Clientes.Find(id);
        if (cliente == null)
        {
            return NotFound();
        }

        if (_db.Reservas.Any(r => r.ClienteId == id))
        {
            TempData["Erro"] = "Não é possível excluir: o cliente possui reservas registradas.";
            return RedirectToAction(nameof(Index));
        }

        _db.Clientes.Remove(cliente);
        _db.SaveChanges();

        TempData["Mensagem"] = "Cliente excluído.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidarCpfUnico(Cliente cliente)
    {
        if (cliente.Cpf != null && _db.Clientes.Any(c => c.Cpf == cliente.Cpf && c.Id != cliente.Id))
        {
            ModelState.AddModelError(nameof(Cliente.Cpf), "Já existe um cliente com este CPF.");
        }
    }
}
