using EMS.Domain.Models.Questions;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace EMS.Domain.Models.Answers
{
    public class Answer :IComparable<Answer>,ICloneable
    {
        public int Id {get; set; }
        public string Text { get; set; }
        public Answer() { }
        public Answer(int _id,string _text)
        {
            Id= _id;
            Text= _text??"";
        }
        public override string ToString() => $"{Id} - {Text}";
      

        public override bool Equals(object? obj)
        {
            Answer R = obj as Answer;

            if (R == null) return false;

            if (this.GetType() != R.GetType()) return false;

            if (Object.ReferenceEquals(this, R)) return true;

            return Id.Equals(R.Id);

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id,Text);
        }

        public int CompareTo(Answer? other)
        {
            if(other == null) return 1;
            else if(other == this) return 0;
            else return Id.CompareTo(other.Id);
            
        }

        public object Clone()
        {
            return new Answer(Id,Text);
        }
    }
}
