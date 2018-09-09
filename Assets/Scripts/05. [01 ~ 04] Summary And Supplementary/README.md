<p>
  UniRx 에서 발행되는 메시지는 3종류 내의 어떤것이 되고,<br>
  다음과 같은 용도로 사용됩니다.<br>
  <ul>
    <li>OnNext : 통상 이벤트가 발행되었을 때에 통지하는 메시지.<br>
      <ul>
        <li>Unit 형 이라고 하는 특수한 형을 발행 가능.(UniRx.Unit)
          <ul>
            <li>이벤트가 발생한 타이밍이 중요하고, OnNext 메시지의 내용은 뭐든 상관 없는 경우.</li>
            <li>예를들어, Scene 의 초기화 완료 또는 플레이어 사망과 같은 때에 사용가능.</li>
          </ul>
        </li>
      </ul>
    </li>
    <li>OnError : 스트림의 처리중에 예외가 발생한 것을 통지.</li>
      <ul>
        <li>OnError 메시지가 Subscribe 까지 도달한 경우, 그 스트림의 구독은 종료되고 파괴됩니다.</li>
        <li>예외 핸들링용 오퍼레이터
          <ul>
            <li>Retry : OnError 가 오면 다시 Subscribe 시도. (재시도 회수 지정가능)</li>
            <li>CatchIgnore : OnError 를 받아 에러 처리를 한 후, OnError 를 무시하고 OnCompleted 로 이동.</li>
            <li>OnErrorRetry : OnError 가 오면 에러 처리를 한 후에 Subscribe 다시 수행. (시간 지정가능)</li>
            <li>사용 예 : subject.Select(...).OnErrorRetry((...) => { // 예외 처리 내용 }).Subscribe(...)</li>
          </ul>
        </li>
      </ul>
    </li>
    <li>OnCompleted : 스트림이 종료된 것을 통지.
      <ul>
        <li>'스트림이 완료 되었기 때문에 이후 메시지를 발행하지 않음.' 과 같은 것을 통지.</li>
        <li>OnCompleted 메시지가 Subscribe 에 도달한 경우, OnError 와 마찬가지로 스트림 구독이 종료.</li>
      </ul>
    </li>
  </ul>
</p>

<p>
UniRx 사용시 Subscribe 에는 복수의 오버로드가 존재합니다.<br>
  <ul>
    <li>Subscribe(IObserber observer) ------------------------------기본형</li>
    <li>Subscribe() --------------------------------------------------- 모든 메시지를 무시</li>
    <li>Subscribe(Action onNext) ----------------------------------- OnNext 만</li>
    <li>Subscribe(Action onNext, Action onError) ------------------ OnNext + OnError</li>
    <li>Subscribe(Action onNext, Action onCompleted) ----------- OnNext + OnCompleted</li>
    <li>Subscribe(Action onNext, Action onError, Action onCompleted) -- 전부</li>
    <li>Subscribe 는 IDisposable 을 반환하도록 되어 있습니다. 리소스의 개방을 수행하도록 하는 메서드 입니다.
      <ul>
        <li>Subscribe 메서드가 반환하는 IDisposable 의 Dispose 를 실행하면 스트림의 구독을 종료할수 있습니다.</li>
        <li>Subscribe IDisposable 에 Subscribe 를 기억하도록 하여 특정 스트림만 종료되도록 하기도 가능.</li>
        <li>ex)<br>
            IDisposable disposable1 = integerSubject.Subscribe(<br>
            x => Debug.Log("Subscribe1 : " + x), () => Debug.Log("Completed1"));<br><br>
            IDisposable disposable2 = integerSubject.Subscribe(<br>
            x => Debug.Log("Subscribe2 : " + x), () => Debug.Log("Completed2"));<br><br>
            disposable1.Dispose();<br></li>
      </ul>
    </li>
  </ul>
</p>

<p>
스트림의 수명과 Subscribe 의 종료 타이밍<br>
  <ul>
    <li>UniRx 를 사용하면서 특히 신경쓰지 않으면 안되는 것은, 스트림의 라이프 사이클 입니다.</li>
    <li>오브젝트가 빈번히 출현과 삭제를 반복하는 Unity 에서는,<br>
      특히 이곳을 의식하지 않으면 퍼포먼스의 저하나 에러와 같은 동작 불량을 일으키게 됩니다.</li>
    <li>스트림의 실체는 누가 가지고 있을까?
      <ul>
      <li>스트림의 실체는 Subject 입니다.</li>
      <li>Subject 가 파괴된다면 스트림이 파괴되도록 되어 있습니다.</li>
      <li>스트림의 실체는 Subject 가 내부에 보존하는 호출 함수 리스트(및 함수에 관련된 메서드체인)</li>
      <li>Subject 가 파괴되면 스트림도 전부 파괴됨. 역으로 말하면, Subject 가 남아 있는 한 스트림은 계속 가동된다.</li>
      <li>스트림이 참조하고 있는 게임오브젝트가 먼저 파괴되어 방치되어 버리면, 스트림은 살아있는 채로</li>
      퍼포먼스의 저하, 메모리 누수, NullReferenceException 을 발생시킬 가능성 있음.</li>
      </ul>
    </li>
    <li>스트림의 수명 관리는 세심한 주의를 기울이고, 사용이 끝나면 반드시 Dispose를 호출, 또는 OnCompleted를 발행하는 습관을 들일것.</li>
    <li>AddTo(GameObject) : 지정 게임오브젝트가 Destroy 되면 자동적으로 Dispose 를 호출해주는 기능.
      <ul>
        <li>ex) subject.Where(...).Subscribe(...).AddTo(gameobject);</li>
      </ul>
    </li>
  </ul>
</p>
