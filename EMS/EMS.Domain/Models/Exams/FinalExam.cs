using EMS.Domain.Enums;
using EMS.Domain.Models.Questions;
using EMS.Domain.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Exams
{
    public class FinalExam : Exam
    {
        public FinalExam(int time, List<Question> questions, Subject subject) : base(time, questions, subject)
        {
        }
        public override void ShowExam()
        {
            Console.WriteLine("FINAL EXAM ");
            foreach (var question in Questions)
            {
                question.Display();
                Console.WriteLine();
            }
            if (Mode == ExamMode.Finished)
            {
                Show_student_answers();
                Show_final_grade();
            }
        }

       // public override object Clone() => new FinalExam(Time, (Question[])Questions.Clone(), Subject);
    }
}
