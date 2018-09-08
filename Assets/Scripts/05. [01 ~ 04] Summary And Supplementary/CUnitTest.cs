using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CUnitTest : MonoBehaviour {

    private Subject<Unit> initializeSubject = new Subject<Unit>();

    /// <summary>
    /// 초기화가 완료된 것을 통지.
    /// </summary>
    public IObservable<Unit> OnInitialized {
        get { return initializeSubject; }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(InitializeCoroutine());

        OnInitialized.Subscribe(_ =>
        {
            Debug.Log("초기화 완료");
        });
    }

    IEnumerator InitializeCoroutine()
    {
        // 초기화 시작
        // ...
        // 초기화 끝

        yield return null;

        // 전달될 값이 중요하지 않기 때문에 Unit 로 충분
        initializeSubject.OnNext(Unit.Default);
        initializeSubject.OnCompleted();
    }
}
