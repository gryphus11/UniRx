using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class CSubjectDispose : MonoBehaviour {

    [SerializeField]
    private int timeLeft = 5;
    
    // Dispose Test
    private Subject<int> integerSubject = new Subject<int>();

    // 메모리 누수 방지 테스트를 위한 타이머
    private Subject<int> timerSubject = new Subject<int>();

    public IObservable<int> OnTimeChanged {
        get { return timerSubject; }
    }

    // Use this for initialization
    void Start() {

        #region Dispose Test
        // IDisposable 을 기억
        IDisposable disposable1 = integerSubject.Subscribe(
            x => Debug.Log("Subscribe1 : " + x),
            () => Debug.Log("Completed1")).AddTo(gameObject);

        IDisposable disposable2 = integerSubject.Subscribe(
            x => Debug.Log("Subscribe2 : " + x),
            () => Debug.Log("Completed2")).AddTo(gameObject);

        integerSubject.OnNext(1);
        integerSubject.OnNext(2);

        // 구독 종료
        // 일부 스트림만 구독을 종료하도록 가능.
        disposable1.Dispose();

        // 구독이 종료된 Subject 는 메시지 전달이 불가하여 동작하지 않음.
        // OnCompleted 에 무언가의 처리가 필요한 경우 Dispose 를 해버리면 동작하지 않기때문에 주의.
        // 이 경우는 disposable1 이 구독 중지됨.
        integerSubject.OnNext(3);
        integerSubject.OnCompleted();

        disposable2.Dispose();
        #endregion

        #region AddTo Test
        StartCoroutine(TimerCoroutine());
        timerSubject.Subscribe(x => Debug.Log(x));
        #endregion
    }

    IEnumerator TimerCoroutine()
    {
        yield return null;

        int time = timeLeft;

        while (time >= 0)
        {
            timerSubject.OnNext(time);
            --time;
            yield return new WaitForSeconds(1.0f);
        }
        timerSubject.OnCompleted();
    }
}
