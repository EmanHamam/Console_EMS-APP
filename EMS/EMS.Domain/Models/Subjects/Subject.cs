using EMS.Domain.Models.Exams;
using EMS.Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Subjects
{
    public class Subject
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; set; }

        public Subject(string _name) 
        {
            Name = _name;
            EnrolledStudents = new List<Student>();
        }

        public void Enroll (Student student)
        {
            if(EnrolledStudents.Contains(student))
            {
                throw new Exception("Student already enrolled.");
            }
            EnrolledStudents.Add(student);

        }
        public void NotifyStudents(Exam exam) //event
        {
            //// This is handled by the event subscription in Student class
        }

    }
}
