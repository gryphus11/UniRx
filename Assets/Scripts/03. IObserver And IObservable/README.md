더욱 정확한 설명을 하자면, [SUbject는 IObserver 인터페이스와 IObservable 인터페이스 2가지를 상속하고 있습니다.] 라는 표현이 되겠습니다.

IObserver 인터페이스<br>
&emsp;&emsp;이벤트 메시지를 발행 가능.<br>
&emsp;&emsp;OnNext : 위에 설명.<br>
&emsp;&emsp;OnError : 발생된 에러를 통지하는 메서드.<br>
&emsp;&emsp;OnCompleted : 메시지 발행이 완료된 것을 통지하는 메서드.<br>

IObservable 인터페이스<br>
&emsp;&emsp;이벤트 메시지를 구독 가능.<br>
&emsp;&emsp;Subscribe<br>

여기서 위화감이 들 수도 있다.

Subscribe는 람다식의 형태를 취하고 있는데 인수로 IObserver 를 취하고 있다.

어째서 이런게 가능할까?

바로 Subscribe의 생략호출용의 확장 메서드가 IObservable 에 준비되어 있기 때문이다.

subject.Subscribe(msg => Debug.Log("Subscribe1:" + msg));
의 경우는 생략 기법의 한 종류인 [OnNext 만을 이용하는 경우의 생략기법] 이다.

// OnNext
subject.Subscribe(msg => Debug.Log("Subscribe1:" + msg));

// OnNext & OnError
subject.Subscribe(
    msg => Debug.Log("Subscribe1:" + msg),
    error => Debug.LogError("Error" + error));//OnNext & OnCompleted

// OnNext & OnComplete
subject.Subscribe(
    msg => Debug.Log("Subscribe1:" + msg),
    () => Debug.Log("Completed"));//OnNext & OnError & OnCompleted

// All
subject.Subscribe(
    msg => Debug.Log("Subscribe1:" + msg),
    error => Debug.LogError("Error" + error),
    () => Debug.Log("Completed"));

<br><br>
<img src="https://user-images.githubusercontent.com/42706180/45212467-f13bc280-b2cf-11e8-8500-a1a778dc8ece.png">
