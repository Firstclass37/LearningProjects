using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication2
{
    internal class Board
    {
        public List<Element> Elements { get; }
    
        public Board(int[,] array)
        {
            Elements = new List<Element>();
            Init(array);
        }

        public void Show()
        {
            Console.WriteLine(new string('-', 18));
            for (int row = 0; row < 9; row++)
            {
                var rowElements = Elements.Where(r => r.Row == row).ToList();
                foreach (var element in rowElements)
                {
                    Console.Write(element.Value);
                    if (element.Column + 1 >= 3 && element.Column + 1 < 9 && (element.Column + 1) % 3 == 0)
                        Console.Write("|");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
                if (row + 1 >= 3 && row + 1 < 9 && (row + 1) % 3 == 0)
                    Console.WriteLine(new string('-', 18));
            }
            Console.WriteLine(new string('-', 18));
        }

        private void Init(int[,] array)
        {
            var length = Math.Pow(array.Length, 0.5);
            for (int rowNum = 0; rowNum < length; rowNum++)
            {
                for (int columnNum = 0; columnNum < length; columnNum++)
                {
                    var currentElem = new Element();
                    currentElem.Value = array[rowNum, columnNum];
                    currentElem.Column = columnNum;
                    currentElem.Row = rowNum;
                    currentElem.Square = GetSquareNumber(rowNum, columnNum);
                    Elements.Add(currentElem);
                }
            }
        }

        private int GetSquareNumber(int row, int column)
        {
            var rowSquare = GetSquarePos(row);
            var colSquare = GetSquarePos(column);
            if (rowSquare == 1 && colSquare == 1)
                return 1;
            if (rowSquare == 1 && colSquare == 2)
                return 2;
            if (rowSquare == 1 && colSquare == 3)
                return 3;
            if (rowSquare == 2 && colSquare == 1)
                return 4;
            if (rowSquare == 2 && colSquare == 2)
                return 5;
            if (rowSquare == 2 && colSquare == 3)
                return 6;
            if (rowSquare == 3 && colSquare == 1)
                return 7;
            if (rowSquare == 3 && colSquare == 2)
                return 8;
            if (rowSquare == 3 && colSquare == 3)
                return 9;
            return 1;
        }

        private int GetSquarePos(int val)
        {
            var squareNum = 1;
            if (val >= 0 && val <= 2)
                squareNum = 1;
            else if (val >= 3 && val <= 5)
                squareNum = 2;
            else if (val >= 6 && val <= 8)
                squareNum = 3;
            return squareNum;
        }
    }
}