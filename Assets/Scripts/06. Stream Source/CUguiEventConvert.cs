using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class CUguiEventConvert : MonoBehaviour {

    [SerializeField]
    private Button leftButton = null;

    [SerializeField]
    private Button rightButton = null;

    [SerializeField]
    private Text buttonLog = null;

    [SerializeField]
    private InputField testInputField = null;

    [SerializeField]
    private Text initInputFieldLog = null;

    [SerializeField]
    private Text inputFieldLog = null;

    [SerializeField]
    private Text endEditLog = null;

    [SerializeField]
    private Slider testSlider = null;

    [SerializeField]
    private Text sliderLog = null;

	// Use this for initialization
	void Start () {

        leftButton.OnClickAsObservable()
            .Subscribe(_ => Debug.Log("버튼1 클릭됨."));

        rightButton.OnClickAsObservable()
            .Subscribe(_ => Debug.Log("버튼2 클릭됨."));

        leftButton.OnClickAsObservable()
            .ZipLatest(rightButton.OnClickAsObservable(), (b1, b2) => "Clicked!")
            .Repeat()
            .Subscribe(x => 
            {
                Debug.Log("두 버튼 모두 눌림!");
                buttonLog.text = x;
            });

        IObservable<Unit> clickStream = this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0));
        clickStream.Buffer(clickStream.Throttle(System.TimeSpan.FromMilliseconds(200)))
            .Where(x => x.Count >= 2)
            .Subscribe(_ => Debug.Log("더블클릭!"));

        // 아래 두개의 차이는, Subscribe 시에 현재의 값을 초기값으로 발행하는가 아닌가에 있음.
        // Subscribe 시에 초기값이 필요하다면 전자를 사용할것.
        testInputField.OnValueChangedAsObservable().Subscribe(text => 
        {
            Debug.Log("초기값 있음 : " + text);
            initInputFieldLog.text = "Initialized\n" + text;
        });  // 초기값 있음
        testInputField.onValueChanged.AsObservable().Subscribe(text => 
        {
            Debug.Log("초기값 없음 : " + text);
            inputFieldLog.text = "None\n" + text;
        }); // 초기값 없음

        testInputField.OnEndEditAsObservable().Subscribe(text => 
        {
            Debug.Log("수정중 : " + text);
            endEditLog.text = "End Edit Log\n" + text;
        });
        

        testSlider.OnValueChangedAsObservable().Subscribe(value => 
        {
            Debug.Log("슬라이드 값 : " + value);
            sliderLog.text = value.ToString();
        });
	}
}
