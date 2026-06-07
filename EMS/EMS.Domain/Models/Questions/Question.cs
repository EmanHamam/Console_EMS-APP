using EMS.Domain.Models.Answers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Questions
{
    public abstract class Question
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }
        public AnswerList Answers { get; set; }
        public List<Answer> CorrectAnswers { get; set; }

        protected Question( string header, string body, int marks)
        {
            //Id = id;
            Header = header??"";
            Body = body??"";
            if (marks <= 0)
                throw new ArgumentException("Marks must be > 0.");
            Marks = marks;
            Answers = new AnswerList();
            CorrectAnswers =new List<Answer>();
        }

        public abstract void Display();
        public abstract bool CheckAnswer(List<Answer> studentAnswers);
        public override string ToString() => $"{Header}: {Body} ({Marks} Marks)";
        
        public override bool Equals(object? obj)
        {
            Question R=obj as Question;

            if (R==null) return false;

            if(this.GetType() != R.GetType()) return false;

            if(Object.ReferenceEquals(this, R)) return true;
            
            return Header==R.Header && Body==R.Body;

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Header,Body);
        }


        public abstract object Clone();
    }
}
