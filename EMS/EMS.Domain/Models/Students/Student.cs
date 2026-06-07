using EMS.Domain.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Students
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(int _id,string _name)
        {
            Id= _id;
            Name= _name;
        }
        public override string ToString() => $"ID : {Id} ,Name : {Name}";
      
        public void OnExamStarted(object sender, ExamEventArgs e) 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[NOTIFICATION] Student {Name} (ID: {Id}): The {e.Subject.Name} exam has started!");
            Console.ResetColor();
        }

    }
}
