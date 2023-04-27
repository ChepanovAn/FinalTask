using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
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

    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "/Users/Анатолий/Desktop/Students";

            string studentsFile = "/Users/Анатолий/Desktop/Students.dat";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory("/Users/Анатолий/Desktop/Students");
            }

            string filePath = @"/Users/Анатолий/Desktop/Students/Group.txt";
          
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    Student[] studentsFromFile = ReadFile(studentsFile);

                    var tmp = GetStudentsGroupedByGroup(studentsFromFile);

                    for (int i = 0; i < studentsFromFile.Length; i++)
                    {
                        sw.WriteLine($"{studentsFromFile[i].Name}, {studentsFromFile[i].Group}, {studentsFromFile[i].DateOfBirth.ToString()}");
                    }
                }
            }

        }

        static Student[] ReadFile(string filePath)
        {
            Student[] newStudents;

            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                newStudents = (Student[])formatter.Deserialize(fs);
            }

            return newStudents;
        }

        static Dictionary<string, List<Student>> GetStudentsGroupedByGroup(Student[] students)
        {
            Dictionary<string, List<Student>> studentsDictionary = new Dictionary<string, List<Student>>();

            foreach(Student student in students)
            {
                if(studentsDictionary.ContainsKey(student.Group))
                {
                    var tmp = studentsDictionary[student.Group];

                    tmp.ToList().Add(student);

                    studentsDictionary[student.Group] = tmp;
                }
                else
                {
                    studentsDictionary.Add(student.Group, new List<Student> { student });
                }
            }

            return studentsDictionary;
        }
    }
}
