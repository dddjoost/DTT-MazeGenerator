using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    public class Passenger
    {
        public Station.PassengerType PassengerType;

        public Passenger(Station.PassengerType passengerType)
        {
            this.PassengerType = passengerType;
        }
    }
}