class Program3
{
    public static long directorySize = 0;
    //public static long directorySize2 = 0;

    static void Main(string[] args)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);

        if (directoryInfo.Exists)
        {
            GetDirectoriesSize(directoryInfo);
            GetFilesSize(directoryInfo);

            Console.WriteLine($"Исходный размер папки: {directorySize} байт");

            DeleteDirectories(directoryInfo);
            DeleteFiles(directoryInfo);


            long directorySize1 = directorySize;
            directorySize = 0;

            GetDirectoriesSize(directoryInfo);
            GetFilesSize(directoryInfo);

            long directorySize2 = directorySize;

            long difference = directorySize1 - directorySize2;

            Console.WriteLine($"Освобождено {difference} байт");

            Console.WriteLine($"Текущий размер папки: {directorySize2}");

        }
        else
        {
            Console.WriteLine($"Папки по пути {args[0]} не существует");
        }
    }

    private static void GetFilesSize(DirectoryInfo directoryInfo)
    {
        FileInfo[] files = directoryInfo.GetFiles();

        foreach (FileInfo file in files)
        {
            try
            {
                directorySize += file.Length;
            }
            catch (Exception ex)
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

    private static void DeleteDirectories(DirectoryInfo directoryInfo)
    {
        DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

        foreach (DirectoryInfo directoryInfo1 in directoryInfos)
        {
            DeleteDirectories(directoryInfo1);
            DeleteFiles(directoryInfo1);
            try
            {
                directoryInfo1.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка удаления папки " + ex);
            }
        }
    }

    private static void DeleteFiles(DirectoryInfo directoryInfo)
    {
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        foreach (FileInfo file in fileInfos)
        {
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка удаления файла " + ex);
            }
        }
    }
}