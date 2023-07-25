namespace MoogleEngine;

public class SearchItem
{
    public SearchItem(string title, string snippet, float score)
    {
        this.Title = title;
        this.Snippet = snippet;
        this.Score = score;
    }

    public SearchItem(string v1, double v2, Dictionary<string, List<string>> frecuencia, string v3, string primerasPalabras)
    {
        V1 = v1;
        V2 = v2;
        Frecuencia = frecuencia;
        V3 = v3;
        PrimerasPalabras = primerasPalabras;
    }

    public string Title { get; private set; }

    public string Snippet { get; private set; }

    public float Score { get; private set; }
    public string V1 { get; }
    public double V2 { get; }
    public Dictionary<string, List<string>> Frecuencia { get; }
    public string V3 { get; }
    public string PrimerasPalabras { get; }
}
