using System.IO;

class Program1
{
    static void Main(string[] args)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);

        if (directoryInfo.Exists)
        {
            DeleteDirectories(directoryInfo);
            DeleteFiles(directoryInfo);
        } 
        else
        {
            Console.WriteLine($"папки по пути: {args[0]} не существует");
        }
    }

    private static void DeleteDirectories(DirectoryInfo directoryInfo)
    {
        DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

        foreach (DirectoryInfo directoryInfo1 in directoryInfos)
        {
            DeleteDirectories(directoryInfo1);
            DeleteFiles(directoryInfo1);
            // сделал CreationTime т.к. если использовать LastAccessTime папки не удаляются ведь к ним только что был доступ
            if (directoryInfo1.CreationTime < DateTime.Now.AddMinutes(-30))
            {
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
    }

    private static void DeleteFiles(DirectoryInfo directoryInfo)
    {
        FileInfo[] fileInfos = directoryInfo.GetFiles();

        foreach (FileInfo file in fileInfos)
        {
            if (file.LastAccessTime < DateTime.Now.AddMinutes(-30))
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
}