using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MoogleEngine
{
    class ElAprobado
    {
        static (string[], string[]) LeerArchivosDeTexto()
        {
            string rutaCarpeta = Path.Combine(Directory.GetParent(".").ToString(), "Content");
            // Buscar todos los archivos de texto en la carpeta especificada
            string[] archivos = Directory.GetFiles(rutaCarpeta, "*.txt");
            // Crear un array para el contenido de cada archivo
            string[] contenidoArchivos = new string[archivos.Length];
            // Leer el contenido de cada archivo y almacenarlo en el array correspondiente
            for (int i = 0; i < archivos.Length; i++)
            {
                contenidoArchivos[i] = File.ReadAllText(archivos[i]);
            }

            return (contenidoArchivos, archivos);
        }

        static string[] Normalizar(string documento)
        {
            // Crear una lista para almacenar las palabras normalizadas
            List<string> palabras = new List<string>();
            // Separar el texto en palabras utilizando expresiones regulares
            string[] palabrasTexto = Regex.Split(documento, @"\W+");
            // Normalizar cada palabra a minúscula y agregarla a la lista de palabras
            foreach (string palabra in palabrasTexto)
            {
                if (!string.IsNullOrWhiteSpace(palabra))
                {
                    palabras.Add(palabra.ToLower());
                }
            }
            // Convertir la lista de palabras en un array y devolverlo
            return palabras.ToArray();
        }

        static string[] ObtenerPalabras(string nombreArchivo)
        {
            // Leer el contenido del archivo
            string contenido = File.ReadAllText(nombreArchivo);

            // Normalizar el texto y separarlo en palabras
            string[] palabras = Regex.Split(contenido.ToLower(), @"\W+");

            return palabras;
        }
         
        static double[] CalcularVectorTfIdf(string[] palabras, Dictionary<string, List<string>> frecuencia, string[] array)
        {
            double[] vector = new double[palabras.Length];
            for (int i = 0; i <palabras.Length ; i++)
            {
                string palabra = palabras[i];
                double tf = 0;
                double idf = 0;
                int frecuenciaEnArray = array.Count(p => p == palabra);
                vector[i]=frecuenciaEnArray;
                if (frecuenciaEnArray > 0)
                {
                    tf = (double)frecuenciaEnArray / array.Length;
                    idf = Math.Log((double)array.Length / frecuencia[palabra].Count);
                }
                vector[i] = tf * idf;
            }
            return vector;
        }
        
        static double[] CalcularVectorTfIdfQuery(string[] palabras, Dictionary<string, List<string>> frecuencia, string[] array)
        {
            double[] vector = new double[matriz.GetLength(1)];
            for (int i = 0; i < palabras.Length; i++)
            {
                string palabra = palabras[i];
                int index = frecuencia.Keys.ToList().IndexOf(palabra);
                vector[index] = array.Count(p => p == palabra);
            }
            return vector;
        }

        static double CalcularSimilitudCoseno(double[] vector1, double[] vector2)
        {
            double productoPunto = 1;
            double magnitud1 = 1;
            double magnitud2 = 1;
            for (int i = 0; i < vector1.Length; i++)
            {
                productoPunto += vector1[i] * vector2[i];
                magnitud1 += vector1[i] * vector1[i];
                magnitud2 += vector2[i] * vector2[i];
            }
            magnitud1 = Math.Sqrt(magnitud1);
            magnitud2 = Math.Sqrt(magnitud2);
            return productoPunto / (magnitud1 * magnitud2);
        }

        static string[] ObtenerPalabrasQuery(string query)
        {
            // Normalizar el texto y separarlo en palabras
            string[] palabras = Regex.Split(query.ToLower(), @"\W+");

            return palabras;
        }

        static double[,] CalcularMatrizTfIdf(string[] contenido, string[] nombres)
        {
            // Crear la matriz de TF-IDF       
            Dictionary<string, List<string>> frecuencia = new Dictionary<string, List<string>>();
            for (int i = 0; i < nombres.Length; i++)
            {
                string[] palabrasEnDocumento = Normalizar(File.ReadAllText(nombres[i]));
                foreach (string palabra in palabrasEnDocumento)
                {
                    if (!frecuencia.ContainsKey(palabra))
                    {
                        frecuencia[palabra] = new List<string>();
                    }
                    if (!frecuencia[palabra].Contains(nombres[i]))
                    {
                        frecuencia[palabra].Add(nombres[i]);
                    }
                }
            }

            double[,] matriz = new double[nombres.Length, frecuencia.Count];
            for (int i = 0; i < nombres.Length; i++)
            {
                double[] vectorDocumento = CalcularVectorTfIdf(frecuencia.Keys.ToArray(), frecuencia, Normalizar(contenido[i]));
                for (int j = 0; j < frecuencia.Count; j++)
                {
                    matriz[i, j] = vectorDocumento[j];
                }
            }

            return matriz;
        }
        static double[] CalcularSimilitudQuery(string query, string[] nombres, string[] contenido, double[,] matriz, Dictionary<string, List<string>> frecuencia)
        {
            // Obtener las palabras de la consulta
            string[] palabrasQuery = ObtenerPalabrasQuery(query);
            // Calcular el vector de TF-IDF de la consulta
            double[] vectorQuery = CalcularVectorTfIdfQuery(frecuencia.Keys.ToArray(), frecuencia, palabrasQuery);
            // Calcular las similitudes de coseno entre la consulta y los documentos
            double[] similitudes = new double[nombres.Length];
            for (int i = 0; i < nombres.Length; i++)
            {
                double[] vectorDocumento = new double[matriz.GetLength(1)];
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    vectorDocumento[j] = matriz[i, j];
                }
                similitudes[i] = CalcularSimilitudCoseno(vectorQuery, vectorDocumento);
            }
            return similitudes;
        }
    static double[,] matriz;
     static   string[] contenido;
     static   string[]nombres;
    static  ElAprobado()
    {
        
        // Leer los archivos de texto de la carpeta "content"
        (string[] contenidoTupla, string[] nombresTupla) = LeerArchivosDeTexto();
        contenido= contenidoTupla;
        nombres= nombresTupla;
        // Calcular la matriz de TF-IDF       
         matriz = CalcularMatrizTfIdf(contenido, nombres);

    } 
    
       
    public static SearchItem[] Busqueda(string query)
    {
     
    // Realizar la consulta
    Dictionary<string, List<string>> frecuencia = new Dictionary<string, List<string>>();
    double[] similitudes = CalcularSimilitudQuery(query, nombres, contenido, matriz, frecuencia);

    // Ordenar los resultados por similitud descendente
    //Array.Sort(similitudes, nombres);
    Array.Sort(similitudes);
    Array.Reverse(nombres);
    Array.Reverse(similitudes);

    SearchItem[] resultado = new SearchItem[similitudes.Length];

    // Mostrar los documentos más similares a la consulta
            for (int i = 0, j = 0; i < nombres.Length; i++)
         { 
            System.Console.WriteLine(similitudes[i]);
             if (similitudes[i] >= 0) // Se agregó la condición j < 10 para mostrar solo las 10 primeras palabras
             {
            string[] palabras = ObtenerPalabras(nombres[i]);
            string primerasPalabras = String.Join(" ", palabras.Take(10)); // Se agregó esta línea para obtener las 10 primeras palabras
        
            resultado[j] = new SearchItem(nombres[i], primerasPalabras, (float) similitudes[i]); // Se agregó el parámetro primerasPalabras para guardar las 10 primeras palabras
            j++;
            }
        }

    System.Console.WriteLine("aquiiiiiiiiiiiiiiiiii");


    return resultado;
    
    }
}
}