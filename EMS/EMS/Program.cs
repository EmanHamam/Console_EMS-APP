using Ems.Infrastructure.Helpers;
using EMS.Domain.Models.Answers;
using EMS.Domain.Models.Exams;
using EMS.Domain.Models.Questions;
using EMS.Domain.Models.Students;
using EMS.Domain.Models.Subjects;
using System.Drawing;

namespace EMS
{
    internal class Program
    {
        static List<Question> GetRandomQuestions(List<Question> questions)
        {
            var rng = new Random();
            var Questions = (questions ?? new List<Question>())
                        .OrderBy(_ => rng.Next())
                        .Take(5)
                        .ToList();
            return Questions;
        }
        static void Main(string[] args)
        {
            
            // 1. Setup
            DataSeeder.Seed(out Subject subject, out Student[] students, out List<Question> questions);

            // 2. Create Exams
            
            PracticeExam practiceExam = new PracticeExam(1, GetRandomQuestions(questions), subject);
            FinalExam finalExam = new FinalExam(5, GetRandomQuestions(questions), subject);

            // 3. Subscribe Students to Events
            foreach (var s in students)
            {
                practiceExam.ExamStarted += s.OnExamStarted;
                finalExam.ExamStarted += s.OnExamStarted;
            }

            //4.User Interaction
            while (true)
            {

                string[] menuOptions = { "Practice Exam", "Final Exam", "Exit" };
                int choice = ConsoleHelper.InteractiveMenu("Main Menu", menuOptions);

                if (choice == 3) break;


                Exam selectedExam = (choice == 1) ? practiceExam : finalExam;
                RunExam(selectedExam);
            }

            ConsoleHelper.TypingEffect("Thank you for using the Examination Management System. Goodbye!", 30);
        }

        static void RunExam(Exam exam)
        {
            Console.Clear();
            ConsoleHelper.WriteHeader($"{exam.Subject.Name} – {exam.GetType().Name}");
            ConsoleHelper.TypingEffect($"\nTime limit: {exam.Time} minutes | Questions: {exam.Questions.Count}", 25);
            ConsoleHelper.TypingEffect("Preparing your exam environment...", 30);

            exam.Start();
            Console.WriteLine("\n  All enrolled students notified.");
            Console.WriteLine("\n  Press any key to begin answering questions...");
            
            Console.ReadKey(true);

            
            var userAnswers = new Dictionary<Question, List<Answer>>();
            foreach (var q in exam.Questions) userAnswers[q] = new List<Answer>();

            int currentQ = 0;                     
            bool submitted = false;

            
            while (!submitted && !exam.IsTimeUp)
            {
                RenderExamScreen(exam, userAnswers, currentQ);

                
                 var key = Console.ReadKey(true).Key;

                    //for moving
                 switch (key)
                 {

                     case ConsoleKey.UpArrow:
                     case ConsoleKey.LeftArrow:
                         currentQ = (currentQ == 0) ? exam.Questions.Count : currentQ - 1;
                         break;
                     case ConsoleKey.DownArrow:
                     case ConsoleKey.RightArrow:
                         currentQ = (currentQ == exam.Questions.Count) ? 0 : currentQ + 1;
                         break;

                     case ConsoleKey.Enter:
                         if (currentQ == exam.Questions.Count)
                         {
                             submitted = true;
                         }
                         else
                         {
                             AnswerQuestion(exam.Questions[currentQ], userAnswers, exam);
                         }
                         break;

                     default:
                         if (key >= ConsoleKey.D1 && key <= ConsoleKey.D9)
                         {
                             int idx = key - ConsoleKey.D1;
                             if (idx < exam.Questions.Count) currentQ = idx;
                         }
                         break;
                 
                 }
                
            }

            if (exam.IsTimeUp)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTIME IS UP!  Your answers have been auto-submitted.");
                Console.ResetColor();
                Thread.Sleep(2000);
            }

            foreach (var kv in userAnswers)
                exam.QuestionAnswerDictionary[kv.Key] = kv.Value;

            Console.Clear();
            ConsoleHelper.TypingEffect("Submitting your answers and calculating grade...", 40);
            Thread.Sleep(800);
            exam.Finish();
            ShowResults(exam);
        }

        static void RenderExamScreen(Exam exam, Dictionary<Question, List<Answer>> userAnswers, int focusIdx)
        {
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"  {exam.Subject.Name.ToUpper()} – {exam.GetType().Name.ToUpper()}");
            Console.WriteLine(new string('=', 60));
            Console.ResetColor();

            Console.WriteLine("\n  use arrows to  Navigate   |   Enter  to Answer /Submit \n");

            int totalMarks = exam.Questions.Sum(q => q.Marks);
            int answered = userAnswers.Count(kv => kv.Value.Count > 0);
            Console.WriteLine($"  Progress: {answered} / {exam.Questions.Count} answered   |   Total marks: {totalMarks}\n");
            Console.WriteLine(new string('-', 60));

