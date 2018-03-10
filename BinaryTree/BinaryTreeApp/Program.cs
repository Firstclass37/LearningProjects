using System;
using System.Runtime.Serialization;
using Tree;

namespace BinaryTreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var trree = new BinaryTree<string>();
           Parse();
           Console.ReadKey();
        }

        private static void Parse()
        {
            var formatter = new Formatter<int>();
            var formatInfo = new FormatInfo(ShowType.Infix);
            formatInfo.ValueStart = "<";
            formatInfo.ValueEnd = ">";
            var manager = new TreeManager();
            var originalTree = new BinaryTree<int>();
            originalTree.Add(new[] { 7, 4, 9, 1, 3, -1, 8, 2, 11 });
            Console.WriteLine($"Original  : {formatter.ToString(originalTree, formatInfo)}");
            var savedPath = manager.Save(originalTree, formatInfo);
            var loadedTree = manager.Load<int>(savedPath);
            Console.WriteLine($"AfterSave : {formatter.ToString(loadedTree, formatInfo)}");
            
        }

        private static void Remove()
        {
            var formatter = new Formatter<int>();
            var tree = new BinaryTree<int>();
            tree.Add(new[] { 7, 4, 9, 1, 3, -1, 8, 2, 11 });
            Console.WriteLine(formatter.ToString(tree, ShowType.Infix));
            tree.Remove(1);
            Console.WriteLine(formatter.ToString(tree, ShowType.Infix));
        }

        private static void Contains()
        {
            //var needNext = true;
            var ints = ConsoleExtentions.AskIntArray("Input numbers (min 1)");
            var tree = new BinaryTree<int>();
            tree.Add(ints);
            do
            {
                var number = ConsoleExtentions.AskInt("Number for check");
                Console.WriteLine($"Contains : {tree.Contains(number)}");
                //needNext = UiManager.AskBool("Continue");
            } while (true);
        }

        private static void ShowTree()
        {
            var formatter = new Formatter<int>();
            var tree = new BinaryTree<int>();
            tree.Add(new[] { 7, 4, 9, 1, 3, -1, 8, 2, 11 });
            do
            {
                Console.WriteLine("Show: prefix - 1, postfix - 2, infix - 3");
                Console.Write("Choice : ");
                var answer = Console.ReadKey();
                Console.WriteLine();
                if (answer.Key == ConsoleKey.D1)
                    Console.WriteLine(formatter.ToString(tree, ShowType.Prefix));
                else if (answer.Key == ConsoleKey.D2)
                    Console.WriteLine(formatter.ToString(tree, ShowType.Postfix));
                else if (answer.Key == ConsoleKey.D3)
                    Console.WriteLine(formatter.ToString(tree, ShowType.Infix));
                else
                    Console.WriteLine(":(");
                //needNext = UiManager.AskBool("Continue");
            } while (true);
        }
    }
}
