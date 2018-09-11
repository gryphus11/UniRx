<ul>
  <li><h1>스트림의 소스(메시지 발행원)란?</h1>
    <ul>
      <li>
        UniRx 의 스트림은, 다음 3개로 구성되어 있습니다.
        <ul>
          <li>메시지를 발행하는 소스가 되는 것(Subject등)</li>
          <li>메시지를 전파하는 오퍼레이터(Where, Select 등)</li>
          <li>메시지를 구독하는 것(Subscribe)</li>
        </ul>
        이번에는 스트림의 발단이되는 스트림 소스에 대해 소개합니다.
      </li>
    </ul>
  </li>
  
  <li><h1>스트림 소스가 되는 것들</h1>
    스트림 소스를 사용하는 방법은 몇 가지 존재합니다. UniRx 에 준비된 스트림 소스를 이용할 수도 있고,<br>
    물론 자신이 스트림 소스를 직접 만드는 것도 가능합니다. UniRx 를 이용하는 경우는 다음과 같습니다.
    <ul>
      <li>Subject 시리즈</l>
      <li>ReactiveProperty 시리즈</li>
      <li>팩토리 메서드 시리즈</li>
      <li>UniRx.Triggers 시리즈</li>
      <li>코루틴을 변환하여 사용</li>
      <li>uGUI 이벤트를 변환하여 사용</li>
      <li>그 외의 UniRx 에 존재하는 것들을 사용</li>
    </ul><br>
    <ul>
      <li>
        <h2>Subject 시리즈</h2>
        몇가지 파생이 존재하며 각각 다른 거동을 취합니다. 용도에 맞추어 적절히 이용해 줍시다.
        <table border="2">
          <tr>
            <th>Subject</th>
            <th>기능</th>
          </tr>
          <tr>
            <td>Subject&ltT&gt</td>
            <td>가장 심플한 것. OnNext 가 실행 되면 값을 발행.</td>
          </tr>
          <tr>
            <td>BehaviorSubject&ltT&gt</td>
            <td>최후에 발행한 값을 캐시하여, 이후의 Subscribe 시에 그 값을 발행해준다.<br>
                초기값을 설정 해야함.
            </td>
          </tr>
          <tr>
            <td>ReplaySubject&ltT&gt</td>
            <td>과거 모든 발행된 값을 캐시하여, 이후의 Subscribe 시에 그 값을 전부 발행.</td>
          </tr>
          <tr>
            <td>AsyncSubject&ltT&gt</td>
            <td>OnNext 값을 즉시 발행하지 않고 내부에 캐시하여, OnCompleted() 가 실행된 타이밍에 가장 마지막의 OnNext 를 1개만 발행<br>
                Future 나 Promise 같은것. 비동기로 처리를 하고 결과를 끝날무렵 꺼내고 싶을 때에 사용할수 있다.
            </td>
          </tr>
        </table>
      </li><br>
      <li>
        <h2>ReactiveProperty 시리즈</h2>
        <ul>
          <li><h3>ReactiveProperty&ltT&gt</h3>
          <ul>
            <li>ReactiveProperty&ltT&gt 는 보통의 변수에 Subject 의 기능을 붙인것 입니다.<br>
              변수의 느낌으로 정의해 사용할 수 있어서 사용이 쉬움.
            </li>
            <li>ex)<br>
              ReactiveProperty&ltint&gt reactiveProperty = new ReactiveProperty&ltint&gt(10);<br>
              reactiveProperty.Value = 20;<br>
              int currentValue = reactiveProperty.Value;<br>
            </li>
            <li>위의 ReactiveProperty&ltT&gt 는 인스펙터에 표시되지 않지만,<br>
                각각의 형에 대한 ReactiveProperty 가 존재하며 이를 이용해 인스펙터에 표시할 수 있습니다.<br>
                IntReactiveProperty, StringReactiveProperty, ...<br>
                ex) IntReactiveProperty playerReactiveHealth = new IntReactiveProperty(100);<br>
                enum 형도 존재합니다만, 조금더 공부가 필요.<br>
                원작자 홈페이지 (일본어)<a href="http://neue.cc/2015/04/13_510.html">경량 이벤트 후크와 uGUI연결에 따른 데이터바인딩</a>
            </li>
          </ul>
          </li>
          <li><h3>ReactiveCollection&ltT&gt</h3>
          <ul>
            <li>ReactiveCollection&ltT&gt 는 ReactiveProperty 와 같은 것으로,<br>
                상태의 변화를 통지하는 기능을 내장한 List<T> 입니다.
            </li>
            <li>ReactiveCollection 은 보통의 리스트와 같이 사용 가능하며, 상태의 변화를 Subscribe 할 수 있도록 되어있습니다.<br>
                사용 가능한 이벤트는 다음과 같습니다.
              <ul>
                <li>요소의 추가</li>
                <li>요소의 삭제</li>
                <li>요소의 수 변화</li>
                <li>요소의 덮어쓰기</li>
                <li>요소의 이동</li>
                <li>리스트의 클리어</li>
              </ul>
            </li>
          </ul>
          </li>
          <li><h3>ReactiveDictionary&ltT1, T2&gt</h3>
          <ul>
              <li>Dictionary 버전. ReactiveCollection 과 동작이 대부분 같으므로 생략.</li>
          </ul>
          </li>
        </ul>
      </li>
      <li><h2>팩토리 메서드 시리즈</h2>
        팩토리 메서드란, UniRx 가 제공하고 있는 스트림 소스 구축 메서드의 집합 입니다.<br>
        Subject 만으로는 표현할 수 없는 복잡한 스트림을 간단히 만드는 것이 가능한 경우가 있습니다.<br>
        Unity 에서 UniRx 를 이용하는 경우는 팩토리 메서드를 이용할 기회는 별로 없을지도 모르지만,<br>
        어딘가엔 도움이 될거라 생각하므로 배워두는게 좋을 것입니다.<br>
        단, 팩토리 메서드는 수가 많기 때문에, 이용 빈도가 높은 것을 발췌하여 소개합니다.<br>
        자세한 사항은 링크를 참고 : 
        <a href="http://reactivex.io/documentation/ko/operators.html">ReactiveX Creating Observables</a><br>
        <ul>
          <li><h3>Observable.Create&ltT&gt</h3>
            <ul>
              <li>자유롭게 값을 발행할 수 있는 팩토리 메서드 입니다.</li>
              <li>Observable.Create 는 인수로 Func<IObserver<T>, IDisposable> 
                (IObserver<T> 을 받아서 IDisposable 을 반환하는 함수)<br>
                를 가집니다.</li>
              <li>사용 예<br>
                Observable.Create&ltT&gt(observer =&gt<br>
                {<br>
                &emsp;&emsp;// 내용<br>
                &emsp;&emsp;observer.OnCompleted();<br>
                &emsp;&emsp;return Disposable.Create(() =&gt<br>
                &emsp;&emsp;{<br>
                &emsp;&emsp;&emsp;&emsp;// 내용<br>
                &emsp;&emsp;});<br>
                }).Subscribe( //내용 );</li>
            </ul>
          </li>
          <li><h3>Observable.Start</h3>
            <ul>
              <li>주어진 블록을 별개의 스레디에서 실행하고 결과를 1개만 발행하는 팩토리 메서드 입니다.</li>
              <li>비동기로 무언가 처리하고, 결과가 나오면 통지하고 싶은 때에 이용할 수 있습니다.</li>
              <li>사용 예<br>
                Observable.Start(() =&gt<br>
                {<br>
                &emsp;&emsp;// 비동기 처리 작업 내용
                })<br>
                .ObserveOnMainThread() // 반드시!!! 메인 스레드로 오기위해 사용할것.<br>
                .Subscribe(<br>
                &emsp;&emsp;// 비동기 처리후 작업 내용<br>
                );<br>
              </li>
              <li>주의할점이 있습니다. Observable.Start 는 처리를 별개의 스레드에서 실행해 그 스레드로부터 그대로 Subscribe 내의 함수를 실행합                 니다. 이것은 스레드 세이프가 아닌 Unity 에 대해서 문제를 일으킬 수 있어 주의할 필요가 있습니다.</li>
              <li>메시지를 별개의 스레드로부터 메인 스레드로 전환하고싶은 경우, ObserveOnMainThread 라는 오퍼레이터를 이용합시다. 
                이 오퍼레이터를 끼움으로써, 이 오퍼레이터 이후 Unity 의 메인 스레드에서 실행되도록 변환합니다.</li>
            </ul>
          </li>
          <li><h3>Observable.Timer/TimerFrame</h3>
            <ul>
              <li>Observable.Timer 는 일정시간 후에 메시지를 발행하는 심플한 팩토리 메서드 입니다.</li>
              <li>실시간으로 지정할 경우는 Timer 를, Unity 의 프레임 수로 지정할 경우는 TimerFrame 을 이용합니다.</li>
              <li>Timer / TimerFrame 은 인수에 따라 동작이 다릅니다. 1개만 지정한 경우 OneShot 과 같은 동작으로 종료하고,
              2개를 지정한 경우는 정기적으로 메시지를 발행하는 동작이 됩니다. 또, 스케쥴러로 실행할 스레드를 지정하는 것도 가능합니다.</li>
              <li>비슷한 팩토리 메서드로 Observable.Interval/IntervalFrame 이 존재합니다. 이쪽은 Timer/TimerFrame 의 2개의 인수를 
                지정하는 경우의 생략판 같이 되어 있습니다. Interva/IntervalFrame 에는 타이머를 기동하기까지의 대기 시간을 
                입력할 수 없습니다.</li>
              <li>Timer TimerFrame 을 정기적 실행을 할 경우, Dispose 하는것을 잊지 않도록 충분히 주의할 필요가 있습니다.</li>
            </ul>
          </li>
        </ul>
      </li>
      <li><h2>UniRx.Triggers 시리즈</h2>
        UniRx.Triggers 는, using UniRx.Triggers; 로 이용가능한 스트림 소스 입니다. Unity 의 콜백 이벤트를 UniRx 의 IObservable 에 
        변환하여 제공해 줍니다. UniRx 에는 이것이 가장 중요하고 편리하다 생각합니다.<br>
        Triggers 는 수가 매우 많아 링크를 참고해 주세요. <a href="https://github.com/neuecc/UniRx/wiki/UniRx.Triggers">UniRx.Triggers</a>
        유니티가 제공하는 대부분의 콜백 이벤트를 스트림으로써 취득가능하게 되었고, GameObject가 Destroy된 때에 OnCompleted 를 자동적으로 
        발행해주어 수명관리의 걱정도 없습니다.
      </li>
    </ul>
  </li>
</ul>
