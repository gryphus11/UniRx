using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class CErrorMessageTest : MonoBehaviour
{

    public Text errorLogTextUi = null;

    private Subject<string> stringSubject = new Subject<string>();

    // Use this for initialization
    void Start()
    {

        // OnError : 도중에 발생한 예외를 Subscribe 에 받음. 스트림 중단.
        stringSubject
            .Select(x => int.Parse(x))
            .Subscribe(
                x => Debug.Log("OnError 성공 : " + x),
                ex => Debug.Log("OnError 예외발생 : " + ex.Message),
                () => Debug.Log("OnError Completed")
            );

        // OnErrorRetry : 도중에 예외가 발생했다면 에러 처리를 수행 후 Subscribe 를 수행.
        stringSubject
            .Select(x => int.Parse(x))
            .OnErrorRetry((System.Exception ex) =>
            {
                Debug.Log("OnErrorRetry 예외가 발생하여 재구독 합니다 : " + ex.Message);
            })
            .Subscribe(
                x => Debug.Log("OnErrorRetry 성공 : " + x),
                // OnErrorRetry 에서 처리하기 때문에 예외 처리부를 구현하지 않아도 됨.
                //error => Debug.Log("OnErrorRetry 예외 발생 : " + error.Message),
                () => Debug.Log("OnErrorRetry Completed")
            );

        // CatchIgnore : 예외 처리를 한 이후에 OnCompleted 로 이동.
        stringSubject
            .Select(x => int.Parse(x))
            .CatchIgnore((System.Exception ex) =>
            {
                Debug.Log("CatchIgnore 예외 발생 : " + ex.Message);
            })
            .Subscribe(
                x => Debug.Log("CatchIgnore 성공 : " + x),
                // CatchIgnore 에서 처리하기 때문에 예외 처리부를 구현하지 않아도 됨.
                //error => Debug.Log("CatchIgnore 예외 발생 : " + error.Message),
                () => Debug.Log("CatchIgnore Completed")
            );

        // Retry : 예외 발생시 다시 SUbscribe (재시도 회수 지정 가능)
        stringSubject
            .Select(x => int.Parse(x))
            .Retry(3)
            .Subscribe(
                x => Debug.Log("Retry 성공 : " + x),
                // 예외 처리를 하지 않고 바로 재시도를 하기 때문에 예외 처리부를 구현하지 않아도 됨.
                //error => Debug.Log("Retry 예외 발생 : " + error.Message),
                () => Debug.Log("Retry Completed")
            );

        stringSubject.OnNext("1");
        stringSubject.OnNext("2");
        stringSubject.OnNext("Hello");
        stringSubject.OnNext("3");
        stringSubject.OnNext("4");
        stringSubject.OnNext("5");
        stringSubject.OnCompleted();
    }

    private void OnStreamError()
    {
        Debug.Log("Catch Error");
    }
}
