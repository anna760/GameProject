using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);  // move only the position of x, y, z
    [SerializeField] float period = 2f;
    [Range(0, 1)] [SerializeField] float movementFactor;
    Vector3 startingPos;                                                   // starting position must be stored for absolute movement

    void Start()
    {
        startingPos = transform.position;                                  // starting position store
    }
    void Update()
    {
        
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);
            print(rawSinWave);
            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;


    }

    
}

