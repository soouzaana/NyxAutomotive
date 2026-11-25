using System.ComponentModel.DataAnnotations;

namespace NyxAutomotive.Models;

public class Funcionario
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Nome { get; set; }

    [Required]
    [StringLength(40)]
    public string Cargo { get; set; }

    public Funcionario()
    {
    }

    public Funcionario(string nome, string cargo)
    {
        Nome = nome;
        Cargo = cargo;
    }
}
