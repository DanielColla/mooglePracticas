namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda

        SearchItem[] items = ElAprobado.Busqueda(query);

        for(int i = 0; i < items.Length; i++){
            System.Console.WriteLine(items[i].Title);
        }

        return new SearchResult(items, query);
    }
}
