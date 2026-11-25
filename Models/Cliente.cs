using System.ComponentModel.DataAnnotations;

namespace NyxAutomotive.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    [StringLength(80, ErrorMessage = "O nome deve ter até 80 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe o CPF.")]
    [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF inválido. Ex.: 123.456.789-09.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Informe o telefone.")]
    [StringLength(20, ErrorMessage = "O telefone deve ter até 20 caracteres.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Informe o e-mail.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [StringLength(80)]
    [Display(Name = "E-mail")]
    public string Email { get; set; }

    public Cliente()
    {
    }

    public Cliente(string nome, string cpf, string telefone, string email)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
    }
}
