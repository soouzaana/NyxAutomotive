using Microsoft.EntityFrameworkCore;
using NyxAutomotive.Models;

namespace NyxAutomotive.Data;

public class NyxContext : DbContext
{
    public NyxContext(DbContextOptions<NyxContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
}
