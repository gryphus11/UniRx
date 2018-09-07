using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTimeView : MonoBehaviour
{

    [SerializeField]
    private CTimeCounter _timerCounter = null;

    [SerializeField]
    private Text _counterText = null;

    private void Start()
    {
        // 이벤트의 변화를 uGui에 갱신 
        //_timerCounter.OnTimeChanged += ShowTimer;

        _timerCounter.OnTimeChanged += time =>
        {
            if (_counterText != null)
            {
                _counterText.text = "Event : " + time.ToString();
            }
        };
    }

    private void ShowTimer(int time)
    {
        if (_counterText != null)
        {
            _counterText.text = "Event : " + time.ToString();
        }
    }
}
