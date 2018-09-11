using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CFollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _targetObject = null;

    public Vector3 offset = new Vector3(0.0f, 3.0f, -2.0f);

    [Range(0.0f, 1.0f)]
    public float smoothDamp = 0.2f;

    // Use this for initialization
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetObject.position + offset, smoothDamp);
        transform.LookAt(_targetObject);
    }
}
