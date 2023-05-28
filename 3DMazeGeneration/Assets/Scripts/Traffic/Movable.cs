using System;
using UnityEngine;

namespace DefaultNamespace.MazeGenerators
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movable : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;


        private void Start()
        {
            rigidbody.maxLinearVelocity = 20;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.AddForce(Vector3.forward,ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody.AddForce(Vector3.left,ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.AddForce(Vector3.back,ForceMode.VelocityChange);
            }
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody.AddForce(Vector3.right,ForceMode.VelocityChange);
            }
        }
    }
}