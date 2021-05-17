using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace SudokuSolver
{
    public class Square
    {
        public enum Box
        {
            TopLeft,
            TopMiddle,
            TopRight,
            CentreLeft,
            CentreMiddle,
            CentreRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        }

        public int Element { get; set; }
        public List<int> PotentialElements { get; private set; } = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        public Point Position { get; set; } 

        public Box ParentBox { get; set; }

        public void SetParentBox()
        {
            if (0 <= Position.X && Position.X < 3 && 
                0 <= Position.Y && Position.Y < 3)
            {
                ParentBox = Box.TopLeft;
            }
            else if (3 <= Position.X && Position.X < 6 && 
                     0 <= Position.Y && Position.Y < 3)
            {
                ParentBox = Box.TopMiddle;
            }
            else if (6 <= Position.X && Position.X < 9 && 
                     0 <= Position.Y && Position.Y < 3)
            {
                ParentBox = Box.TopRight;
            }
            else if (0 <= Position.X && Position.X < 3 && 
                     3 <= Position.Y && Position.Y < 6)
            {
                ParentBox = Box.CentreLeft;
            }
            else if (3 <= Position.X && Position.X < 6 &&
                     3 <= Position.Y && Position.Y < 6)
            {
                ParentBox = Box.CentreMiddle;
            }
            else if (6 <= Position.X && Position.X < 9 &&
                     3 <= Position.Y && Position.Y < 6)
            {
                ParentBox = Box.CentreRight;
            }
            else if (0 <= Position.X && Position.X < 3 && 
                     6 <= Position.Y && Position.Y < 9)
            {
                ParentBox = Box.BottomLeft;
            }
            else if (3 <= Position.X && Position.X < 6 &&
                     6 <= Position.Y && Position.Y < 9)
            {
                ParentBox = Box.BottomMiddle;
            }
            else if (6 <= Position.X && Position.X < 9 &&
                     6 <= Position.Y && Position.Y < 9)
            {
                ParentBox = Box.BottomRight;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Position must be within the confines of the Sudoku");
            }
        }
    }
}
