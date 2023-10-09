using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program4
    {
        static void Main(string[] args)
        {
            //string StudentsPath = @"C:\Users\Eugene\OneDrive\Рабочий стол\Students";
            string StudentsPath = @"C:\Users\Kira\Desktop\Students";
            Directory.CreateDirectory(StudentsPath);

            if (!File.Exists(args[0]))
            {
                throw new Exception("file not found");
            }

            var students = Serialize(args[0]);
            WriteFile(students, StudentsPath);

        }

        static Student[] Serialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                var students = (Student[])formatter.Deserialize(fs);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                return students;
            }
        }

        static void WriteFile(Student[] students, string StudentsPath)
        {
            
            foreach (Student student in students)
            {
                Console.Write(student.Name + "\t" + student.Group + "\t" + student.DateOfBirth);
                Console.WriteLine("");

                if (!File.Exists(StudentsPath + @"\" + student.Group + ".txt"))
                {
                    File.Create(StudentsPath + @"\" + student.Group + ".txt");
                }

                FileInfo fileInfo = new FileInfo(StudentsPath + @"\" + student.Group + ".txt");

                using (StreamWriter sw = fileInfo.AppendText())
                {
                    sw.WriteLine(student.Name + student.DateOfBirth + "\n");
                    sw.Close();
                }
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