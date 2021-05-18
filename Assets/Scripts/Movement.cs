using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    [SerializeField] private float _thrustPower = 100f;
    [SerializeField] private float _rotationRate = 100f;
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Process Inputs from the Player
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        // Apply The Force To the Rocket if the SPACE Key Is Pressed
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Time.deltaTime * _thrustPower * Vector3.up);
            // Play rocket booster sound effect only if its not already playing
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            // Stop the rocket booster sound effect if we are not pressing the SPACE Key
            _audioSource.Stop();
        }

    }

    private void ProcessRotation()
    {
        // Rotate the Rocket Left or Right Depending On the Key Pressed (A or D)
        // Only Allow One Of the Keys to Do Something So the Player Cant Rotate the Rocket
        // Both Left & Right
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-_rotationRate);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(_rotationRate);
        }

    }

    private void ApplyRotation(float rotation)
    {
        // Freezing rotation so we can manually rotate
        _rigidbody.freezeRotation = true;
        
        transform.Rotate(Time.deltaTime * rotation * Vector3.forward);
        
        //Unfreezing rotation so the physics system can take over and handle collision
        _rigidbody.freezeRotation = false;
    }
    
}
