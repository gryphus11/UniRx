using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class CFactoryMethods : MonoBehaviour
{
    private IObservable<int> intFactoryMethod = null;
    private List<IDisposable> disposableList = new List<IDisposable>();

    // Use this for initialization
    void Start()
    {
        ServicePointManager.ServerCertificateValidationCallback = OnRemoteCertificateValidationCallback;
        //ShowObservableCreate();
        //ShowObservableStart();
        ShowObservableTimer();
    }

    private void ShowObservableCreate()
    {
        intFactoryMethod = Observable.Create<int>(observer =>
        {
            Debug.Log("Start");

            for (int i = 0; i < 100; ++i)
            {
                observer.OnNext(i);
            }

            Debug.Log("Finished");
            observer.OnCompleted();
            return Disposable.Create(() =>
            {
                Debug.Log("Dispose");
            });
        });

        intFactoryMethod
            .Where(x => (x % 7 == 0))
            .Select(x => x * 2)
            .Subscribe(x => Debug.Log(x));
    }

    private void ShowObservableStart()
    {
        Observable.Start(() =>
        {
            WebRequest request = WebRequest.Create("https://www.google.com/");
            WebResponse response = request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        })
        .ObserveOnMainThread() // 메시지별 스레드로부터 유니티 메인 스레드로 전환 (매우 중요!)
        .Subscribe(
            x => Debug.Log(x),
            () => Debug.Log("Web Response Completed"));
    }

    private void ShowObservableTimer()
    {
        // 5초후 실행하고 종료.
        disposableList.Add(Observable.Timer(TimeSpan.FromSeconds(5))
            .Subscribe(
            _ => Debug.Log("5초 경과 했습니다."),
            () => Debug.Log("1 인수 타이머가 종료.")));

        disposableList.Add(Observable.Timer(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1))
            .Subscribe(
            _ => Debug.Log("일정 시간 대기후 일정 간격으로 실행합니다."),
            () => Debug.Log("2 인수 타이머가 종료"))
            .AddTo(gameObject));

        disposableList.Add(Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(
            _ => Debug.Log("일정 간격으로 실행"),
            () => Debug.Log("1 인수 인터벌 종료"))
            .AddTo(gameObject));

    }

    /// <summary>
    /// 인증에러 발생시 해결을 위한 콜백
    /// 참고:
    /// https://answers.unity.com/questions/792342/how-to-validate-ssl-certificates-when-using-httpwe.html
    /// https://qiita.com/satotin/items/a5392d04d2edad74f6ad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="certificate"></param>
    /// <param name="chain"></param>
    /// <param name="sslPolicyErrors"></param>
    /// <returns></returns>
    public bool OnRemoteCertificateValidationCallback(
        System.Object sender, 
        X509Certificate certificate, 
        X509Chain chain, SslPolicyErrors 
        sslPolicyErrors)
    {
        bool isOk = true;
        
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }

    private void OnDestroy()
    {
        int listCount = disposableList.Count;

        Debug.Log("Destroy CFactoryMethods : " + listCount);

        for (int i = 0; i < listCount; ++i)
        {
            disposableList[i].Dispose();
        }

        disposableList.Clear();
    }
}
