using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.MazeGenerators
{
    public class Player : MonoBehaviour
    {
        public int id;

        public Station.PassengerType[] Passengers;

        [SerializeField] private PassengerUI passengerUI;
        [SerializeField] private int maxPassengers = 6;
        public UnityEvent<int> pointsHandedIn = new();
        public UnityEvent peoplePickedUpEvent = new();

        private void Start()
        {
            Passengers = new Station.PassengerType[maxPassengers];
            for (int i = 0; i < Passengers.Length; i++)
            {
                Passengers[i] = Station.PassengerType.NonePassenger;
            }
        }

        public bool AddPassenger(Station.PassengerType passenger)
        {
            for (int index = 0; index < Passengers.Length; index++)
            {
                if (Passengers[index] == Station.PassengerType.NonePassenger)
                {
                    passengerUI.SetPassengerUI(passenger);
                    Passengers[index] = passenger;
                    return true;
                }
            }

            return false;
        }

        public Station.PassengerType[] RemovePassengersFromType(Station.PassengerType passengerType)
        {
            List<Station.PassengerType> passengers = new List<Station.PassengerType>();
            for (int index = 0; index < Passengers.Length; index++)
            {
                if (Passengers[index] == passengerType)
                {
                    passengers.Add(Passengers[index]);
                    passengerUI.RemovePassengerUI(Passengers[index]);
                    Passengers[index] = Station.PassengerType.NonePassenger;
                }
            }

            return passengers.ToArray();
        }
    }
}