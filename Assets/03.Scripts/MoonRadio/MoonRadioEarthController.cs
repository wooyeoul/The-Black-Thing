using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoonRadioEarthController : MonoBehaviour
{
    [SerializeField]
    GameObject sendEarth;
    [SerializeField]
    GameObject closeAlert;
    [SerializeField]
    GameObject sendAlert;

    [SerializeField]
    GameObject exceedAlert;

    [SerializeField]
    GameObject textLength;

    [SerializeField]
    GameObject answerTextBox;

    bool isCheckingWithin500;
    int textlineCnt;
    private void OnEnable()
    {
        textlineCnt = 0; 
        isCheckingWithin500 = true;
    }
    public void write2Moon(TMP_Text text)
    {
        //누르면, 박스는 사라진다.

        if (text.text.Length >= 1)
        {
            answerTextBox.SetActive(false);
        }
        else
        {
            answerTextBox.SetActive(true);
        }
        
        textlineCnt = text.text.Length;
        textLength.GetComponent<TMP_Text>().text = textlineCnt.ToString() + "/500";

        if (textlineCnt > 500)
        {
            isCheckingWithin500 = false;
            exceedAlert.SetActive(true);
        }
        else
        {
            exceedAlert.SetActive(false);
            isCheckingWithin500 = true;
        }
        //한글자라도 있으면 없애고, 한글자 존재하면 생김.
        //글씨 처리..
        //text.text는 moonbut누를시 전달될 string
    }

    public void OnEndEdit(TMP_Text text)
    {
        Debug.Log(text.text.Length);
    }

    public void send2MoonBut()
    {
        //textfield가 사라진다.
        //현재 누른 오브젝트 실행 후 애니메이션 끝나면 함수 실행
        //Debug.Log(inputText);
        if (isCheckingWithin500 == false) //500글자 이내
        {
            exceedAlert.SetActive(true);
            //전송 불가능함.
            return;
        }
        exceedAlert.SetActive(false);
        GameObject currObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        currObj.GetComponent<Animator>().SetBool("isGoing", true);
        sendEarth.GetComponent<Animator>().SetBool("isGoing", true);
    }
}
