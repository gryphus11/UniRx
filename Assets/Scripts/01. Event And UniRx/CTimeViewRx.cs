using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CTimeViewRx : MonoBehaviour
{

    [SerializeField]
    private CTimeCounterRx _timerCounterRx = null;

    [SerializeField]
    private Text _counterText = null;

    // Use this for initialization
    void Start()
    {

        // 타이머의 카운트카 변화한 이벤트를 받아 uGui 텍스트를 갱신
        _timerCounterRx.OnTimeChanged.Subscribe(time =>
        {
            // 현재의 타이머값을 UI에 반영
            _counterText.text = "UniRx : " + time.ToString();
        });
    }
}
