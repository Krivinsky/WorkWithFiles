using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program4
    {
        static void Main(string[] args)
        {
            string StudentsPath = @"C:\Users\Eugene\OneDrive\Рабочий стол\Students";

            Directory.CreateDirectory(StudentsPath);

            //FileInfo file = new FileInfo(args[0]);

            BinaryFormatter formatter = new BinaryFormatter();

            if (!File.Exists(args[0]))
            {
                throw new Exception("file not found");
            }

            using (var fs = new FileStream(args[0], FileMode.OpenOrCreate))
            {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                var students = (Student[])formatter.Deserialize(fs);

                foreach (Student student in students)
                {
                    Console.Write(student.Name + "\t" + student.Group + "\t" + student.DateOfBirth);
                    Console.WriteLine("");

                    if (!File.Exists (StudentsPath + @"\" + student.Group + ".txt"))
                    {
                    File.Create(StudentsPath + @"\" + student.Group + ".txt");
                    }

                    FileInfo fileInfo = new FileInfo(StudentsPath + @"\" + student.Group + ".txt");

                    using (StreamWriter sw = File.AppendText(fileInfo.FullName))
                    {
                        sw.Write(student.Name);
                        sw.Write(student.DateOfBirth + "\n");
                        sw.Close();
                    }
                    
                    
                    //using (var frWrite = new FileStream(fileInfo.Name, FileMode.OpenOrCreate))
                    //{
                    //    formatter.Serialize(frWrite, student);
                    //}
                }



#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }


        }
    }

    [Serializable]
     class Student
    {
         public string Name { get; set; }

        public string Group { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth) 
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }

    }
}