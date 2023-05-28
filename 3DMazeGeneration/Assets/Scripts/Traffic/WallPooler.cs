using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    public class WallPooler : MonoBehaviour
    {
        private Queue<GameObject> _walls;
        [SerializeField] private GameObject wall;
        
        private void Start()
        {
            _walls = new Queue<GameObject>();
        }

        private void AddNewWalls(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject wallObject = Instantiate(this.wall);
                wallObject.SetActive(false);
                _walls.Enqueue(wallObject);
            }
        }

        public GameObject Dequeue()
        {

            bool hasWall = _walls.TryDequeue(out GameObject wall);

            if (hasWall)
            {
                wall.SetActive(true);
                return wall;
            }
            AddNewWalls(20);
            GameObject wallResult = _walls.Dequeue();
            wallResult.SetActive(true);
            return wallResult;
        }

        public void Enqueue(GameObject wallGameObject)
        {
            wallGameObject.SetActive(false);
            _walls.Enqueue(wallGameObject);
        }

        public void Enqueue(List<GameObject> walls)
        {

            for (int i = 0; i < walls.Count; i++)
            {
                Enqueue(walls[i]);
            }
        }
    }
}