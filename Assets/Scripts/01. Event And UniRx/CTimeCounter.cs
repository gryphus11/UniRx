using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimeCounter : MonoBehaviour
{

    public delegate void TimeEventHandler(int time);
    public event TimeEventHandler OnTimeChanged;

    // Use this for initialization
    void Start()
    {
        // 타이머 가동
        StartCoroutine(TimerCoroutine());
    }

    /// <summary>
    /// 100초를 카운트하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerCoroutine()
    {
        int time = 100;

        while (time > 0)
        {
            --time;

            // 이벤트 통지
            OnTimeChanged(time);

            yield return new WaitForSeconds(1);
        }
    }
}
