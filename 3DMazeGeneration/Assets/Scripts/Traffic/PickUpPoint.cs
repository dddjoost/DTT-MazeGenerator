using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.MazeGenerators
{
    [RequireComponent(typeof(Station))]
    public class PickUpPoint : MonoBehaviour
    {
        private Station _station;
        [FormerlySerializedAs("PickupType")] public Station.PassengerType pickupType;
        public int id = -1;

        private void Start()
        {
            _station = GetComponent<Station>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null && player.id == id)
            {
                Station.PassengerType[] pickedUpPoint = player.RemovePassengersFromType(pickupType);
                player.pointsHandedIn.Invoke(pickedUpPoint.Length);
                bool goOn = true;
                while (goOn)
                {
                    Station.PassengerType removablePassenger = _station.RemovePassenger();
                    if (removablePassenger == Station.PassengerType.NonePassenger) break;

                    player.AddPassenger(removablePassenger);
                    player.peoplePickedUpEvent.Invoke();
                }
            }
        }
    }
}