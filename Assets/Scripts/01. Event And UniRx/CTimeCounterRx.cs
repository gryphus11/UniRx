using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CTimeCounterRx : MonoBehaviour
{

    // 이벤트를 발행하는 핵이 되는 인스턴스
    private Subject<int> _timerSubject = new Subject<int>();

    // 이벤트의 구독측만을 공개
    public IObservable<int> OnTimeChanged {
        get { return _timerSubject; }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        int time = 100;

        while (time > 0)
        {
            --time;

            // 이벤트 발행
            _timerSubject.OnNext(time);

            yield return new WaitForSeconds(1);
        }
    }
}
