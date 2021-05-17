using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SudokuSolver
{
    class Sudoku
    {
        public Square[,] Squares { get; private set; } = new Square[9, 9];

        public Sudoku(int[,] elements)
        {
            Square[,] squares = new Square[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Square square = new Square();

                    square.Element = elements[i, j];
                    square.Position = new Point(i, j);
                    square.SetParentBox();

                    squares[i, j] = square;
                }
            }

            Squares = squares;
        }

        public void Solve()
        {
            while (!SudokuFilled(Squares))
            {
                for (int currentNumber = 1; currentNumber <= 9; currentNumber++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (SquareFilled(Squares[i, j]))
                            {
                                Squares[i, j].PotentialElements.Clear();
                                continue;
                            }

                            CheckBox(Squares[i, j], currentNumber);
                            CheckLine(Direction.Vertical, Squares[i, j], currentNumber);
                            CheckLine(Direction.Horizontal, Squares[i, j], currentNumber);

                            if (Squares[i, j].PotentialElements.Count == 1)
                            {
                                Squares[i, j].Element = Squares[i, j].PotentialElements[0];
                            }
                        }
                    }
                }                
            }            
        }

        private bool SudokuFilled(Square[,] elements)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (elements[i, j].Element == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool SquareFilled(Square square)
        {
            return square.Element != 0;
        }

        private void CheckBox(Square square, int currentNumber)
        {
            Square[,] currentBox = GetCurrentBox(square);
            if (BoxAlreadyContainsNumber(currentBox, currentNumber))
            {
                square.PotentialElements.Remove(currentNumber);
                return;
            }
        }

        private Square[,] GetCurrentBox(Square square)
        {
            switch(square.ParentBox)
            {
                case Square.Box.TopLeft:
                    return GetBoxFromSudoku(0, 0);

                case Square.Box.TopMiddle:
                    return GetBoxFromSudoku(3, 0);
                    
                case Square.Box.TopRight:
                    return GetBoxFromSudoku(6, 0);
                    
                case Square.Box.CentreLeft:
                    return GetBoxFromSudoku(0, 3);
                    
                case Square.Box.CentreMiddle:
                    return GetBoxFromSudoku(3, 3);
                    
                case Square.Box.CentreRight:
                    return GetBoxFromSudoku(6, 3);
                    
                case Square.Box.BottomLeft:
                    return GetBoxFromSudoku(0, 6);
                    
                case Square.Box.BottomMiddle:
                    return GetBoxFromSudoku(3, 6);
                    
                case Square.Box.BottomRight:
                    return GetBoxFromSudoku(6, 6);

                default:
                    throw new ArgumentException("Must be a real box location");                   
            }
        }

        private Square[,] GetBoxFromSudoku(int rowModifier, int columnModifier)
        {
            Square[,] box = new Square[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    box[i, j] = Squares[i + rowModifier, j + columnModifier];
                }
            }

            return box;
        }

        private bool BoxAlreadyContainsNumber(Square[,] box, int currentNumber)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (box[i, j].Element == currentNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private enum Direction
        {
            Vertical,
            Horizontal
        }

        private void CheckLine(Direction direction, Square square, int currentNumber)
        {
            int[] line = GetLine(direction, square);
            if (line.Contains(currentNumber))
            {
                square.PotentialElements.Remove(currentNumber);
                return;
            }
        }

        private int[] GetLine(Direction direction, Square square)
        {
            int[] line = new int[9];

            switch (direction)
            {
                case Direction.Vertical:
                    for (int i = 0; i < 9; i++)
                    {
                        line[i] = Squares[square.Position.X, i].Element;
                    }
                    break;

                case Direction.Horizontal:
                    for (int i = 0; i < 9; i++)
                    {
                        line[i] = Squares[i, square.Position.Y].Element;
                    }
                    break;
            }

            return line;
        }
    }
}
