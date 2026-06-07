using EMS.Domain.Models.Answers;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace EMS.Domain.Models.Questions
{
    public class ChooseAllQuestion : Question
    {
        public ChooseAllQuestion( string _body, int _marks):base("Choose more than One", _body, _marks)
        {

        }

        public override bool CheckAnswer(List<Answer> studentAnswer)
        {
            if (studentAnswer.Count != CorrectAnswers.Count) return false;
            var correctIds = new HashSet<int>();
            foreach (var a in CorrectAnswers) correctIds.Add(a.Id);
            foreach (var a in studentAnswer) if (!correctIds.Contains(a.Id)) return false;
            return true;
        }

        public override object Clone()
        {
            var clone = new ChooseAllQuestion(Body, Marks);
            for (int i = 0; i < Answers.Count; i++) clone.Answers.Add((Answer)Answers[i].Clone());
            clone.CorrectAnswers = new List<Answer>(CorrectAnswers.Count);
            for (int i = 0; i < CorrectAnswers.Count; i++) clone.CorrectAnswers[i] = (Answer)CorrectAnswers[i].Clone();
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
