using Microsoft.EntityFrameworkCore;
using NyxAutomotive.Data;
using NyxAutomotive.Models;

namespace NyxAutomotive.Services;

public class LocadoraService
{
    private readonly NyxContext _db;
    private readonly AlgoritmosService _algoritmos;

    public LocadoraService(NyxContext db, AlgoritmosService algoritmos)
    {
        _db = db;
        _algoritmos = algoritmos;
    }

    public List<Veiculo> ListarVeiculos()
    {
        return _db.Veiculos.Include(v => v.Categoria).ToList();
    }

    public List<Veiculo> ListarVeiculosDisponiveis()
    {
        return _algoritmos.SelecionarDisponiveis(ListarVeiculos());
    }

    public List<Cliente> ListarClientes()
    {
        return _db.Clientes.ToList();
    }

    public List<Categoria> ListarCategorias()
    {
        return _db.Categorias.ToList();
    }

    public List<Reserva> ListarReservas()
    {
        var reservas = _db.Reservas.Include(r => r.Cliente).Include(r => r.Veiculo).ToList();
        reservas.Reverse();
        return reservas;
    }

    public (bool Sucesso, string Mensagem) RegistrarReserva(int clienteId, int veiculoId, DateTime retirada, DateTime devolucao)
    {
        if (retirada == default || devolucao == default)
        {
            return (false, "Informe as datas de retirada e devolução.");
        }

        if (devolucao.Date < retirada.Date)
        {
            return (false, "A data de devolução não pode ser anterior à data de retirada.");
        }

        Cliente cliente = _db.Clientes.Find(clienteId);
        if (cliente == null)
        {
            return (false, "Selecione um cliente cadastrado.");
        }

        Veiculo veiculo = _db.Veiculos.Find(veiculoId);
        if (veiculo == null)
        {
            return (false, "Selecione um veículo.");
        }

        if (!veiculo.EstaDisponivel)
        {
            return (false, "Este veículo está indisponível para reserva.");
        }

        var reserva = new Reserva(clienteId, veiculoId, retirada, devolucao, veiculo.ValorDiaria);
        veiculo.Alugar();

        _db.Reservas.Add(reserva);
        _db.SaveChanges();

        return (true, $"Reserva registrada para {cliente.Nome}: {reserva.QuantidadeDias()} diária(s), total de {Formatacao.Moeda(reserva.ValorTotal)}.");
    }

    public (bool Sucesso, string Mensagem) CancelarReserva(int id)
    {
        Reserva reserva = _db.Reservas.Find(id);
        if (reserva == null)
        {
            return (false, "Reserva não encontrada.");
        }

        if (!reserva.EstaAtiva)
        {
            return (false, "Esta reserva já está cancelada.");
        }

        reserva.Cancelar();

        Veiculo veiculo = _db.Veiculos.Find(reserva.VeiculoId);
        if (veiculo != null)
        {
            veiculo.Liberar();
        }

        _db.SaveChanges();

        return (true, "Reserva cancelada e veículo liberado.");
    }
}
