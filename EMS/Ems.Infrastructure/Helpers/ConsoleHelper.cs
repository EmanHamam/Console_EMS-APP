using System;
using System.Collections.Generic;
using System.Text;

namespace Ems.Infrastructure.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"  {text.ToUpper()}");
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }

        public static void WriteSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static int GetSelection(string prompt, int min, int max)
        {
            int choice;
            while (true)
            {
                Console.Write($"{prompt} ({min}-{max}): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
                    return choice;
                WriteError("Invalid selection. Try again.");
            }
        }
        public static void DrawEMSHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(15, 1);
            Console.WriteLine(" _______  __  __   _____ ");
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("|  ____||  \\/  | / ____|");
            Console.SetCursorPosition(15, 3);
            Console.WriteLine("| |__   | \\  / || (___  ");
            Console.SetCursorPosition(15, 4);
            Console.WriteLine("|  __|  | |\\/| | \\___ \\ ");
            Console.SetCursorPosition(15, 5);
            Console.WriteLine("| |____ | |  | | ____) |");
            Console.SetCursorPosition(15, 6);
            Console.WriteLine("|______||_|  |_||_____/ ");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(20, 8);
            Console.WriteLine("--- EXAMINATION MANAGEMENT SYSTEM ---");
            Console.ResetColor();
        }
        public static int InteractiveMenu(string title, string[] options)
        {
            int selectedIndex = 0;
            ConsoleKey key;

            while (true)
            {
                Console.Clear();
                DrawEMSHeader();
                WriteHeader(title);
                Console.WriteLine("\nUse Arrow Keys to navigate, Enter to select:\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {options[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]} ");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    return selectedIndex + 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public static int[] InteractiveMultiSelect(string title, string[] options)
        {
            int selectedIndex = 0;
            HashSet<int> selectedIndices = new HashSet<int>();
            ConsoleKey key;

            while (true)
            {
                Console.Clear();
                WriteHeader(title);
                Console.WriteLine("\nUse Arrow Keys to navigate, Space to toggle, Enter to confirm:\n");

                for (int i = 0; i < options.Length; i++)
                {
                    string prefix = selectedIndices.Contains(i) ? "[X]" : "[ ]";
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"> {prefix} {options[i]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {prefix} {options[i]} ");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    if (selectedIndices.Contains(selectedIndex)) selectedIndices.Remove(selectedIndex);
                    else selectedIndices.Add(selectedIndex);
                }
                else if (key == ConsoleKey.Enter)
                {
                    int[] result = new int[selectedIndices.Count];
                    int idx = 0;
                    foreach (var i in selectedIndices) result[idx++] = i + 1;
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void TypingEffect(string text, int delay = 20)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}
