using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor.
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    
    // CACHE - e.g. references for readibility or speed.
    Rigidbody rb;
    AudioSource audioSource;
    
    // STATE - private instance (member) variable

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
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        // Adds a force to the rigidbody relative to its coordinate system.
        // Force can be applied only to an active rigidbody.
        // (0, 1, 0) for the rocket to take off.
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // Decrease SpaceX mass to 0.2f.

        if (!audioSource.isPlaying)
        {
            // audioSource.Play();
            // for spesific audioClip (if we have more than one)
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    
    void RotateRight()
    {
        // Using axis Z
        // transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        ApplyRotation(rotationThrust);

        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    void RotateLeft()
    {
        // Vector3.back == (0, 0, -1)
        // transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);
        ApplyRotation(-rotationThrust);

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    // Extract method and rename with F2
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
