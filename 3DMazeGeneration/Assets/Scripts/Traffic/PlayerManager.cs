using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace.MazeGenerators
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager PlayerSingleton;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] public int amountOfPlayers;
        private Player[] _players;
        public UnityEvent endOfGame = new ();

        private void Awake()
        {
            if (PlayerSingleton != null)
            {
                Destroy(this);
                return;
            }
            PlayerSingleton = this;
            
        }

        private void Start()
        {

            MazeGeneration.ChangeMapSize.AddListener(SpawnPlayers);

            // do AI stuff for player?
        }

        private void SpawnPlayers(int width, int height, float cellWidth, float cellHeight)
        {
            if (_players == null)
            {
                _players = new Player[amountOfPlayers];
                for (int i = 0; i < amountOfPlayers; i++)
                {
                    Debug.Log("Instantiated player prefab!!!!");
                    _players[i] = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
                }
            }
            
            Vector3 startPos = new Vector3(UnityEngine.Random.Range(0, width) * cellWidth / 2, 0,
                UnityEngine.Random.Range(0, height) * cellHeight / 2);
            Debug.Log(_players.Length);
            Debug.Log(amountOfPlayers);
            for (int i = 0; i < amountOfPlayers; i++)
            {
                _players[i].transform.position = startPos;
                _players[i].id = i;
            }
        }

    }
}