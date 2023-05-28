using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.MazeGenerators;
using UnityEngine;

public class Station : MonoBehaviour
{
    public enum PassengerType
    {
        RedPassenger,
        BluePassenger,
        GreenPassenger,
        PurplePassenger,
        NonePassenger
    }

    private PassengerType[] _passengerTypes;
    private Queue<PassengerType> _passengers;
    [SerializeField] private PassengerUI passengerUI;
    [SerializeField] private int maxPassengers = 18;

    private void Start()
    {
        _passengers = new Queue<PassengerType>();
        _passengerTypes = (Enum.GetValues(typeof(PassengerType))) as PassengerType[];
        // possiblePassengers[0].GetComponents<IPassenger>();
    }

    public void AddPassenger(PassengerType passengerType)
    {
        if (passengerType == PassengerType.NonePassenger)
        {
            Debug.LogError("none passenger was added. this is not normal behaviour");
            return;
        }

        if (_passengers.Count == maxPassengers)
        {
            PlayerManager.PlayerSingleton.endOfGame.Invoke();
        }
        Debug.Log("test?");
        
        // UI
        passengerUI.SetPassengerUI(passengerType);
        //Add to passenger list.
        _passengers.Enqueue(passengerType);
    }

    public PassengerType RemovePassenger()
    {
        //remove from passenger list.
        bool passenger = _passengers.TryDequeue(out PassengerType result);
        //UI
        if (!passenger)
        {
            return PassengerType.NonePassenger;
        }

        passengerUI.RemovePassengerUI(result);
        return result;
    }
}