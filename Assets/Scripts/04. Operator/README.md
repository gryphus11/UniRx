"Subscribe 는 IObservable 의 기능을 호출하고 있다."<br>
바꿔 말하면 이말은 곧 "IObservable 을 상속하면 뭐든지 Subscribe가 가능하다."<br>
상대가 Subject 인지 아닌지에 관계없이 IObservable 이 있다면 Subscribe 가 가능해진다.<br>

<img src="https://user-images.githubusercontent.com/42706180/45249609-10813100-b35e-11e8-8085-c29f913e96e0.png">
<img src="https://user-images.githubusercontent.com/42706180/45249610-124af480-b35e-11e8-82c7-bbf5797db691.png">


이런 다양한 발상 아래, Subject 와 말단의 Subscribe 의 사이에 끼어들어 이런저런 메시지를 처리하는 부품을 UniRx 에서는 "오퍼레이터" 라고 부릅니다.<br>
