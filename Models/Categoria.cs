using System.ComponentModel.DataAnnotations;

namespace NyxAutomotive.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome da categoria.")]
    [StringLength(40, ErrorMessage = "O nome deve ter até 40 caracteres.")]
    public string Nome { get; set; }
}
