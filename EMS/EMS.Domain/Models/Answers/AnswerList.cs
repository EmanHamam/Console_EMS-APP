using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EMS.Domain.Models.Answers
{
    public class AnswerList
    {
        public List<Answer> Answers { get; set; }
        public int Count { get; set; }
        public AnswerList()
        {
            Answers = new List<Answer>();
            Count = 0;
        }
        

        public void Add(Answer answer)
        {
            if (answer == null) throw new ArgumentNullException("Anwer must have value");
            if (Answers.Contains(answer))
                throw new InvalidOperationException($"Answer already exists.");

            Answers.Add(answer);
            Count++;

        }
        public Answer  GetById(int id)
        {

            for (int i = 0; i < Answers?.Count; i++)
            {
                if (Answers[i].Id == id)
                    return Answers[i];
            }

            return null;

        }
        public Answer this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < Answers.Count)
                {
                    return Answers[idx];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            set {
                if (idx >= 0 && idx < Answers.Count)
                {
                    Answers[idx] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

            }
        }
    }
}
