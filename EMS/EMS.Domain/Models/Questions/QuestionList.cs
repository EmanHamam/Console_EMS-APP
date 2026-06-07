using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Models.Questions
{
    public class QuestionList:List<Question>
    {
        private readonly string _logFile;
        public QuestionList(string _fileName)
        {
        
            _logFile = _fileName;
        }
        //add
        public new void Add(Question question)
        {
            base.Add(question);
            LogToFile(question);
        }

        private void LogToFile(Question q)
        {
            using (StreamWriter sw = new StreamWriter(_logFile, true))
            {
                sw.WriteLine($"[{DateTime.Now}] Added: {q}");
            }
        }
    }
}
