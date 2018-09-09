UniRx 에서 발행되는 메시지는 3종류 내의 어떤것이 되고,<br>
다음과 같은 용도로 사용됩니다.<br>
&emsp;&emsp;● OnNext : 통상 이벤트가 발행되었을 때에 통지하는 메시지.<br>
&emsp;&emsp;&emsp;&emsp;◎ Unit 형 이라고 하는 특수한 형을 발행 가능.(UniRx.Unit)<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ 이벤트가 발생한 타이밍이 중요하고, OnNext 메시지의 내용은 뭐든 상관 없는 경우.<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ 예를들어, Scene 의 초기화 완료 또는 플레이어 사망과 같은 때에 사용가능.<br>
&emsp;&emsp;● OnError : 스트림의 처리중에 예외가 발생한 것을 통지.<br>
&emsp;&emsp;&emsp;&emsp;◎ OnError 메시지가 Subscribe 까지 도달한 경우, 그 스트림의 구독은 종료되고 파괴됩니다.<br>
&emsp;&emsp;&emsp;&emsp;◎ 예외 핸들링용 오퍼레이터<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ Retry : OnError 가 오면 다시 Subscribe 시도. (재시도 회수 지정가능)<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ CatchIgnore : OnError 를 받아 에러 처리를 한 후, OnError 를 무시하고 OnCompleted 로 이동.<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ OnErrorRetry : OnError 가 오면 에러 처리를 한 후에 Subscribe 다시 수행. (시간 지정가능)<br>
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ 사용 예 : subject.Select(...).OnErrorRetry((...) => { // 예외 처리 내용 }).Subscribe(...)<br>
&emsp;&emsp;● OnCompleted : 스트림이 종료된 것을 통지.<br>
&emsp;&emsp;&emsp;&emsp;◎ '스트림이 완료 되었기 때문에 이후 메시지를 발행하지 않음.' 과 같은 것을 통지.<br>
&emsp;&emsp;&emsp;&emsp;◎ OnCompleted 메시지가 Subscribe 에 도달한 경우, OnError 와 마찬가지로 스트림 구독이 종료.<br><br>
UniRx 사용시 Subscribe 에는 복수의 오버로드가 존재합니다.<br>
&emsp;&emsp;● Subscribe(IObserber observer) ------------------------------기본형<br>
&emsp;&emsp;● Subscribe() --------------------------------------------------- 모든 메시지를 무시<br>
&emsp;&emsp;● Subscribe(Action onNext) ----------------------------------- OnNext 만<br>
&emsp;&emsp;● Subscribe(Action onNext, Action onError) ------------------ OnNext + OnError<br>
&emsp;&emsp;● Subscribe(Action onNext, Action onCompleted) ----------- OnNext + OnCompleted<br>
&emsp;&emsp;● Subscribe(Action onNext, Action onError, Action onCompleted) -- 전부<br>
&emsp;&emsp;● Subscribe 는 IDisposable 을 반환하도록 되어 있습니다. 리소스의 개방을 수행하도록 하는 메서드 입니다.<br>
&emsp;&emsp;&nbsp;&nbsp;&nbsp;Subscribe 메서드가 반환하는 IDisposable 의 Dispose 를 실행하면 스트림의 구독을 종료할수 있습니다.<br>
&emsp;&emsp;&nbsp;&nbsp;&nbsp;Subscribe IDisposable 에 Subscribe 를 기억하도록 하여 특정 스트림만 종료되도록 하기도 가능.<br>
<p>
&emsp;&emsp;ex)<br>
&emsp;&emsp;IDisposable disposable1 = integerSubject.Subscribe(<br>
&emsp;&emsp;&emsp;&emsp;x => Debug.Log("Subscribe1 : " + x), () => Debug.Log("Completed1"));<br><br>
&emsp;&emsp;IDisposable disposable2 = integerSubject.Subscribe(<br>
&emsp;&emsp;&emsp;&emsp;x => Debug.Log("Subscribe2 : " + x), () => Debug.Log("Completed2"));<br><br>
&emsp;&emsp;disposable1.Dispose();<br>
</p>
