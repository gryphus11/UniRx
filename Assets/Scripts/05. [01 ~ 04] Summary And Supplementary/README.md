UniRx 에서 발행되는 메시지는 3종류 내의 어떤것이 되고,<br>
다음과 같은 용도로 사용됩니다.<br>
&emsp;&emsp;● OnNext : 통상 이벤트가 발행되었을 때에 통지하는 메시지.
&emsp;&emsp;&emsp;&emsp;◎ Unit 형 이라고 하는 특수한 형을 발행 가능.(UniRx.Unit)
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ 이벤트가 발생한 타이밍이 중요하고, OnNext 메시지의 내용은 뭐든 상관 없는 경우.
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ 예를들어, Scene 의 초기화 완료 또는 플레이어 사망과 같은 때에 사용가능.
&emsp;&emsp;● OnError : 스트림의 처리중에 예외가 발생한 것을 통지.
&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;▷ OnError 메시지가 Subscribe 까지 도달한 경우, 그 스트림의 구독은 종료되고 파괴됩니다.
&emsp;&emsp;● OnCompleted : 스트림이 종료된 것을 통지.
