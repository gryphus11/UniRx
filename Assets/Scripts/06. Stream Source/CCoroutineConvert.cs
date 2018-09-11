using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CCoroutineConvert : MonoBehaviour {

    public bool IsPaused { get; private set; }

	// Use this for initialization
	void Start ()
    {
        Observable.FromCoroutine<int>(observer => GameTimerCoroutine(observer, 60))
            .Subscribe(time => Debug.Log(time));
	}

    IEnumerator GameTimerCoroutine(IObserver<int> observer, int initialCount)
    {
        int currentCount = initialCount;

        while (currentCount >= 0)
        {
            if (!IsPaused)
            {
                observer.OnNext(currentCount);
                --currentCount;
            }

            yield return new WaitForSeconds(1.0f);
        }

        observer.OnCompleted();
    }
}

