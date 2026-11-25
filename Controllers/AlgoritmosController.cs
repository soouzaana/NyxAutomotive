using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NyxAutomotive.Models;
using NyxAutomotive.Services;

namespace NyxAutomotive.Controllers;

public class AlgoritmosController : Controller
{
    private readonly LocadoraService _locadora;
    private readonly AlgoritmosService _algoritmos;

    public AlgoritmosController(LocadoraService locadora, AlgoritmosService algoritmos)
    {
        _locadora = locadora;
        _algoritmos = algoritmos;
    }

    public IActionResult Index(string acao, string termo, int? id, int? categoriaId)
    {
        var veiculos = _locadora.ListarVeiculos();
        var categorias = _locadora.ListarCategorias();

        ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", categoriaId);
        ViewBag.Acao = acao;
        ViewBag.Termo = termo;
        ViewBag.Id = id;
        ViewBag.TotalVeiculos = veiculos.Count;

        ResultadoAlgoritmo resultado = null;

        switch (acao)
        {
            case "linear":
                resultado = ExecutarBuscaLinear(veiculos, termo);
                break;

            case "binaria":
                resultado = ExecutarBuscaBinaria(veiculos, id);
                break;

            case "bubble":
                var porDiaria = _algoritmos.BubbleSortPorDiaria(veiculos);
                resultado = new ResultadoAlgoritmo
                {
                    Algoritmo = "Bubble Sort",
                    Complexidade = "O(n²)",
                    Descricao = "Compara pares de veículos vizinhos e troca de lugar quando o da esquerda tem diária maior. Ordem: valor da diária, do menor para o maior.",
                    Detalhe = $"{_algoritmos.Comparacoes} comparações e {_algoritmos.Trocas} trocas",
                    Veiculos = porDiaria
                };
                break;

            case "insertion":
                var porAno = _algoritmos.InsertionSortPorAno(veiculos);
                resultado = new ResultadoAlgoritmo
                {
                    Algoritmo = "Insertion Sort",
                    Complexidade = "O(n²)",
                    Descricao = "Pega um veículo por vez e o insere na posição correta da parte já ordenada. Ordem: ano de fabricação, do mais antigo para o mais novo.",
                    Detalhe = $"{_algoritmos.Comparacoes} comparações e {_algoritmos.Trocas} deslocamentos",
                    Veiculos = porAno
                };
                break;

            case "disponiveis":
                var disponiveis = _algoritmos.SelecionarDisponiveis(veiculos);
                resultado = new ResultadoAlgoritmo
                {
                    Algoritmo = "Seleção: veículos disponíveis",
                    Complexidade = "O(n)",
                    Descricao = "Percorre a lista com foreach e separa apenas os veículos com status Disponível.",
                    Detalhe = $"{_algoritmos.Comparacoes} veículos verificados, {disponiveis.Count} selecionados",
                    Veiculos = disponiveis
                };
                break;

            case "categoria":
                if (categoriaId == null)
                {
                    resultado = new ResultadoAlgoritmo
                    {
                        Algoritmo = "Seleção por categoria",
                        Complexidade = "O(n)",
                        Mensagem = "Escolha uma categoria para filtrar."
                    };
                }
                else
                {
                    var daCategoria = _algoritmos.SelecionarPorCategoria(veiculos, categoriaId.Value);
                    resultado = new ResultadoAlgoritmo
                    {
                        Algoritmo = "Seleção por categoria",
                        Complexidade = "O(n)",
                        Descricao = "Percorre a lista com foreach e separa os veículos cuja categoria é a escolhida.",
                        Detalhe = $"{_algoritmos.Comparacoes} veículos verificados, {daCategoria.Count} selecionados",
                        Veiculos = daCategoria
                    };
                }
                break;
        }

        return View(resultado);
    }

    private ResultadoAlgoritmo ExecutarBuscaLinear(List<Veiculo> veiculos, string termo)
    {
        var resultado = new ResultadoAlgoritmo { Algoritmo = "Busca Linear", Complexidade = "O(n)" };

        if (string.IsNullOrWhiteSpace(termo))
        {
            resultado.Mensagem = "Digite uma placa ou um modelo para buscar.";
            return resultado;
        }

        resultado.Veiculos = _algoritmos.BuscaLinear(veiculos, termo);
        resultado.Descricao = $"Percorre a lista do primeiro ao último veículo comparando a placa e o modelo com \"{termo.Trim()}\".";
        resultado.Detalhe = $"{_algoritmos.Comparacoes} comparações, {resultado.Veiculos.Count} encontrado(s)";

        if (resultado.Veiculos.Count == 0)
        {
            resultado.Mensagem = "Nenhum veículo encontrado.";
        }

        return resultado;
    }

    private ResultadoAlgoritmo ExecutarBuscaBinaria(List<Veiculo> veiculos, int? id)
    {
        var resultado = new ResultadoAlgoritmo { Algoritmo = "Busca Binária", Complexidade = "O(log n)" };

        if (id == null)
        {
            resultado.Mensagem = "Digite o ID do veículo para buscar.";
            return resultado;
        }

        var ordenadaPorId = _algoritmos.InsertionSortPorId(veiculos);
        Veiculo encontrado = _algoritmos.BuscaBinaria(ordenadaPorId, id.Value);

        resultado.Descricao = "Lista ordenada por ID com Insertion Sort; depois olha o elemento do meio e descarta a metade que não pode conter o ID.";
        resultado.Detalhe = $"{_algoritmos.Comparacoes} comparações na busca";

        if (encontrado == null)
        {
            resultado.Mensagem = $"Nenhum veículo com ID {id.Value}.";
        }
        else
        {
            resultado.Veiculos.Add(encontrado);
        }

        return resultado;
    }
}
