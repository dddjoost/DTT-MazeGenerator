using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    public class GrowingTreeGeneration : MazeGeneration
    {
        private List<Cell> _cells;

        protected override void Start()
        {
            _cells = new List<Cell>();
            base.Start();
        }

        protected override void GenerateMaze()
        {
            base.GenerateMaze();
            int randomWidth = Random.Range(0,width);
            int randomHeight = Random.Range(0,height);
            Cell first = Maze[randomWidth, randomHeight];
            first.IsWatched = true;
            _cells.Add(first);
            bool hasNeighbor = DoWalk(first);
            while (_cells.Count > 0)
            {

                Cell cell;

                if (hasNeighbor)
                {
                    cell = _cells[^1];
                }
                else
                {
                    cell = _cells[Random.Range(0, _cells.Count)];
                }

                hasNeighbor = DoWalk(cell);
            }
        }
        
        private bool DoWalk(Cell currentCell)
        {

            Cell randomNeighbor = ChooseRandomNeighbor(currentCell);
            if (randomNeighbor == null)
            {
                _cells.Remove(currentCell);
                return false;
            }
            randomNeighbor.IsWatched = true;
            _cells.Add(randomNeighbor);

            switch (currentCell.GetCellType(randomNeighbor))
            {
                case Cell.CellType.Top:
                    currentCell.Top = false;
                    randomNeighbor.Bottom = false;
                    break;
                case Cell.CellType.Bottom:
                    currentCell.Bottom = false;
                    randomNeighbor.Top = false;
                    break;
                case Cell.CellType.Left:
                    currentCell.Left = false;
                    randomNeighbor.Right = false;
                    break;
                case Cell.CellType.Right:
                    currentCell.Right = false;
                    randomNeighbor.Left = false;
                    break;
            }

            return true;
        }
    }
}