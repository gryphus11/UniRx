using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CReactiveProperty : MonoBehaviour
{
    public ReactiveProperty<int> reactiveProperty = new ReactiveProperty<int>(10);

    // 인스펙터에 사용하고 싶다면 각각의 리액티브 프로퍼티를 사용합시다.
    public IntReactiveProperty playerReactiveHealth = new IntReactiveProperty(0);

    private ReactiveCollection<string> reactiveCollection = new ReactiveCollection<string>();
    private ReactiveDictionary<string, float> reactiveDictionary = new ReactiveDictionary<string, float>();

    // Use this for initialization
    void Start()
    {
        //ShowReactiveProperty();
        //ShowIntReactiveProperty();
        ShowReactiveCollection();
    }

    private void ShowReactiveProperty()
    {
        reactiveProperty.Value = 20;
        int currentValue = reactiveProperty.Value;
        reactiveProperty.Subscribe(x => Debug.Log(x));
        reactiveProperty.Value = 30;
    }

    private void ShowIntReactiveProperty()
    {
        int hp = playerReactiveHealth.Value;
        Debug.Log("Player HP : " + hp);

        playerReactiveHealth.Value = 100;
        // 위로 마지막 변화값에 대해 출력후 아래로 값이 변할때마다 대응.
        playerReactiveHealth.Subscribe(x => Debug.Log("Remain HP1 : " + x));
        playerReactiveHealth.Value -= 10;
        playerReactiveHealth.Value -= 10;
        playerReactiveHealth.Subscribe(x => Debug.Log("Remain HP2 : " + x));
        playerReactiveHealth.Value -= 10;
        playerReactiveHealth.Subscribe(x => Debug.Log("Remain HP3 : " + x));
        playerReactiveHealth.Value -= 10;
    }

    private void ShowReactiveCollection()
    {
        // Subscribe 가 구현 된 이후의 변화에 대해 동작함.
        reactiveCollection.ObserveAdd().Subscribe(x => Debug.Log("요소 추가 : " + x));
        reactiveCollection.ObserveCountChanged().Subscribe(x => Debug.Log("요소의 수 변경 : " + x));
        reactiveCollection.ObserveMove().Subscribe(x => Debug.Log("요소 이동 : " + x));
        reactiveCollection.ObserveRemove().Subscribe(x => Debug.Log("요소 제거 : " + x));
        reactiveCollection.ObserveReset().Subscribe(x => Debug.Log("클리어 : " + x));
        
        reactiveCollection.Add("Unity Reactive Extension");
        reactiveCollection.Add("ReactiveProperty Series");
        reactiveCollection.Add("Sample");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy CReactiveProperty");
        reactiveProperty.Dispose();
        playerReactiveHealth.Dispose();
        reactiveCollection.Clear();
        reactiveCollection.Dispose();
        reactiveDictionary.Clear();
        reactiveDictionary.Dispose();
    }
}
