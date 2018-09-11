<ol>
  <li>스트림의 소스(메시지 발행원)란?
    <ul>
      <li>
        UniRx 의 스트림은, 다음 3개로 구성되어 있습니다.
        <ol>
          <li>메시지를 발행하는 소스가 되는 것(Subject등)</li>
          <li>메시지를 전파하는 오퍼레이터(Where, Select 등)</li>
          <li>메시지를 구독하는 것(Subscribe)</li>
        </ol>
        이번에는 스트림의 발단이되는 스트림 소스에 대해 소개합니다.
      </li>
    </ul>
  </li><br>  
  
  <li>스트림 소스가 되는 것들<br>
    스트림 소스를 사용하는 방법은 몇 가지 존재합니다. UniRx 에 준비된 스트림 소스를 이용할 수도 있고,<br>
    물론 자신이 스트림 소스를 직접 만드는 것도 가능합니다. UniRx 를 이용하는 경우는 다음과 같습니다.
    <ul>
      <li>Subject 시리즈</li>
      <li>ReactiveProperty 시리즈</li>
      <li>팩토리 메서드 시리즈/li>
      <li>UniRx.Triggers 시리즈</li>
      <li>코루틴을 변환하여 사용</li>
      <li>uGUI 이벤트를 변환하여 사용</li>
      <li>그 외의 UniRx 에 존재하는 것들을 사용</li>
    </ul><br>
    <ul>
      <li>
        Subject 시리즈<br>
        몇가지 파생이 존재하며 각각 다른 거동을 취합니다. 용도에 맞추어 적절히 이용해 줍시다.
        <table border="2">
          <tr>
            <th>Subject</th>
            <th>기능</th>
          </tr>
          <tr>
            <td>Subject<T></td>
            <td>가장 심플한 것. OnNext 가 실행 되면 값을 발행.</td>
          </tr>
          <tr>
            <td>BehaviorSubject<T></td>
            <td>최후에 발행한 값을 캐시하여, 이후의 Subscribe 시에 그 값을 발행해준다.<br>
                초기값을 설정 해야함.
            </td>
          </tr>
          <tr>
            <td>ReplaySubject<T></td>
            <td>과거 모든 발행된 값을 캐시하여, 이후의 Subscribe 시에 그 값을 전부 발행.</td>
          </tr>
          <tr>
            <td>AsyncSubject<T></td>
            <td>OnNext 값을 즉시 발행하지 않고 내부에 캐시하여, OnCOmpleted() 가 실행된 타이밍에 가장 마지막의 OnNext 를 1개만 발행<br>
                Future 나 Promise 같은것. 비동기로 처리를 하고 결과를 끝날무렵 꺼내고 싶을 때에 사용할수 있다.
            </td>
          </tr>
        </table>
      </li>
    </ul>
  </li><br>
</ol>
