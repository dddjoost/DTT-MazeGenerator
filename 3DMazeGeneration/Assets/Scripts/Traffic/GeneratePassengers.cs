using System;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    [RequireComponent(typeof(Station))]
    public class GeneratePassengers : MonoBehaviour
    {
        private Station _station;
        [SerializeField] public Station.PassengerType[] generatablePassengerTypes;
        private float randomTimer = 0;

        private void Start()
        {
            _station = GetComponent<Station>();
            randomTimer = UnityEngine.Random.Range(100f, 1000f);
        }

        private void FixedUpdate()
        {
            randomTimer--;
        }

        private void Update()
        {
            if (randomTimer <= 0)
            {
                randomTimer = UnityEngine.Random.Range(1000f, 1000f);
                _station.AddPassenger(generatablePassengerTypes[UnityEngine.Random.Range(0, generatablePassengerTypes.Length)]);
            }   
        }
    }
}