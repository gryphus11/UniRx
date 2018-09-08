'Subscribe 는 IObservable 의 기능을 호출하고 있다.'<br>
바꿔 말하면 이말은 곧 'IObservable 을 상속하면 뭐든지 Subscribe가 가능하다.'<br>
상대가 Subject 인지 아닌지에 관계없이 IObservable 이 있다면 Subscribe 가 가능해진다.<br>

<img src="https://user-images.githubusercontent.com/42706180/45249609-10813100-b35e-11e8-8085-c29f913e96e0.png">
<img src="https://user-images.githubusercontent.com/42706180/45249610-124af480-b35e-11e8-82c7-bbf5797db691.png"><br>

이런 다양한 발상 아래, Subject 와 말단의 Subscribe 의 사이에 끼어들어 이런저런 메시지를 처리하는 부품을 UniRx 에서는 '오퍼레이터' 라고 부릅니다.<br>
UniRx 의 오퍼레이터 일부를 소개 하자면,<br>
&emsp;&emsp;● Where : 필터링 한다.<br>
&emsp;&emsp;● Select : 메시지를 변환한다.<br>
&emsp;&emsp;● Distinct : 중복을 배제한다.<br>
&emsp;&emsp;● Buffer : 일정 개수 정리가 될 때 까지 대기.<br>
&emsp;&emsp;● ThrottleFirst : 단시간에 한꺼번에 메시지가 오는 경우 선두의 것만을 사용.<br>
등이 존재한다.<br>

자, 여기까지 'Subject 를 Subscribe 한다.' 또는 'Subject 에 오퍼레이터를 끼워 Subscribe 한다.' 와 같은 표현을 사용해 왔습니다. 역시 매번 이런 용어를 사용한다면 괴롭기 때문에, 이것들을 표현하는 단어로서 '스트림' 을 소개합니다.<br><br>

UniRx 의 '스트림' 은, '메시지가 발행되고 부터 Subscribe에 도달하기 까지의 일련의 처리의 흐름' 을 표현하는 키워드 입니다.<br>
'오퍼레이터를 조합하여 스트림을 구축한다.', 'Subscribe 하고 스트림을 가동시킨다.', 'OnCompleted를 발행하여 스트림을 정지시킨다.'<br>
등의 사용 방법입니다.<br>

<img src="https://user-images.githubusercontent.com/42706180/45249864-ba62bc80-b362-11e8-80d2-cca528b7e833.png"><br>
