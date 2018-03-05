using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ConsoleApplication2;

public class Program
{
    public static void Main()
    {

        var board = new int[9, 9]
        {
            {5,3,0,0,7,0,0,0,0},
            {6,0,0,1,9,5,0,0,0},
            {0,9,8,0,0,0,0,6,0},
            {8,0,0,0,6,0,0,0,3},
            {4,0,0,8,0,3,0,0,1},
            {7,0,0,0,2,0,0,0,6},
            {0,6,0,0,0,0,2,8,0},
            {0,0,0,4,1,9,0,0,5},
            {0,0,0,0,8,0,0,7,9}

        };
        
        PrintSudoku(board);
        
        SolveSudoku(board);

        Console.WriteLine();
        Console.WriteLine();
        
        PrintSudoku(board);

        Console.ReadKey();
    }

    
    private static readonly int[] _numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9};
    private static readonly int _empty = 0;


    private static void SolveSudoku(int[,] array)
    {
        var queue = CreateQueue(array);
        var attemptCount = 10000;
        while (queue.Any() && attemptCount-- > 0)
        {
            var currentElement = queue.Dequeue();
            if (TryInit(array, currentElement.Item1, currentElement.Item2) != true)
                queue.Enqueue(currentElement);
        } 
    }

    private static bool TryInit(int[,] array, int rowNum, int columnNum)
    {
        if (array[rowNum, columnNum] != _empty)
            return true;

        var availabelRow = GetAvailableRow(array, rowNum);
        var availableColumn = GetAvailableColumn(array, columnNum);
        var availableSquare = GetAvailableSquare(array, rowNum, columnNum);

        var canBe = availabelRow.Intersect(availableColumn).Intersect(availableSquare).ToList();
        if (canBe.Count == 1)
            array[rowNum, columnNum] = canBe.First();
        return canBe.Count == 1;
    }

    private static int[] GetAvailableColumn(int[,] array, int column)
    {
        var columnElements = GetColumnElements(array, column);
        var availableColumn = GetAvailableNumber(columnElements);
        return availableColumn;
    }

    private static int[] GetAvailableRow(int[,] array, int row)
    {
        var rowElements = GetRowElements(array, row);
        var availabelRow = GetAvailableNumber(rowElements);
        return availabelRow;
    }

    private static int[] GetAvailableSquare(int[,] array, int row, int column)
    {
        var squareElements = GetSquareElement(array, row, column);
        var availableSquare = GetAvailableNumber(squareElements);
        return availableSquare;
    }

    private static int[] GetAvailableNumber(int[] inputNumbers)
    {
        return _numbers.Except(inputNumbers).Where(n => n != _empty).ToArray();
    }
    
    private static int[] GetRowElements(int[,] array, int rowNum)
    {
        var rowElements = new List<int>();
        for (int columnNum = 0; columnNum < 9; columnNum++)
        {
            if (array[rowNum, columnNum] != _empty)
                rowElements.Add(array[rowNum, columnNum]);
        }
        return rowElements.ToArray();
    }
    
    private static int[] GetColumnElements(int[,] array, int columnNum)
    {
        var columnElement = new List<int>();
        for (int rowNum = 0; rowNum < 9; rowNum++)
        {
            if (array[rowNum, columnNum] != _empty)
                columnElement.Add(array[rowNum, columnNum]);
        }
        return columnElement.ToArray();
    }

    private static int[] GetSquareElement(int[,] array, int row, int column)
    {
        var squareNumbers = new List<int>();
        var startRowPos = row/3 * 3;
        var startColPos = column / 3 * 3;
        for (int r = startRowPos; r < startRowPos + 3; r++)
        {
            for (int c = startColPos; c < startColPos + 3; c++)
            {
                if (array[r, c] != _empty)
                    squareNumbers.Add(array[r, c]);
            }
        }
        return squareNumbers.ToArray();
    }
    
    private static void PrintSudoku(int[,] sudoku)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.Write(sudoku[i, j]);
                if (j+1 >= 3 && j+1 < 9 &&(j+1) % 3 == 0)
                    Console.Write("|");
                else
                    Console.Write(" ");
            }
            Console.WriteLine();
            if (i+1 >= 3 && i+1 < 9 &&(i+1) % 3 == 0)
                Console.WriteLine(new string('-', 18));
        }
    }

    private static Queue<Tuple<int, int>> CreateQueue(int[,] array)
    {
        var stack = new Queue<Tuple<int, int>>();
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (array[row, column] == _empty)
                    stack.Enqueue(new Tuple<int, int>(row, column));
            }
        }
        return stack;
    }
}