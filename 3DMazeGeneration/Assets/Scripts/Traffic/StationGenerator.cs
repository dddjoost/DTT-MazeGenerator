using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    public class StationGenerator : MonoBehaviour
    {
        public static StationGenerator StationGeneratorSingleton;
        public Station.PassengerType[] PassengerTypes = Enum.GetValues(typeof(Station.PassengerType)) as Station.PassengerType[];
        [SerializeField] private List<GameObject> stationPrefabs;

        //[SerializeField] private List<Station.PassengerType> stationPrefabTypes;
        public Dictionary<Station.PassengerType, List<PickUpPoint>> Stations;

        private void Start()
        {
            if (StationGeneratorSingleton != null)
            {
                Destroy(this);
            }
            StationGeneratorSingleton = this;
            MazeGeneration.ChangeMapSize.AddListener(ChangedMapSize);
        }

        public void ChangedMapSize(int width, int height, float widthCell, float heightCell)
        {
            int amountOfPlayers = PlayerManager.PlayerSingleton.amountOfPlayers;
            if (Stations != null)
            {
                foreach (List<PickUpPoint> pickUpPoints in Stations.Values)
                {
                    foreach (PickUpPoint pickUpPoint in pickUpPoints)
                    {
                        Destroy(pickUpPoint.gameObject);
                    }
                }
                Stations.Clear();
            }
            else
            {
                Stations = new Dictionary<Station.PassengerType, List<PickUpPoint>>();
            }
            
            
            for (int station = 0; station < stationPrefabs.Count; station++)
            {
                Stations.Add(PassengerTypes[station], new List<PickUpPoint>(amountOfPlayers));
                Vector3 randomLocation = new Vector3(
                    UnityEngine.Random.Range(0, width) * widthCell, 0,
                    UnityEngine.Random.Range(0, height) * heightCell);
                Debug.Log(randomLocation);
                Debug.Log(PassengerTypes[station].ToString());
                for (int playerId = 0; playerId < amountOfPlayers; playerId++)
                {
                    PickUpPoint pickUpPoint = Instantiate(stationPrefabs[station], randomLocation, Quaternion.identity)
                        .GetComponent<PickUpPoint>();
                    pickUpPoint.id = playerId;
                    Stations[PassengerTypes[station]].Add(pickUpPoint);
                }
            }
        }
    }
}