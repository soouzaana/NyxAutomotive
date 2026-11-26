# NYX Automotive

Sistema web simples de locação de veículos, desenvolvido em C# com ASP.NET Core MVC para um trabalho acadêmico de Algoritmos e Estruturas de Dados. Permite cadastrar veículos, clientes e categorias, consultar a frota e registrar reservas. A página **Algoritmos** demonstra busca, ordenação e seleção implementadas manualmente.

## Participantes
- Ana Beatriz
- Ana Maria
- Karen Yamaguchi
- Lúcia Viana
- Maria Júlya

## Tecnologias

- C# / .NET 8
- ASP.NET Core MVC + Razor Views
- SQLite + Entity Framework Core 8
- HTML5, CSS3 e JavaScript (apenas interações visuais simples)

## Funcionalidades

- **Dashboard:** total de veículos, disponíveis, alugados, clientes e reservas.
- **Veículos:** cadastrar, listar, editar, excluir e ver detalhes.
- **Clientes:** cadastrar, listar, editar e excluir.
- **Reservas:** escolher cliente e veículo disponível, informar retirada e devolução, cálculo automático do total e cancelamento (libera o veículo).
- **Categorias:** cadastrar e consultar (Econômico, Compacto, Sedan, SUV, Luxo).
- **Algoritmos:** demonstração das buscas, ordenações e seleções, com o nome do algoritmo e o número de comparações exibidos.

Regras das reservas: não reserva veículo alugado; a devolução não pode ser anterior à retirada; reservar marca o veículo como Alugado; cancelar o libera. O mínimo cobrado é 1 diária.

## Estrutura de pastas

```
NyxAutomotive/
├── Models/        Veiculo, Cliente, Reserva, Categoria, Funcionario
├── Controllers/   Home, Veiculos, Clientes, Reservas, Categorias, Algoritmos
├── Data/          NyxContext (EF Core + SQLite) e DbInitializer (dados iniciais)
├── Services/      AlgoritmosService (algoritmos), LocadoraService (regras de reserva)
├── Views/         Páginas Razor
├── wwwroot/       css/, js/, img/
├── Program.cs
└── appsettings.json
```

## Como executar

Requisitos: [.NET SDK 8](https://dotnet.microsoft.com/download) (ou Visual Studio 2022 com a carga de trabalho ASP.NET).

```
cd NyxAutomotive
dotnet run
```

Acesse `http://localhost:5080`. No Visual Studio, abra `NyxAutomotive.csproj` e execute (F5).

O banco `nyx.db` é criado automaticamente na primeira execução, já com 10 veículos, 6 clientes, 5 categorias e 3 reservas de exemplo. Para reiniciar os dados, pare o sistema e apague o arquivo `nyx.db`.

## Algoritmos implementados

Todos estão em `Services/AlgoritmosService.cs`, trabalham sobre `List<Veiculo>` e não usam `OrderBy`, `Sort()`, `Where()` ou `BinarySearch()`.

| Algoritmo | Uso | Complexidade |
|---|---|---|
| Busca Linear | Percorre a lista e encontra veículos por placa ou modelo | O(n) |
| Busca Binária | Encontra um veículo pelo ID em lista ordenada por ID (a lista é ordenada antes com Insertion Sort) | O(log n) |
| Bubble Sort | Ordena pelo valor da diária | O(n²) |
| Insertion Sort | Ordena pelo ano de fabricação | O(n²) |
| Seleção | `SelecionarDisponiveis` e `SelecionarPorCategoria` filtram a lista com `foreach` | O(n) |
