using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CInterfaceTest<T> : MonoBehaviour, IObserver<T>, IObservable<T>
{
    #region 인터페이스의 구현해야할 내용

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(T value)
    {
        throw new NotImplementedException();
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        throw new NotImplementedException();
    }

    #endregion

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
