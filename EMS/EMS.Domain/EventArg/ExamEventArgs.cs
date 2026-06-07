using EMS.Domain.Models.Exams;
using EMS.Domain.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.EventArg
{
    public class ExamEventArgs :EventArgs
    {
        public Subject Subject { get; }
        public Exam Exam { get; }
        public ExamEventArgs(Subject s,Exam e)
        {
            Subject=s; Exam=e;
        }
    }

}
