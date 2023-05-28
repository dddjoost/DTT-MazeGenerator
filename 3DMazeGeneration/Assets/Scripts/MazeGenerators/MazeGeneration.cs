using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace DefaultNamespace.MazeGenerators
{
    public class MazeGeneration : MonoBehaviour
    {
        public int width;
        public int height;
        public Cell[,] Maze;
        [SerializeField] private bool useSeed;
        [SerializeField] private int seed;
        [SerializeField] private GameObject wall;
        private List<GameObject> _maze;
        [SerializeField] public float widthCell = 10f;
        [SerializeField] public float heightCell = 10f;
        [SerializeField] private WallPooler wallPooler;
        public static UnityEvent<int, int, float, float> ChangeMapSize;

        private void Awake()
        {
            // width, height, widthCell, heightCell
            ChangeMapSize = new UnityEvent<int, int, float, float>();
            ChangeMapSize.AddListener(CreateMaze);
        }

        protected virtual void Start()
        {
            
            //CreateMaze(10, 20, 30,10);
            ChangeMapSize.Invoke(10,10,10,10);
           
            
        }

        public void CreateMaze(int width, int height, float widthCell, float heightCell)
        {
            this.width = width;
            this.height = height;
            this.widthCell = widthCell;
            this.heightCell = heightCell;
            Maze = new Cell[width, height];

            if (_maze != null && _maze.Count != 0)
            {
                wallPooler.Enqueue(_maze);
                _maze.Clear();
            }
            else
            {
                _maze = new List<GameObject>();
            }
            GenerateMaze();
            StartCoroutine(CreateWalls());
           // ChangeMapSize.Invoke(this.width,this.height,widthCell,heightCell);
        }


        protected void FillMaze()
        {
            for (int width = 0; width < Maze.GetLength(0); width++)
            {
                for (int height = 0; height < Maze.GetLength(1); height++)
                {
                    Maze[width, height] = new Cell(width, height);
                }
            }

            for (int width = 0; width < Maze.GetLength(0); width++)
            {
                for (int height = 0; height < Maze.GetLength(1); height++)
                {
                    Cell top = null;
                    if (height < Maze.GetLength(1) - 1)
                    {
                        top = Maze[width, height + 1];
                    }

                    Cell bottom = null;
                    if (height > 0)
                    {
                        bottom = Maze[width, height - 1];
                    }

                    Cell left = null;
                    if (width > 0)
                    {
                        left = Maze[width - 1, height];
                    }

                    Cell right = null;
                    if (width < Maze.GetLength(0) - 1)
                    {
                        right = Maze[width + 1, height];
                    }

                    Maze[width, height].SetProperties(top, bottom, left, right);
                }
            }
        }

        protected virtual void GenerateMaze()
        {
            FillMaze();
        }

        protected IEnumerator CreateWalls()
        {
            float halfWidthWall = widthCell / 2;
            float halfHeightWall = heightCell / 2;
            for (int i = 0; i < Maze.GetLength(0); i++)
            {
                for (int j = 0; j < Maze.GetLength(1); j++)
                {
                    Cell current = Maze[i, j];
                    if (current.Top)
                    {
                        GameObject wallObjectTop = wallPooler.Dequeue();
                        wallObjectTop.transform.position = new Vector3(widthCell * i, 0, heightCell * j + (halfHeightWall));
                        wallObjectTop.transform.rotation = Quaternion.identity;
                        wallObjectTop.transform.localScale = new Vector3(widthCell, 1,1);
                        _maze.Add(wallObjectTop);
                    }

                    if (current.Left)
                    {
                        GameObject wallObjectLeft = wallPooler.Dequeue();
                        wallObjectLeft.transform.position = new Vector3(widthCell * i - halfWidthWall, 0, heightCell * j);
                        wallObjectLeft.transform.rotation = Quaternion.Euler(0, 90, 0);
                        wallObjectLeft.transform.localScale = new Vector3(heightCell, 1,1);
                        _maze.Add(wallObjectLeft);
                    }


                    if (j == 0)
                    {
                        GameObject wallObjectBounds = wallPooler.Dequeue();
                        wallObjectBounds.transform.position = new Vector3(widthCell * i, 0, -halfHeightWall);
                        wallObjectBounds.transform.rotation = Quaternion.identity;
                        wallObjectBounds.transform.localScale = new Vector3(widthCell, 1,1);
                        _maze.Add(wallObjectBounds);
                    }

                    if (i == 0)
                    {
                        GameObject wallObjectBounds = wallPooler.Dequeue();
                        wallObjectBounds.transform.position = new Vector3(widthCell * width - halfWidthWall, 0, heightCell * j);
                        wallObjectBounds.transform.rotation = Quaternion.Euler(0, 90, 0);
                        wallObjectBounds.transform.localScale = new Vector3(heightCell, 1,1);
                        _maze.Add(wallObjectBounds);
                    }
                }

                yield return null;
            }
        }

        protected Cell ChooseRandomNeighbor(Cell cell)
        {
            Cell[] availableNeighbors = cell.GetUnwatchedCells();
            if (availableNeighbors.Length == 0)
            {
                return null;
            }

            return availableNeighbors[UnityEngine.Random.Range(0, availableNeighbors.Length)];
        }
    }
}