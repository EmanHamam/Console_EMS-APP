using EMS.Domain.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Questions
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion( string _body, int _marks, bool isTrue) :base("True/False", _body, _marks)
        {
            Answers.Add(new Answer(1, "True"));
            Answers.Add(new Answer(2, "False"));
            Answer correct = isTrue ? Answers[0] : Answers[1];
            CorrectAnswers = new List<Answer> { correct };
        }

        public override bool CheckAnswer(List<Answer> studentAnswers)
        {
            if(studentAnswers==null) return false;
            return studentAnswers[0].Equals(CorrectAnswers[0]);
        }

        public override object Clone()
        {
            return new TrueFalseQuestion(Body, Marks, CorrectAnswers[0].Id==1);
        }

        public override void Display()
        {
            Console.WriteLine($"[ {Header} ] - {Marks} Marks");
            Console.WriteLine(Body);
            Console.WriteLine("1. True\t2. False");
        }
    }
}
