using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    
    public class PassengerUI : MonoBehaviour
    {
        [SerializeField] private List<Material> materials;
        [SerializeField] private List<Station.PassengerType> passengerTypes;
        private List<Material> _noneMaterial;
        [SerializeField] private GameObject meshRendererParent;

        private MeshRenderer[] _meshRenderers;
        private Station.PassengerType[] _currentPassengerTypes;
        private void Start()
        {
            _meshRenderers = meshRendererParent.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.gameObject.SetActive(false);
            }
            _currentPassengerTypes = new Station.PassengerType[_meshRenderers.Length];
            for (int i = 0; i < _currentPassengerTypes.Length; i++)
            {
                _currentPassengerTypes[i] = Station.PassengerType.NonePassenger;
            }

            _noneMaterial = new List<Material>()
                { materials[passengerTypes.IndexOf(Station.PassengerType.NonePassenger)] };
            
            Debug.Log(_currentPassengerTypes[0]);
            
        }


        public bool SetPassengerUI(Station.PassengerType passengerType)
        {
            for (int i = 0; i < _currentPassengerTypes.Length; i++)
            {
                if (_currentPassengerTypes[i] == Station.PassengerType.NonePassenger)
                {
                    _currentPassengerTypes[i] = passengerType;
                    int index = passengerTypes.IndexOf(passengerType);
                    Material material = materials[index];
                    _meshRenderers[i].SetMaterials(new List<Material> { material });
                    _meshRenderers[i].gameObject.SetActive(true);
                    return true;
                }
            }

            return false;


        }

        public bool RemovePassengerUI(Station.PassengerType passengerType)
        {
            for (int i = 0; i < _currentPassengerTypes.Length; i++)
            {
                if (_currentPassengerTypes[i] == passengerType)
                {
                    _currentPassengerTypes[i] = Station.PassengerType.NonePassenger;
                    _meshRenderers[i].SetMaterials(_noneMaterial);
                    _meshRenderers[i].gameObject.SetActive(false);
                    return true;
                }
            }

            return false;
        }
    }
}