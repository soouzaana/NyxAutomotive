namespace NyxAutomotive.Models;

public enum StatusReserva
{
    Ativa = 0,
    Cancelada = 1
}

public class Reserva
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; }

    public DateTime DataRetirada { get; set; }
    public DateTime DataDevolucao { get; set; }

    public decimal ValorTotal { get; private set; }

    public StatusReserva Status { get; private set; } = StatusReserva.Ativa;

    public bool EstaAtiva => Status == StatusReserva.Ativa;

    public string StatusTexto => EstaAtiva ? "Ativa" : "Cancelada";

    public Reserva()
    {
    }

    public Reserva(int clienteId, int veiculoId, DateTime retirada, DateTime devolucao, decimal valorDiaria)
    {
        ClienteId = clienteId;
        VeiculoId = veiculoId;
        DataRetirada = retirada.Date;
        DataDevolucao = devolucao.Date;
        ValorTotal = CalcularTotal(valorDiaria);
    }
    
    public int QuantidadeDias()
    {
        int dias = (DataDevolucao.Date - DataRetirada.Date).Days;
        if (dias < 1)
        {
            dias = 1;
        }
        return dias;
    }

    public decimal CalcularTotal(decimal valorDiaria)
    {
        return QuantidadeDias() * valorDiaria;
    }

    public void Cancelar()
    {
        Status = StatusReserva.Cancelada;
    }
}
