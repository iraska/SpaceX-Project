using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0,1)] float movementFactor;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // for oscilattor's current position
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (period == 0f) { return; } // transform.position assign attempt for 'Oscillator' is not valid. Input position is {NaN, NaN, NaN}
        if (period <= Mathf.Epsilon) { return; }

        // we need to create a mechanism to be measuring time
        // Time.time: how much time has ellapsed
        float cycles = Time.time / period;

        // const: constant variable
        // Mathf.PI: The ratio of the circumference of a circle to its diameter. Note that this value is a 32-bit floating point number i.e. a float.
        // Tau: the constant is numerically equal to 2*pi (2 times pi), and with value approximately 6.28.
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        // rawSinWave going from -1 to 1, we want to go from 0 to 1
        movementFactor = (rawSinWave + 1f) / 2f; // 0 to 1
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset; // new position
    }
}
