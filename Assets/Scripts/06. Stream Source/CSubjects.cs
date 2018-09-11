using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CSubjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //ShowBehaviorSubject();
        //ShowReplaySubjects();
        ShowAsyncSubject();
	}
	
    private void ShowBehaviorSubject()
    {
        BehaviorSubject<int> behaviorSubject = new BehaviorSubject<int>(100);

        // 초기값 100부터 아래의 모든 OnNext 를 받음.
        behaviorSubject.Subscribe(x => Debug.Log("Subscribe1 : " + x));

        behaviorSubject.OnNext(30);
        behaviorSubject.OnNext(40);
        behaviorSubject.OnNext(50);
        
        // 마지막 값 50을 캐시하여 마지막 OnNext 이후부터 나오는 Subscribe 에 발행.
        behaviorSubject.Subscribe(x => Debug.Log("Subscribe2 : " + x));
        behaviorSubject.Subscribe(x => Debug.Log("Subscribe3 : " + x));

        behaviorSubject.OnCompleted();
    }

    private void ShowReplaySubjects()
    {
        ReplaySubject<string> replaySubject = new ReplaySubject<string>();

        replaySubject.Subscribe(x => Debug.Log("Subscribe1 : " + x));

        replaySubject.OnNext("ReplaySubject");
        replaySubject.OnNext("Test");
        replaySubject.OnNext("Brunhild");

        // 모든 값을 캐시하여 이후의 Subscribe 에 똑같이 발행.
        replaySubject.Subscribe(x => Debug.Log("Subscribe2 : " + x));
        replaySubject.Subscribe(x => Debug.Log("Subscribe3 : " + x));

        replaySubject.OnCompleted();
    }

    private void ShowAsyncSubject()
    {
        // OnCompleted 가 실행되는 시점 기준으로 가장 마지막 OnNext 의 값만 발행
        AsyncSubject<float> asyncSubject = new AsyncSubject<float>();
        asyncSubject.Subscribe(x => Debug.Log("Subscribe1 : " + x));

        asyncSubject.OnNext(15.5f);
        //asyncSubject.OnCompleted();
        asyncSubject.OnNext(36.5f);
        //asyncSubject.OnCompleted();
        asyncSubject.OnNext(72.3f);
        //asyncSubject.OnCompleted();

        asyncSubject.Subscribe(x => Debug.Log("Subscribe2 : " + x));
        asyncSubject.Subscribe(x => Debug.Log("Subscribe3 : " + x));
        asyncSubject.OnCompleted();
    }
}
