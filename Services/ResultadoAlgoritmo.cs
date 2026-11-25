using NyxAutomotive.Models;

namespace NyxAutomotive.Services;

public class ResultadoAlgoritmo
{
    public string Algoritmo { get; set; }
    public string Complexidade { get; set; }
    public string Descricao { get; set; }
    public string Detalhe { get; set; }
    public string Mensagem { get; set; }
    public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
