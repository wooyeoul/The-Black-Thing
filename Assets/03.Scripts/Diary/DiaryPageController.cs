using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryPageController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isClick;
    float clickTime = 0.0f;
    [SerializeField]
    float minClickTime;
    public void OnEnable()
    {
        isClick = false;
    }

    public void ButtonUp()
    {
        isClick = true;
    }

    public void ButtonDown()
    {
        isClick = false;
        Debug.Log("Click 꾸욱");

        if (clickTime >= minClickTime)
        {
            //특정 동작 수행
            Debug.Log("Click 꾸욱");
        }
    }

    public void Update()
    {
        if(isClick)
        {
            clickTime += Time.deltaTime;
        }
        else 
        {
            clickTime = 0.0f;
        }
    }
}
