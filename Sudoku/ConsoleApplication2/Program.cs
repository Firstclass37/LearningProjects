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

        newBoard.Show();

        solver.Solve(newBoard);

        newBoard.Show();
        Console.WriteLine($"Stemps : {solver.StepCount}");
        Console.ReadKey();
    }

    
    private static int[] numbers = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
    private static int[] positions = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8};
    private static int _empty = 0;


    private static void SolveSudoku(int[,] array)
    {
        var fullInitedRow = new List<int>();
        do
        {
            for (int rowNum = 0; rowNum < 9; rowNum++)
            {
                if (fullInitedRow.Contains(rowNum))
                    continue;
                var isRowInited = true;
                for (int columnNum = 0; columnNum < 9; columnNum++)
                {
                    var inited = TryInit(array, rowNum, columnNum);
                    isRowInited = isRowInited && inited;
                }
                if (isRowInited)
                    fullInitedRow.Add(rowNum);
            }
            Console.WriteLine(new string('=', 15));
        } while (fullInitedRow.Count < 8);
    }

    private static bool TryInit(int[,] array, int rowNum, int columnNum)
    {
        if (array[rowNum, columnNum] != _empty)
            return true;
        var columnElements = GetColumnElements(array, columnNum);
        var rowElements = GetRowElements(array, rowNum);
        var squareElements = GetSquareElement(array, rowNum, columnNum);
                
        var missingInRow = GetAvailableNumber(rowElements).Except(squareElements).Except(columnElements).ToList();
        if (missingInRow.Count == 1)
        {
            array[rowNum, columnNum] = missingInRow.First();
            return true;
        }
                
        var missingInColumn = GetAvailableNumber(columnElements).Except(squareElements).Except(rowElements).ToList();
        if (missingInColumn.Count == 1)
        {
            array[rowNum, columnNum] = missingInColumn.First();
            return true;
        }
        return false;
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
        var startRowPos = numbers.First(n => (n == 1 || n % 3 == 0) && (n - 1) <= row) - 1;
        var endRowPos = numbers.First(n =>  n % 3 == 0 && (n - 1) >= row) - 1;
        var startColPos = numbers.First(n => (n == 1 || n % 3 == 0)  && (n - 1) <= column) - 1;
        var endColPos = numbers.First(n => n % 3 == 0 && (n - 1) >= column) - 1;
        for (int r = startRowPos; r <= endRowPos; r++)
        {
            for (int c = startColPos; c <= endColPos; c++)
            {
                if (array[r, c] != _empty)
                    squareNumbers.Add(array[r, c]);
            }
        }
        return squareNumbers.ToArray();
    }

    private static int[] GetAvailableNumber(int[] inputNumbers)
    {
        var numbersFOrCheck = inputNumbers.Except(new List<int>() {0}).ToArray();
        var available = numbers.Except(numbersFOrCheck).ToArray();
        return available;
    }

}