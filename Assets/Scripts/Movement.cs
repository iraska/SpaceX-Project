using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // We have to write the return type of the method.
    void ProcessThrust()
    {
        // GetKey will continue to happen while you are holding the key
        if (Input.GetKey(KeyCode.Space))
        {
            // Adds a force to the rigidbody relative to its coordinate system.
            // Force can be applied only to an active rigidbody.
            // (0, 1, 0) for the rocket to take off.
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); 
            // Decrease SpaceX mass to 0.2f.
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotate left");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotate right");
        }
    }

    // GetKeyDown will happen once when you hit the key
    // GetKeyUp happens once when you release key
}
