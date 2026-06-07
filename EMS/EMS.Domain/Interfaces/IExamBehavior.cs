using EMS.Domain.Models.Answers;
using EMS.Domain.Models.Questions;
using System.Collections.Generic;

namespace EMS.Domain.Interfaces
{
    
    public interface IExamBehavior
    {
        bool IsTimeUp { get; }
        void Start();

        void Finish();

        int CorrectExam();

        //int GetTimeLimit();

        //int GetRemainingSeconds();


        //void SaveResultsToFile(int score, int total);
    }
}