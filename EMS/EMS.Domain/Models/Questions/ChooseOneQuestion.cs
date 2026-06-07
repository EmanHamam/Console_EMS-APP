using EMS.Domain.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Questions
{
    public class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion(string _body, int _marks) : base( "Choose One", _body, _marks)
        {
            //if (_answers.Count < 2)
            //    throw new ArgumentException("Answers must be at least 2 answers.");
        }

        public override bool CheckAnswer(List<Answer> studentAnswers)
        {
            if (studentAnswers == null) return false;
            return studentAnswers[0].Equals(CorrectAnswers[0]);
        }

        public override object Clone()
        {
            var clone = new ChooseOneQuestion(Body, Marks);
            for (int i = 0; i < Answers.Count; i++) clone.Answers.Add((Answer)Answers[i].Clone());
            for (int i = 0; i < CorrectAnswers.Count; i++) clone.CorrectAnswers.Add((Answer)CorrectAnswers[i].Clone());
            return clone;
        }

        public override void Display()
        {
            Console.WriteLine($"[ {Header} ] - {Marks} Marks");
            Console.WriteLine(Body);
            for (int i = 0; i < Answers.Count; i++)
                Console.WriteLine($"{i + 1}. {Answers[i].Text}");
        }
    }
}
