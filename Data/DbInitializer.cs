using NyxAutomotive.Models;

namespace NyxAutomotive.Data;

public static class DbInitializer
{
    public static void Inicializar(NyxContext db)
    {
        db.Database.EnsureCreated();

        if (db.Categorias.Any())
        {
            return;
        }

        db.Categorias.Add(new Categoria { Nome = "Econômico" });
        db.Categorias.Add(new Categoria { Nome = "Compacto" });
        db.Categorias.Add(new Categoria { Nome = "Sedan" });
        db.Categorias.Add(new Categoria { Nome = "SUV" });
        db.Categorias.Add(new Categoria { Nome = "Luxo" });
        db.SaveChanges();

        var veiculos = new List<Veiculo>
        {
            new Veiculo("Fiat", "Mobi", 2022, "QWE1A23", 1, 110m),
            new Veiculo("Chevrolet", "Onix", 2023, "RTY2B34", 2, 140m),
            new Veiculo("Hyundai", "HB20", 2021, "UIO3C45", 2, 130m),
            new Veiculo("Toyota", "Corolla", 2024, "ASD4D56", 3, 260m),
            new Veiculo("Honda", "Civic", 2020, "FGH5E67", 3, 240m),
            new Veiculo("Jeep", "Compass", 2023, "JKL6F78", 4, 330m),
            new Veiculo("Volkswagen", "T-Cross", 2022, "ZXC7G89", 4, 290m),
            new Veiculo("BMW", "320i", 2024, "VBN8H90", 5, 520m),
            new Veiculo("Mercedes-Benz", "C 180", 2023, "MNB9I01", 5, 560m),
            new Veiculo("Renault", "Kwid", 2019, "POI0J12", 1, 95m)
        };
        db.Veiculos.AddRange(veiculos);

        db.Clientes.Add(new Cliente("Ana Souza", "123.456.789-09", "(92) 99111-2233", "ana.souza@email.com"));
        db.Clientes.Add(new Cliente("Bruno Lima", "234.567.890-12", "(92) 99222-3344", "bruno.lima@email.com"));
        db.Clientes.Add(new Cliente("Carla Mendes", "345.678.901-23", "(92) 99333-4455", "carla.mendes@email.com"));
        db.Clientes.Add(new Cliente("Diego Ferreira", "456.789.012-34", "(92) 99444-5566", "diego.ferreira@email.com"));
        db.Clientes.Add(new Cliente("Eduarda Rocha", "567.890.123-45", "(92) 99555-6677", "eduarda.rocha@email.com"));
        db.Clientes.Add(new Cliente("Felipe Alves", "678.901.234-56", "(92) 99666-7788", "felipe.alves@email.com"));

        db.Funcionarios.Add(new Funcionario("Marina Costa", "Atendente"));
        db.Funcionarios.Add(new Funcionario("Rafael Nunes", "Gerente"));
        db.SaveChanges();

        DateTime hoje = DateTime.Today;

        var reservaHb20 = new Reserva(1, 3, hoje, hoje.AddDays(3), 130m);
        var reservaCompass = new Reserva(2, 6, hoje.AddDays(1), hoje.AddDays(6), 330m);
        var reservaCancelada = new Reserva(3, 9, hoje.AddDays(2), hoje.AddDays(4), 560m);
        reservaCancelada.Cancelar();

        var hb20 = db.Veiculos.Find(3);
        var compass = db.Veiculos.Find(6);
        hb20.Alugar();
        compass.Alugar();

        db.Reservas.Add(reservaHb20);
        db.Reservas.Add(reservaCompass);
        db.Reservas.Add(reservaCancelada);
        db.SaveChanges();
    }
}
