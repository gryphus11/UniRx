using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class COperatorTest : MonoBehaviour {

    [SerializeField]
    private Text logTextUi = null;

    private Subject<string> operatorTestSubject = new Subject<string>();

	// Use this for initialization
	void Start () {

        // 콘솔창 출력, 필터는 "Enemy" 를 포함하는 스트링값
        operatorTestSubject
            .Where(x => x.Contains("Enemy"))
            .Subscribe(x => Debug.LogFormat("플레이어가 {0} 에 충돌했습니다.", x));

        // UI Text 출력, 필터는 "Wall" 을 포함하는 스트링값
        operatorTestSubject
            .Where(x => x.Contains("Wall"))
            .Subscribe(x => 
            {
                if (logTextUi != null)
                {
                    logTextUi.text += "플레이어가 " + x + " 에 충돌했습니다.\n";
                }
            });

        operatorTestSubject.OnNext("Enemy");
        operatorTestSubject.OnNext("Wall");
        operatorTestSubject.OnNext("Wall");
        operatorTestSubject.OnNext("Enemy");
        operatorTestSubject.OnNext("Enemy");

    }
}
