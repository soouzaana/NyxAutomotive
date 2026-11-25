using System.Globalization;

namespace NyxAutomotive.Services;

public static class Formatacao
{
    public static string Moeda(decimal valor)
    {
        string texto = valor.ToString("N2", CultureInfo.InvariantCulture);
        texto = texto.Replace(",", "#").Replace(".", ",").Replace("#", ".");
        return "R$ " + texto;
    }

    public static string Data(DateTime data)
    {
        return data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
}
