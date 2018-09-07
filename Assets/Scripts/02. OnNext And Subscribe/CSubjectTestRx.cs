using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class CSubjectTestRx : MonoBehaviour {

    public Text logTextUi = null;

    private Subject<string> _testSubject = new Subject<string>();

	// Use this for initialization
	void Start () {

        // 동작 안함! 구독(Subscribe)이 먼저 구현되지 않았기 때문에 메시지를 발행해도 반응없음
        //_testSubject.OnNext("Hello World!");
        //_testSubject.OnNext("안녕하세요!");

        _testSubject.Subscribe(msg => Debug.Log("Subscribe1 : " + msg));
        _testSubject.Subscribe(msg => Debug.Log("Subscribe2 : " + msg));
        _testSubject.Subscribe(msg => Debug.Log("Subscribe3 : " + msg));

        _testSubject.Subscribe(msg => 
        {
            if (logTextUi != null)
            {
                logTextUi.text += msg + "\n";
            }
        });

        _testSubject.OnNext("Hello World!");
        _testSubject.OnNext("안녕하세요!");
    }
}
