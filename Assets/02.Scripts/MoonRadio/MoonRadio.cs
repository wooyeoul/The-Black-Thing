using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoonRadio : MonoBehaviour
{
    [SerializeField]
    GameObject moonRadioController;
    [SerializeField]
    GameObject alert;

    Animator blinkMoonRadioAnim;

    private void Start()
    {
        blinkMoonRadioAnim = GetComponent<Animator>();
        moonRadioController = GameObject.Find("MoonRadio").transform.GetChild(0).gameObject;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }
        
        if(alert != null)
        {
            OpenAlert();
            //alert 알람뜨기
            return;
        }

        //시간대가 밤이 아닐 경우에는 아래는 작동하지 않는다.
        if (moonRadioController.activeSelf == false)
        {
            //밤일 때 speed 1, 밤이 아니면 0
            blinkMoonRadioAnim.SetFloat("speed", 0f);
            moonRadioController.SetActive(true);
        }
    }

    public void OpenAlert()
    {
        if (alert.activeSelf == false)
        {
            alert.SetActive(true);
            StartCoroutine(CloseAlter(alert));
        }
    }

    IEnumerator CloseAlter(GameObject alert)
    {
        yield return new WaitForSeconds(1.5f);
        alert.SetActive(false);
    }
}
