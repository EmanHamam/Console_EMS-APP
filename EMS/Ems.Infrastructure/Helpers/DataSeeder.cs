using EMS.Domain.Models.Answers;
using EMS.Domain.Models.Questions;
using EMS.Domain.Models.Students;
using EMS.Domain.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Infrastructure.Helpers
{
    public static class DataSeeder
    {
        public static void Seed(out Subject subject, out Student[] students, out List<Question> questions)
        {
            subject = new Subject("C# Advanced Programming");

            students = new Student[]
            {
                new Student(101, "Eman"),
                new Student(102, "Mohamed"),
                new Student(103, "Ahmed"),
                new Student(104, "Ali")

            };

            foreach (var s in students) subject.Enroll(s);

            var qList = new QuestionList($"questions_log_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            // Q1: T/F
            qList.Add(new TrueFalseQuestion("C# is a functional-only language.", 5, false));
            qList.Add(new TrueFalseQuestion("Interfaces in C# can contain method implementations via default interface methods (.NET 8+).", 5, true));
            qList.Add(new TrueFalseQuestion("The 'sealed' keyword prevents a class from being inherited.", 5, true));
            qList.Add(new TrueFalseQuestion("Value types in C# are stored on the heap.", 5, false));
            qList.Add(new TrueFalseQuestion("'async' and 'await' keywords are used for asynchronous programming in C#.", 5, true));
            qList.Add(new TrueFalseQuestion("A struct can inherit from another struct in C#.", 5, false));
            qList.Add(new TrueFalseQuestion("String is a reference type in C#.", 5, true));
            qList.Add(new TrueFalseQuestion("The 'using' statement is only used for importing namespaces.", 5, false));
            qList.Add(new TrueFalseQuestion("Generics in C# provide type safety at compile time.", 5, true));
            qList.Add(new TrueFalseQuestion("An abstract class can be instantiated directly.", 5, false));
            // Q2: Choose One
            var q11 = new ChooseOneQuestion("Which keyword is used for inheritance in C#?", 10);
            q11.Answers.Answers.AddRange(new[] { new Answer(1, ":"), new Answer(2, "extends"), new Answer(3, "implements"), new Answer(4, "inherits") });
            q11.CorrectAnswers = new List<Answer> { q11.Answers[0] };
            qList.Add(q11);

            var q12 = new ChooseOneQuestion("Which access modifier makes a member visible only within the same class?", 10);
            q12.Answers.Answers.AddRange(new[] { new Answer(1, "public"), new Answer(2, "protected"), new Answer(3, "private"), new Answer(4, "internal") });
            q12.CorrectAnswers = new List<Answer> { q12.Answers[2] };
            qList.Add(q12);

            var q13 = new ChooseOneQuestion("What does LINQ stand for?", 10);
            q13.Answers.Answers.AddRange(new[] { new Answer(1, "Language Integrated Query"), new Answer(2, "Linked Index Query"), new Answer(3, "List Integrated Queue"), new Answer(4, "Language Index Queue") });
            q13.CorrectAnswers = new List<Answer> { q13.Answers[0] };
            qList.Add(q13);

            var q14 = new ChooseOneQuestion("Which collection type ensures unique elements in C#?", 10);
            q14.Answers.Answers.AddRange(new[] { new Answer(1, "List<T>"), new Answer(2, "Queue<T>"), new Answer(3, "HashSet<T>"), new Answer(4, "Stack<T>") });
            q14.CorrectAnswers = new List<Answer> { q14.Answers[2] };
            qList.Add(q14);

            var q15 = new ChooseOneQuestion("What is the base class of all classes in C#?", 10);
            q15.Answers.Answers.AddRange(new[] { new Answer(1, "Base"), new Answer(2, "Object"), new Answer(3, "Class"), new Answer(4, "System") });
            q15.CorrectAnswers = new List<Answer> { q15.Answers[1] };
            qList.Add(q15);

            var q16 = new ChooseOneQuestion("Which keyword is used to prevent method overriding in C#?", 10);
            q16.Answers.Answers.AddRange(new[] { new Answer(1, "static"), new Answer(2, "readonly"), new Answer(3, "sealed"), new Answer(4, "const") });
            q16.CorrectAnswers = new List<Answer> { q16.Answers[2] };
            qList.Add(q16);

            var q17 = new ChooseOneQuestion("What does the 'ref' keyword do in C#?", 10);
            q17.Answers.Answers.AddRange(new[] { new Answer(1, "Creates a copy of a value"), new Answer(2, "Passes variable by reference"), new Answer(3, "Declares a reference type"), new Answer(4, "Marks a return type") });
            q17.CorrectAnswers = new List<Answer> { q17.Answers[1] };
            qList.Add(q17);

            var q18 = new ChooseOneQuestion("Which design pattern provides a single instance of a class?", 10);
            q18.Answers.Answers.AddRange(new[] { new Answer(1, "Factory"), new Answer(2, "Observer"), new Answer(3, "Singleton"), new Answer(4, "Decorator") });
            q18.CorrectAnswers = new List<Answer> { q18.Answers[2] };
            qList.Add(q18);

            var q19 = new ChooseOneQuestion("What is the correct way to declare a nullable int in C#?", 10);
            q19.Answers.Answers.AddRange(new[] { new Answer(1, "int? x"), new Answer(2, "nullable int x"), new Answer(3, "int x = null"), new Answer(4, "Nullable x") });
            q19.CorrectAnswers = new List<Answer> { q19.Answers[0] };
            qList.Add(q19);

            var q20 = new ChooseOneQuestion("Which method is used to convert a string to an integer in C#?", 10);
            q20.Answers.Answers.AddRange(new[] { new Answer(1, "Convert.ToInt()"), new Answer(2, "int.Parse()"), new Answer(3, "Int.Convert()"), new Answer(4, "string.ToInt()") });
            q20.CorrectAnswers = new List<Answer> { q20.Answers[1] };
            qList.Add(q20);

            // Q3: Choose All
            // ── CHOOSE ALL (10) ────────────────────────────────────────────
            var q21 = new ChooseAllQuestion("Which of these are SOLID principles?", 15);
            q21.Answers.Answers.AddRange(new[] { new Answer(1, "Single Responsibility"), new Answer(2, "Open/Closed"), new Answer(3, "Multiple Inheritance"), new Answer(4, "Liskov Substitution") });
            q21.CorrectAnswers = new List<Answer> { q21.Answers[0], q21.Answers[1], q21.Answers[3] };
            qList.Add(q21);

            var q22 = new ChooseAllQuestion("Which of the following are value types in C#?", 15);
            q22.Answers.Answers.AddRange(new[] { new Answer(1, "int"), new Answer(2, "string"), new Answer(3, "bool"), new Answer(4, "class") });
            q22.CorrectAnswers = new List<Answer> { q22.Answers[0], q22.Answers[2] };
            qList.Add(q22);

            var q23 = new ChooseAllQuestion("Which are valid C# access modifiers?", 15);
            q23.Answers.Answers.AddRange(new[] { new Answer(1, "public"), new Answer(2, "visible"), new Answer(3, "protected"), new Answer(4, "internal") });
            q23.CorrectAnswers = new List<Answer> { q23.Answers[0], q23.Answers[2], q23.Answers[3] };
            qList.Add(q23);

            var q24 = new ChooseAllQuestion("Which of these are OOP pillars?", 15);
            q24.Answers.Answers.AddRange(new[] { new Answer(1, "Encapsulation"), new Answer(2, "Compilation"), new Answer(3, "Inheritance"), new Answer(4, "Polymorphism") });
            q24.CorrectAnswers = new List<Answer> { q24.Answers[0], q24.Answers[2], q24.Answers[3] };
            qList.Add(q24);

            var q25 = new ChooseAllQuestion("Which keywords are used for exception handling in C#?", 15);
            q25.Answers.Answers.AddRange(new[] { new Answer(1, "try"), new Answer(2, "error"), new Answer(3, "catch"), new Answer(4, "finally") });
            q25.CorrectAnswers = new List<Answer> { q25.Answers[0], q25.Answers[2], q25.Answers[3] };
            qList.Add(q25);

            var q26 = new ChooseAllQuestion("Which of these are creational design patterns?", 15);
            q26.Answers.Answers.AddRange(new[] { new Answer(1, "Singleton"), new Answer(2, "Observer"), new Answer(3, "Factory Method"), new Answer(4, "Abstract Factory") });
            q26.CorrectAnswers = new List<Answer> { q26.Answers[0], q26.Answers[2], q26.Answers[3] };
            qList.Add(q26);

            var q27 = new ChooseAllQuestion("Which of these are features of C# generics?", 15);
            q27.Answers.Answers.AddRange(new[] { new Answer(1, "Type safety"), new Answer(2, "Dynamic dispatch"), new Answer(3, "Code reuse"), new Answer(4, "Reduced boxing") });
            q27.CorrectAnswers = new List<Answer> { q27.Answers[0], q27.Answers[2], q27.Answers[3] };
            qList.Add(q27);

            var q28 = new ChooseAllQuestion("Which are valid ways to create a thread in C#?", 15);
            q28.Answers.Answers.AddRange(new[] { new Answer(1, "new Thread()"), new Answer(2, "Thread.Spawn()"), new Answer(3, "Task.Run()"), new Answer(4, "async/await") });
            q28.CorrectAnswers = new List<Answer> { q28.Answers[0], q28.Answers[2], q28.Answers[3] };
            qList.Add(q28);

            var q29 = new ChooseAllQuestion("Which LINQ methods return a single element?", 15);
            q29.Answers.Answers.AddRange(new[] { new Answer(1, "First()"), new Answer(2, "Where()"), new Answer(3, "Single()"), new Answer(4, "Select()") });
            q29.CorrectAnswers = new List<Answer> { q29.Answers[0], q29.Answers[2] };
            qList.Add(q29);

            var q30 = new ChooseAllQuestion("Which of these implement IDisposable pattern correctly?", 15);
            q30.Answers.Answers.AddRange(new[] { new Answer(1, "using statement"), new Answer(2, "Garbage Collector call"), new Answer(3, "using declaration (C# 8+)"), new Answer(4, "Dispose() method call") });
            q30.CorrectAnswers = new List<Answer> { q30.Answers[0], q30.Answers[2], q30.Answers[3] };
            qList.Add(q30);

            questions = qList;
        }
    }
}
