using System;
using Tree;

namespace BinaryTreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
           var trree = new BinaryTree<string>();
           Contains();
           Remove();
           Console.ReadKey();
        }

        private static void Remove()
        {
            var tree = new BinaryTree<int>();
            tree.Add(new[] { 7, 4, 9, 1, 3, -1, 8, 2, 11 });
            Console.WriteLine(tree.Show());
            tree.Remove(1);
            Console.WriteLine(tree.Show());
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
            var tree = new BinaryTree<int>();
            tree.Add(new[] { 7, 4, 9, 1, 3, -1, 8, 2, 11 });
            do
            {
                Console.WriteLine("Show: prefix - 1, postfix - 2, infix - 3");
                Console.Write("Choice : ");
                var answer = Console.ReadKey();
                Console.WriteLine();
                if (answer.Key == ConsoleKey.D1)
                    Console.WriteLine(tree.Show(ShowType.Prefix));
                else if (answer.Key == ConsoleKey.D2)
                    Console.WriteLine(tree.Show(ShowType.Postfix));
                else if (answer.Key == ConsoleKey.D3)
                    Console.WriteLine(tree.Show(ShowType.Infix));
                else
                    Console.WriteLine(":(");
                //needNext = UiManager.AskBool("Continue");
            } while (true);
        }
    }
}
