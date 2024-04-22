namespace platformyTechnologiczne7.Services;

public class DirectoryMethods
{
    public void ContentsOfTheDirectory(string directoryPath, string indent = "")
    {
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Podany katalog nie istnieje.");
            return;
        }

        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

        Console.WriteLine($"Katalog: {directoryInfo.Name}");

        FileSystemInfo[] entries = directoryInfo.GetFileSystemInfos();

        foreach (var entry in entries)
        {
            string entryType = entry switch
            {
                FileInfo f => "-",
                DirectoryInfo d => "a",
                _ => "Unknown"
            };

            string permissions = $"{entry.GetDosAttributes()}";

            string owner = entry switch 
            {
                FileInfo f => f.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString().Split('\\').Last(),
                DirectoryInfo d => d.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString().Split('\\').Last(),
                _ => "Unknown"
            };

            string group = entry switch
            {
                FileInfo f => f.GetAccessControl().GetGroup(typeof(System.Security.Principal.NTAccount)).ToString().Split('\\').Last(),
                DirectoryInfo d => d.GetAccessControl().GetGroup(typeof(System.Security.Principal.NTAccount)).ToString().Split('\\').Last(),
                _ => "Unknown"
            };

            string size = entry switch
            {
                FileInfo f => ((FileInfo)entry).Length.ToString(),
                DirectoryInfo d => Directory.GetFileSystemEntries(entry.FullName).Length.ToString(),
                _ => "Unknown"
            };

            string lastModified = entry.LastWriteTime.ToString("MMM dd HH:mm");

            Console.WriteLine($"{entryType}{permissions,-3} {owner,-2} {group,-10} {size,-10} {lastModified,-10} {entry.Name}\n");
                
        }

        Console.WriteLine(); 

        foreach (var subDir in entries.Where(e => (e.Attributes & FileAttributes.Directory) == FileAttributes.Directory))
        {
            ContentsOfTheDirectory(subDir.FullName, indent + "   ");
        }
    }

    private object GetDirectorySize(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath); 
        long totalSize = files.Length;

        return $"({totalSize} {(totalSize == 1 ? "file" : "files")})";
    }

    public void DisplayDirectoryTree(string directoryPath, string indent = "", bool isLast = true)
    {
        Dictionary<string, string> colorCodes = new Dictionary<string, string>()
        {
            { ".txt", "\u001b[38;5;111m" }, 
            { ".exe", "\u001b[32;1m" },
            { ".jpg", "\u001b[38;5;209m" } 
        };

        string hiddenFileColor = "\u001b[31m";

        string reset = "\u001b[0m";
        string directoryColor = "\u001b[34m";
        string indentColor = "\u001b[37m";
        string italic = "\u001b[3m";

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"{reset}\u001b[31;1m└── Katalog nie istnieje.{reset}");
            return;
        }

        if (indent != "")
            Console.Write(indent);

        string lineConnector = isLast ? "└── " : "├── ";
        string verticalConnector = isLast ? "    " : "│   ";

        Console.Write($"{indentColor}{lineConnector}{reset}"); 

        Console.WriteLine($"{directoryColor}{Path.GetFileName(directoryPath)}{reset} {italic}{GetDirectorySize(directoryPath)}");

        string[] files = Directory.GetFiles(directoryPath);
        string[] directories = Directory.GetDirectories(directoryPath);

        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            string fileSize = $"({new FileInfo(file).Length} bytes)";
            string extension = Path.GetExtension(file);
            string colorCode = colorCodes.TryGetValue(extension, out var code) ? code : reset;
            string fileAttributes = new FileInfo(file).GetDosAttributes();
            if ((File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden)
                colorCode = hiddenFileColor;

            Console.Write($"{reset}{indent}{(isLast ? "    " : "│   ")}{(i == files.Length - 1 && directories.Length == 0 ? "└── " : "├── ")}{colorCode}{Path.GetFileName(file)}{reset} {italic}{fileSize} {fileAttributes}\n");
        }

        for (int j = 0; j < directories.Length; j++)
        {
            string newIndent = $"{indent}{verticalConnector}";
            DisplayDirectoryTree(directories[j], newIndent, j == directories.Length - 1);
        }
    }


}