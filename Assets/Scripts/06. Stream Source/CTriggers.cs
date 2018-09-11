using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[RequireComponent (typeof(Rigidbody), typeof(Collider))]
public class CTriggers : MonoBehaviour
{
    public float moveForce = 10.0f;
    public bool isForceEnabled = false;

    private Rigidbody _rigidbody = null;
    private float _horizontal = 0.0f;
    private float _vertical = 0.0f;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // 플래그가 유효한 사이, 위쪽으로 힘을 가한다.
        this.FixedUpdateAsObservable()
            .Where(_ => isForceEnabled)
            .Subscribe(_ => 
            {
                Debug.Log("점프");
                _rigidbody.AddForce(Vector3.up * 30.0f, ForceMode.Impulse);
            });

        this.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.tag == "WarpZone")
            .Subscribe(_ => 
            {
                Debug.Log("워프존에 진입.");
                isForceEnabled = true;
            });

        this.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.tag == "WarpZone")
            .Subscribe(_ => 
            {
                Debug.Log("워프존을 벗어남.");
                isForceEnabled = false;
            });

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _horizontal = Input.GetAxis("Horizontal");
                _vertical = Input.GetAxis("Vertical");
                _rigidbody.AddForce((Vector3.right * _horizontal + Vector3.forward * _vertical) * moveForce, ForceMode.VelocityChange);
            });
    }
}