            //showing Questions menu 
            for (int i = 0; i < exam.Questions.Count; i++)
            {
                var q = exam.Questions[i];
                bool isFocused = (i == focusIdx);
                bool hasAnswer = userAnswers[q].Count > 0;
                string answerPreview = hasAnswer
                    ? $"  → {string.Join(", ", userAnswers[q].Select(a => a.Text))}"
                    : "";

                if (isFocused)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = hasAnswer ? ConsoleColor.Green : ConsoleColor.Gray;
                }

                Console.Write($"  [Q{i + 1}]. {q.Body,-45} ({q.Marks}m)");
                if (hasAnswer && !isFocused)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(answerPreview);
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 60));

            // Submit
            bool submitFocused = (focusIdx == exam.Questions.Count);
            if (submitFocused)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            Console.WriteLine($"\n  [ SUBMIT / FINISH EXAM ]");
            Console.ResetColor();
        }

        
        static void AnswerQuestion(Question q, Dictionary<Question, List<Answer>> userAnswers, Exam exam)
        {
            if (q is TrueFalseQuestion || q is ChooseOneQuestion)
            {
                string[] options = q.Answers.Answers.Select(a => a.Text).ToArray();
                int ans = ConsoleHelper.InteractiveMenu($"Q: {q.Body}  ({q.Marks} marks)", options);
                if(ans!=-1)
                    userAnswers[q] = new List<Answer> { q.Answers[ans - 1] };
            }
            else if (q is ChooseAllQuestion)
            {
                string[] options = q.Answers.Answers.Select(a => a.Text).ToArray();
                int[] selected = ConsoleHelper.InteractiveMultiSelect($"Q: {q.Body}  ({q.Marks} marks)", options);
                if (selected != null) 
                    userAnswers[q] = selected.Select(idx => q.Answers[idx - 1]).ToList();
            }
        }


        static void DisplayAnswer(List<Answer> ans, string header)
        {
            if (header == "Choose One" || header == "True/False") Console.WriteLine(ans[0].Text);
            else if (header == "Choose more than One") Console.WriteLine(string.Join(", ", ans));
            else Console.WriteLine("No Answer");
        }
        static void ShowResults(Exam exam)
        {
            Console.Clear();
            

            if (exam is PracticeExam)
            {
                ConsoleHelper.WriteHeader("Exam Results Summary");

                int score = exam.CorrectExam();
                int total = exam.Questions.Sum(q => q.Marks);
                double pct = (double)score / total * 100;

                Console.WriteLine($"\n  Final Score : {score} / {total}  ({pct:F1}%)");

                // Progress bar
                Console.Write("  Grade       : [");
                int barLen = 30;
                int filled = (int)(pct / 100 * barLen);
                Console.ForegroundColor = pct >= 60 ? ConsoleColor.Green : ConsoleColor.Red;
                for (int i = 0; i < barLen; i++) Console.Write(i < filled ? "█" : "░");
                Console.ResetColor();
                Console.WriteLine("]");

                Console.WriteLine();
                Console.ForegroundColor = pct >= 90 ? ConsoleColor.Green
                                        : pct >= 60 ? ConsoleColor.Yellow
                                        : ConsoleColor.Red;
                string grade = pct >= 90 ? "A – Excellent!" : pct >= 75 ? "B – Good" : pct >= 50 ? "C – Pass" : "F – Fail";
                Console.WriteLine($"  Result      : {grade}");
                Console.ResetColor();

                Console.WriteLine(new string('-', 60));

                ConsoleHelper.WriteSuccess("\n  Practice Mode: Detailed Review");
                foreach (var q in exam.Questions)
                {
                    exam.QuestionAnswerDictionary.TryGetValue(q, out var given);
                    bool correct = q.CheckAnswer(given);

                    Console.WriteLine($"\n  Q: {q.Body}");
                    Console.Write("Your Answer: ");
                    DisplayAnswer(exam.QuestionAnswerDictionary.GetValueOrDefault(q), q.Header);
                   

                    Console.ForegroundColor = correct ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(correct ? " Correct!" : "  Incorrect.");
                    Console.ResetColor();

                    if (!correct && q.CorrectAnswers != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"  Correct Ans : {string.Join(", ", q.CorrectAnswers.Select(a => a.Text))}");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                ConsoleHelper.WriteSuccess("\n  Final Exam: Results Submitted.");
                foreach (var q in exam.Questions)
                {
                    exam.QuestionAnswerDictionary.TryGetValue(q, out var given);
                    bool correct = q.CheckAnswer(given);

                    Console.WriteLine($"\n  Q: {q.Body}");
                    Console.Write("Your Answer: ");
                    DisplayAnswer(exam.QuestionAnswerDictionary.GetValueOrDefault(q), q.Header);
                }
            }

            Console.WriteLine();
            exam.SaveResultsToFile();

            Console.WriteLine("\n  Press any key to return to menu...");
            Console.ReadKey(true);
        }

    }
}
