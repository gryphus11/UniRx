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
  </li><br>  
  
  <li><h1>스트림 소스가 되는 것들</h1><br>
    스트림 소스를 사용하는 방법은 몇 가지 존재합니다. UniRx 에 준비된 스트림 소스를 이용할 수도 있고,<br>
    물론 자신이 스트림 소스를 직접 만드는 것도 가능합니다. UniRx 를 이용하는 경우는 다음과 같습니다.
    <ul>
      <li>Subject 시리즈</l2>
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
      <li><h2>팩토리 메서드 </h2>
      </li>
    </ul>
  </li>
</ul>
