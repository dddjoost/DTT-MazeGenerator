using System.Collections.Generic;

namespace DefaultNamespace.MazeGenerators
{
    public class Cell
    {
        public enum CellType
        { Top, Bottom, Left, Right, None
        }
        public bool Top;
        public bool Bottom;
        public bool Left;
        public bool Right;

        public bool IsWatched;

        public Cell TopCell;
        public Cell BottomCell;
        public Cell LeftCell;
        public Cell RightCell;

        public int Width;
        public int Height;

        public Cell(Cell topCell,
            Cell bottomCell, Cell leftCell, Cell rightCell, int width, int height) : this(width,height)
        {
            
            TopCell = topCell;
            BottomCell = bottomCell;
            LeftCell = leftCell;
            RightCell = rightCell;
        }

        public Cell(int width, int height)
        {
            Top = true;
            Bottom = true;
            Left = true;
            Right = true;
            IsWatched = false;
            Width = width;
            Height = height;
        }

        public CellType GetCellType(Cell cell)
        {
            if (TopCell != null && cell == TopCell)
            {
                return CellType.Top;
            }
            if (BottomCell != null && cell == BottomCell)
            {
                return CellType.Bottom;
            }
            if (LeftCell != null && cell == LeftCell)
            {
                return CellType.Left;
            }
            if (RightCell != null && cell == RightCell)
            {
                return CellType.Right;
            }

            return CellType.None;
        }

        public void SetProperties(Cell topCell,
            Cell bottomCell, Cell leftCell, Cell rightCell)
        {
            TopCell = topCell;
            BottomCell = bottomCell;
            LeftCell = leftCell;
            RightCell = rightCell;
        }

        public Cell[] GetUnwatchedCells()
        {
            bool top = TopCell is { IsWatched: false };
            bool bottom = BottomCell is { IsWatched: false };
            bool left = LeftCell is { IsWatched: false };
            bool right = RightCell is { IsWatched: false };

            List<Cell> cells = new List<Cell>(4);
            if (top)
            {
                cells.Add(TopCell);
            }
            if (bottom)
            {
                cells.Add(BottomCell);
            }
            if (left)
            {
                cells.Add(LeftCell);
            }
            if (right)
            {
                cells.Add(RightCell);
            }

            return cells.ToArray();

        }
    }
}