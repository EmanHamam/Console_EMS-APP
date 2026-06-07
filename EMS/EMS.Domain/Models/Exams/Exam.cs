using EMS.Domain.Delegates;
using EMS.Domain.Enums;
using EMS.Domain.EventArg;
using EMS.Domain.Models.Answers;
using EMS.Domain.Models.Questions;
using EMS.Domain.Models.Subjects;
using EMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Timers;


namespace EMS.Domain.Models.Exams
{
    public abstract class Exam : IExamBehavior, ICloneable, IComparable<Exam>
    {
        public int Id { get; set; }
        public int Time { get; set; } //mins
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }
        public Dictionary<Question, List<Answer>> QuestionAnswerDictionary { get; set; }
        public Subject Subject { get; set; }
        private System.Timers.Timer examTimer;
        private DateTime startTime;
        private ExamMode mode;
        public ExamMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                if (mode == ExamMode.Starting)
                    onExamStarted(new ExamEventArgs(Subject, this));
            }
        }


        protected Exam(int time, List<Question> questions, Subject subject)
        {
            Questions = questions ?? new List<Question>();
            NumberOfQuestions=questions?.Count ?? 0;
            Mode = ExamMode.Queued;
            Subject= subject;
            Time = time;
            QuestionAnswerDictionary = new Dictionary<Question, List<Answer>>();
        }
        public abstract void ShowExam();
        
        public override string ToString() => $"{Subject} :: {NumberOfQuestions} :: {Time}";
      
        public override bool Equals(object? obj)
        {
            Exam R = obj as Exam;

            if (R == null) return false;

            if (this.GetType() != R.GetType()) return false;

            if (Object.ReferenceEquals(this, R)) return true;

            return Time == R.Time && NumberOfQuestions == R.NumberOfQuestions &&Subject?.Name==R.Subject?.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject?.Name, Time);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(Exam? other) 
        {
            if(other == null) return 1;

            else if(other == this) return 0;
            else
            {
                if(Time != other.Time)
                    return Time.CompareTo(other.Time);
                else return NumberOfQuestions.CompareTo(other.NumberOfQuestions);
            }

        }
        public void Show_student_answers()
        {
            foreach (var questtionAnswer in QuestionAnswerDictionary)
            {
                Console.WriteLine($"{questtionAnswer.Key.ToString()}");
                foreach (var ans in questtionAnswer.Value)
                {
                    Console.WriteLine(ans.ToString());
                }
            }
        }
        public void Show_correct_answers()
        {
            foreach (var questtionAnswer in QuestionAnswerDictionary)
            {
                foreach (var ans in questtionAnswer.Key.CorrectAnswers)
                {
                    Console.WriteLine(ans.ToString());
                }

                Console.WriteLine();

            }
        }
        public void Show_final_grade()
        {
            Console.WriteLine($"Total Grade {CorrectExam()}");
        }

        //event
        public event ExamStartedHandler ExamStarted;
        protected virtual void onExamStarted(ExamEventArgs e)
        {
            ExamStarted?.Invoke(this, e);
        }



        //Implement IExamBehavior
        public bool IsTimeUp
        {
            get
            {
                return Mode == ExamMode.Finished ||
                        (DateTime.Now - startTime).TotalSeconds >= Time * 60;
            }
        }

        public virtual void Start()
        {
            if (Mode != ExamMode.Queued)
                throw new InvalidOperationException("Exam cannot be started.");
            startTime = DateTime.Now;
            StartTimer();
            Mode = ExamMode.Starting;
            Console.WriteLine("Exam Started");
        }
        public virtual void Finish()
        {
            

            StopTimer();
            Mode = ExamMode.Finished;
            Console.WriteLine("Exam Finished.");
            SaveResultsToFile();
        }
        public int CorrectExam()
        {
            int totalCount = 0;
            foreach (var q in Questions)
            {
                if ((QuestionAnswerDictionary.ContainsKey(q)) && q.CheckAnswer(QuestionAnswerDictionary[q]))
                    totalCount += q.Marks;

            }
            return totalCount;
        }


        private void StartTimer()
        {
            examTimer = new System.Timers.Timer (Time * 60 * 1000);

            examTimer.Elapsed += (sender, e) =>
            {
                Console.WriteLine("\nTime is up!");

                Finish();
            };

            examTimer.AutoReset = false;

            examTimer.Start();
        }

        private void StopTimer()
        {
            examTimer?.Stop();
            examTimer?.Dispose();
        }

        public void SaveResultsToFile()
        {
            string fileName = $"ExamResults_{Subject?.Name}_{DateTime.Now:yyyyMMddHHmmss}.txt";

            using StreamWriter writer = new StreamWriter(fileName, true);

            writer.WriteLine("Exam Results");
            writer.WriteLine("--------------------------");
            writer.WriteLine($"Subject: {Subject?.Name}");
            writer.WriteLine($"Total Questions: {NumberOfQuestions}");
            writer.WriteLine($"Exam Time: {Time} minutes");
         //   writer.WriteLine($"Final Grade: {CorrectExam()}");
            writer.WriteLine();

            foreach (var pair in QuestionAnswerDictionary)
            {
                writer.WriteLine(pair.Key.ToString());

                writer.WriteLine("Student Answers:");

                foreach (var ans in pair.Value)
                    writer.WriteLine(ans.ToString());

                writer.WriteLine();
            }

            writer.WriteLine("=====================================");
        }
    }
}
