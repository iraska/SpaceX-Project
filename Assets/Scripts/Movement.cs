using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Using axis Z
            // transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            // Vector3.back == (0, 0, -1)
            // transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);
            ApplyRotation(-rotationThrust);
        }
    }

    // Extract method
    void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation so we can manually rotate
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // Unfreezing rotation so the physic system can take over
        rb.freezeRotation = false;
    }

    // GetKeyDown will happen once when you hit the key
    // GetKeyUp happens once when you release key
}
