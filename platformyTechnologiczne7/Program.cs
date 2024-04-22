
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nie podano sciezki katalogu.");
        }

        string directoryPath = args[0];

        Console.WriteLine("=====ZADANIE 1=====\n");
        ContentsOfTheDirectory(directoryPath);

        Console.WriteLine("=====ZADANIE 2=====\n");
        ContentsOfTheDirectoryRecursively(directoryPath);

        AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);
    }

    static void ContentsOfTheDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Podany katalog nie istnieje.");
        }

        string[] files = Directory.GetFiles(directoryPath);
        string[] directories = Directory.GetDirectories(directoryPath);

        Console.WriteLine("Pliki w katalogu: ");
        foreach (string file in files) { Console.WriteLine("    " + Path.GetFileName(file)); }

        Console.WriteLine("\nPodkatalogi: ");
        foreach (string d in directories) { Console.WriteLine("    " + Path.GetFileName(d)); }

        Console.WriteLine("\n");
    }

    static void ContentsOfTheDirectoryRecursively(string directoryPath, string indent = " ")
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Podany katalog nie istnieje.");
        }

        string[] files = Directory.GetFiles(directoryPath);
        string[] directories = Directory.GetDirectories(directoryPath);

        if (files.Length > 0)
        {
            Console.WriteLine($"{indent}Pliki w katalogu: ");
            foreach (var file in files)
            {
                Console.WriteLine($"{indent}- {Path.GetFileName(file)}");
            }
        }

        if (directories.Length > 0)
        {
            Console.WriteLine($"\n{indent}Podkatalogi: ");

            foreach (var d in directories)
            {
                Console.WriteLine($"{indent}- {Path.GetFileName(d)}");
                ContentsOfTheDirectoryRecursively(d, indent + "   "); // Wywołanie rekurencyjne z większym wcięciem
            }
        }
        
    }

/*var formatter = new BinaryFormatter();*/
}

