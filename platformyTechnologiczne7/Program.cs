using platformyTechnologiczne7.Services;

namespace platformyTechnologiczne7;

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

        DirectoryMethods directoryMethods = new DirectoryMethods();

        directoryMethods.ContentsOfTheDirectory(directoryPath);
        directoryMethods.DisplayDirectoryTree(directoryPath);

        var sortedContent = new SortedDictionary<string, long>(new CustomStringComparer());

        string[] files = Directory.GetFiles(directoryPath);

        foreach (var file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            sortedContent[fileInfo.Name] = fileInfo.Length;
        }

        foreach (var kvp in sortedContent)
        {
            Console.WriteLine($"{kvp.Key,-10} {kvp.Value}");
        }

        AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);
    }
    /*var formatter = new BinaryFormatter();*/
}