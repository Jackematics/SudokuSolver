using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCases TestCases = new TestCases();

            Console.WriteLine("Very Easy 1: ");
            PrintResults(TestCases.VeryEasyElements1);

            Console.WriteLine("Very Easy 2: ");
            PrintResults(TestCases.VeryEasyElements2);

            Console.WriteLine("Very Easy 3: ");
            PrintResults(TestCases.VeryEasyElements3);

            Console.WriteLine("Easy 1: ");
            PrintResults(TestCases.EasyElements1);
            Console.ReadLine();
        }

        static private void PrintResults(int[,] testCase)
        {
            Sudoku sudoku = new Sudoku(testCase);

            Console.WriteLine("Before Solve:");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku.Squares[j, i].Element == 0)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(sudoku.Squares[j, i].Element + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("After Solve: ");

            sudoku.Solve();            

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(sudoku.Squares[j, i].Element + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
