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

        var newBoard = new Board(board);
        var solver = new Solver();

        
        PrintSudoku(board);
        
        SolveSudoku(board);

        Console.WriteLine();
        Console.WriteLine();
        
        
        PrintSudoku(board);
        //newBoard.Show();

        //solver.Solve(newBoard);

        //newBoard.Show();
        Console.WriteLine($"Stemps : {solver.StepCount}");
        Console.ReadKey();
    }

    
    private static int[] numbers = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
    private static int[] positions = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
    private static int _empty = 0;


    private static void SolveSudoku(int[,] array)
    {
        var queue = CreateStack(array);
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
        var columnElements = GetColumnElements(array, columnNum);
        var rowElements = GetRowElements(array, rowNum);
        var squareElements = GetSquareElement(array, rowNum, columnNum);

        var availabelRow = GetAvailableNumber(rowElements);
        var availableColumn = GetAvailableNumber(columnElements);
        var availableSquare = GetAvailableNumber(squareElements);

        var canBe = availabelRow.Intersect(availableColumn).Intersect(availableSquare).ToList().ElementAtOrDefault(1);
        if (canBe != 0)
            array[rowNum, columnNum] = canBe;
        return canBe != 0;
    }

    private static int[] GetAvailableNumber(int[] inputNumbers)
    {
        return numbers.Except(inputNumbers).Where(n => n != _empty).ToArray();
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
    

    private static Queue<Tuple<int, int>> CreateStack(int[,] array)
    {
        var stack = new Queue<Tuple<int, int>>();
        for (int row = 0; row < 9; row++)
        {
            for (int column = 0; column < 9; column++)
            {
                if (array[row, column] == 0)
                    stack.Enqueue(new Tuple<int, int>(row, column));
            }
        }
        return stack;
    }
}