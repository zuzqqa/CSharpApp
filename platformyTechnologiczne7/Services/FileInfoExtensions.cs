using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public static class FileInfoExtensions{
    public static DateTime? GetOldestItem(this DirectoryInfo directoryInfo)
    {
        try
        {
            DateTime? oldestTime = directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Select(file => (DateTime?)file.LastWriteTime)
                .Min();

            return oldestTime;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Brak dostępu do pliku: {ex.Message}");
            return null;
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Nie znaleziono katalogu: {ex.Message}");
            return null;
        }
    }

    public static string GetDosAttributes(this FileSystemInfo fileSystemInfo)
    {
        return $"{((fileSystemInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? "r" : "-")}" +
        $"{((fileSystemInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive ? "a" : "-")}" +
        $"{((fileSystemInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "h" : "-")}" +
        $"{((fileSystemInfo.Attributes & FileAttributes.System) == FileAttributes.System ? "s" : "-")}";
    }
}