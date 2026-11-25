using NyxAutomotive.Models;

namespace NyxAutomotive.Services;

public class AlgoritmosService
{
    public int Comparacoes { get; private set; }
    public int Trocas { get; private set; }

    public List<Veiculo> BuscaLinear(List<Veiculo> veiculos, string termo)
    {
        Comparacoes = 0;
        Trocas = 0;

        var encontrados = new List<Veiculo>();
        string busca = Normalizar(termo);

        for (int i = 0; i < veiculos.Count; i++)
        {
            Comparacoes++;
            Veiculo veiculo = veiculos[i];

            if (Normalizar(veiculo.Placa).Contains(busca) || Normalizar(veiculo.Modelo).Contains(busca))
            {
                encontrados.Add(veiculo);
            }
        }

        return encontrados;
    }

    public Veiculo BuscaBinaria(List<Veiculo> ordenadaPorId, int id)
    {
        Comparacoes = 0;
        Trocas = 0;

        int inicio = 0;
        int fim = ordenadaPorId.Count - 1;

        while (inicio <= fim)
        {
            int meio = (inicio + fim) / 2;
            Comparacoes++;

            if (ordenadaPorId[meio].Id == id)
            {
                return ordenadaPorId[meio];
            }

            if (ordenadaPorId[meio].Id < id)
            {
                inicio = meio + 1;
            }
            else
            {
                fim = meio - 1;
            }
        }

        return null;
    }

    public List<Veiculo> BubbleSortPorDiaria(List<Veiculo> veiculos)
    {
        Comparacoes = 0;
        Trocas = 0;

        var lista = new List<Veiculo>(veiculos);

        for (int i = 0; i < lista.Count - 1; i++)
        {
            bool houveTroca = false;

            for (int j = 0; j < lista.Count - 1 - i; j++)
            {
                Comparacoes++;

                if (lista[j].ValorDiaria > lista[j + 1].ValorDiaria)
                {
                    Veiculo auxiliar = lista[j];
                    lista[j] = lista[j + 1];
                    lista[j + 1] = auxiliar;
                    Trocas++;
                    houveTroca = true;
                }
            }

            if (!houveTroca)
            {
                break;
            }
        }

        return lista;
    }

    public List<Veiculo> InsertionSortPorAno(List<Veiculo> veiculos)
    {
        return InsertionSort(veiculos, veiculo => veiculo.Ano);
    }

    public List<Veiculo> InsertionSortPorId(List<Veiculo> veiculos)
    {
        return InsertionSort(veiculos, veiculo => veiculo.Id);
    }

    private List<Veiculo> InsertionSort(List<Veiculo> veiculos, Func<Veiculo, int> chave)
    {
        Comparacoes = 0;
        Trocas = 0;

        var lista = new List<Veiculo>(veiculos);

        for (int i = 1; i < lista.Count; i++)
        {
            Veiculo atual = lista[i];
            int j = i - 1;

            while (j >= 0)
            {
                Comparacoes++;

                if (chave(lista[j]) > chave(atual))
                {
                    lista[j + 1] = lista[j];
                    Trocas++;
                    j--;
                }
                else
                {
                    break;
                }
            }

            lista[j + 1] = atual;
        }

        return lista;
    }


    public List<Veiculo> SelecionarDisponiveis(List<Veiculo> veiculos)
    {
        Comparacoes = 0;
        Trocas = 0;

        var selecionados = new List<Veiculo>();

        foreach (Veiculo veiculo in veiculos)
        {
            Comparacoes++;

            if (veiculo.EstaDisponivel)
            {
                selecionados.Add(veiculo);
            }
        }

        return selecionados;
    }

    public List<Veiculo> SelecionarPorCategoria(List<Veiculo> veiculos, int categoriaId)
    {
        Comparacoes = 0;
        Trocas = 0;

        var selecionados = new List<Veiculo>();

        foreach (Veiculo veiculo in veiculos)
        {
            Comparacoes++;

            if (veiculo.CategoriaId == categoriaId)
            {
                selecionados.Add(veiculo);
            }
        }

        return selecionados;
    }

    private static string Normalizar(string texto)
    {
        if (texto == null)
        {
            return "";
        }

        return texto.Replace("-", "").Replace(" ", "").ToUpperInvariant();
    }
}
