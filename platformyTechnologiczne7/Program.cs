using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Nie podano sciezki katalogu.");
            return;
        }

        string directoryPath = args[0];
        var directoryMethods = new DirectoryMethods();

        directoryMethods.ContentsOfTheDirectory(directoryPath);
        directoryMethods.DisplayDirectoryTree(directoryPath);

        AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);
    }

    /*var formatter = new BinaryFormatter();*/
}

