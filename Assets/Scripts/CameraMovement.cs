using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    private Vector3 _intialOffset;
    private Vector3 _cameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _intialOffset = transform.position - _targetTransform.position;
    }
    
    void FixedUpdate()
    {
        _cameraPosition = _targetTransform.position + _intialOffset;
        transform.position = _cameraPosition;
    }
}
