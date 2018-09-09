using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CDestroyTimer : MonoBehaviour {

    [SerializeField]
    private CSubjectDispose _disposeTimer = null;
    
    private void Start()
    {
        _disposeTimer.OnTimeChanged
            .Where(x => x == 0)
            .Subscribe( x =>
            {
                transform.position = Vector3.zero;
                Debug.Log("Timer End : " + x);
            })
            // AddTo 가 없으면 Space 키를 누를시 이 오브젝트가 제거되며 
            // 0초에 MissingReferenceException 이 뜨게된다.
            // 참조한 CSubjectDispose 내에 아직 해당 Subscribe 가 등록되어 있지만
            // 오브젝트가 제거되며 구독한 오브젝트의 tranform 을 참조할 수 없기 때문이다.
            // AddTo는 이 오브젝트가 사라지더라도 자동으로 해당 Subscribe 를 Dispose 해준다.
            .AddTo(gameObject);
    }

    // Update is called once per frame
    void Update () {

        // 5초가 지나기 전,후에 Space 키를 누르는 테스트를 위한 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
	}
}
