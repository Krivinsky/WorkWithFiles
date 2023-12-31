﻿class Program2
{
    public static long directorySize = 0;

    static void Main(string[] args)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);

        if (directoryInfo.Exists)
        {
            GetDirectoriesSize(directoryInfo);
            GetFilesSize(directoryInfo);
        } else
        {
            Console.WriteLine($"Папки по пути {args[0]} не существует");
        }
        Console.WriteLine($"Размер папки: {directorySize}");
    }

    private static void GetFilesSize(DirectoryInfo directoryInfo)
    {
        FileInfo[] files = directoryInfo.GetFiles();

        foreach (FileInfo file in files)
        {
            try
            {
                directorySize += file.Length;
            } catch (Exception ex)
            {
                Console.WriteLine($"Файл - {file} недоступен" + ex);
            }
        }
    }

    private static void GetDirectoriesSize(DirectoryInfo directoryInfo)
    {
        DirectoryInfo[] currentDirectoryInfo = directoryInfo.GetDirectories();

        foreach (DirectoryInfo currentDirectory in currentDirectoryInfo)
        {
            GetDirectoriesSize(currentDirectory);
            GetFilesSize(currentDirectory);
        }
    }
}