using platformyTechnologiczne7.Services;
using System.Runtime.Serialization.Formatters.Binary;

namespace platformyTechnologiczne7;

class Program
{
    static void Main(string[] args)
    {
        AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);

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

        Console.WriteLine();

        foreach (var kvp in sortedContent)
        {
            Console.WriteLine($"{kvp.Key,-10} -> {kvp.Value} bytes");
        }

        var formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream("sortedContent.bin", FileMode.Create))
        {
            formatter.Serialize(fs, sortedContent);
        }

        SortedDictionary<string, long> deserializedContent;

        using(FileStream fs = new FileStream("sortedContent.bin", FileMode.Open))
        {
            deserializedContent = formatter.Deserialize(fs) as SortedDictionary<string, long> ?? throw new InvalidOperationException();
        }

        Console.WriteLine("\nZawartość kolekcji po deserializacji:");
        foreach (var kvp in deserializedContent)
        {
            Console.WriteLine($"{kvp.Key,-10} {kvp.Value} bytes");
        }

    }
}