using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPosition;
    private float _movementFactor;
    
    [SerializeField] private Vector3 _movementVector;
    [SerializeField] private float _period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Protect against dividing by 0
        if (_period <= Mathf.Epsilon) { return; }
        
        // Use a sin wave to oscillate an obstacle between 2 points
        float cycles = Time.time / _period;
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        
        // Recalculate the sin equation to change it from ranging from -1 to 1 to instead 0 to 1
        _movementFactor = (rawSineWave + 1f) / 2f;
        
        // Move the obstacle
        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}
