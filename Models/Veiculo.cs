using System.ComponentModel.DataAnnotations;

namespace NyxAutomotive.Models;

public enum StatusVeiculo
{
    Disponivel = 0,
    Alugado = 1
}

public class Veiculo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a marca.")]
    [StringLength(40, ErrorMessage = "A marca deve ter até 40 caracteres.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "Informe o modelo.")]
    [StringLength(60, ErrorMessage = "O modelo deve ter até 60 caracteres.")]
    public string Modelo { get; set; }

    [Range(1990, 2100, ErrorMessage = "Informe um ano entre 1990 e 2100.")]
    public int Ano { get; set; }

    [Required(ErrorMessage = "Informe a placa.")]
    [RegularExpression(@"^[A-Za-z]{3}-?[0-9][A-Za-z0-9][0-9]{2}$", ErrorMessage = "Placa inválida. Ex.: ABC1D23.")]
    public string Placa { get; set; }

    [Display(Name = "Categoria")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria.")]
    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }

    [Display(Name = "Valor da diária (R$)")]
    [Range(1, 100000, ErrorMessage = "Informe um valor de diária maior que zero.")]
    public decimal ValorDiaria { get; set; }

    public StatusVeiculo Status { get; private set; } = StatusVeiculo.Disponivel;

    public bool EstaDisponivel => Status == StatusVeiculo.Disponivel;

    public string StatusTexto => EstaDisponivel ? "Disponível" : "Alugado";

    public Veiculo()
    {
    }

    public Veiculo(string marca, string modelo, int ano, string placa, int categoriaId, decimal valorDiaria)
    {
        Marca = marca;
        Modelo = modelo;
        Ano = ano;
        Placa = placa;
        CategoriaId = categoriaId;
        ValorDiaria = valorDiaria;
    }

    public void Alugar()
    {
        Status = StatusVeiculo.Alugado;
    }

    public void Liberar()
    {
        Status = StatusVeiculo.Disponivel;
    }
}
