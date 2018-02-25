using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeApp
{
    internal static class ConsoleExtentions
    {
        public static int[] AskIntArray(string message)
        {
            var ints = new List<int>();
            do
            {
                Console.Write($"{message} : ");
                var stringValues = Console.ReadLine();
                if (stringValues != null)
                    ints = ParseInts(stringValues.Split(' ').ToArray());
            } while (!ints.Any());
            return ints.ToArray();
        }

        public static int AskInt(string message)
        {
            var gotInt = false;
            int value = 0;
            do
            {
                Console.Write($"{message} : ");
                var stringValues = Console.ReadLine();
                if (stringValues != null)
                {
                    gotInt = int.TryParse(stringValues, out value);
                }
            } while (!gotInt);
            return value;
        }

        public static bool AskBool(string message)
        {
            bool boolAswer;
            bool isCorrectAnswer;
            do
            {
                Console.Write($"{message} (y/n)?: ");
                var answer = Console.ReadKey();
                Console.WriteLine();
                isCorrectAnswer = answer.KeyChar == 'y' || answer.KeyChar == 'n';
                boolAswer = isCorrectAnswer && answer.KeyChar == 'y';
            } while (!isCorrectAnswer);
            return boolAswer;
        }




        private static List<int> ParseInts(string[] stringArray)
        {
            var ints = new List<int>();
            foreach (var stringValue in stringArray)
            {
                if (int.TryParse(stringValue, out var value))
                    ints.Add(value);
            }
            return ints;
        }
    }
}
