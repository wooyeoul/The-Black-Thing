using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiaryPageController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isClick;
    float clickTime = 0.0f;
    [SerializeField]
    float minClickTime;

    [SerializeField]
    TMP_Text title;

    [SerializeField]
    TMP_Text leftPage;


    [SerializeField]
    TMP_Text subTitle;

    [SerializeField]
    Image []subImg;
    [SerializeField]
    TMP_Text []subTxt;

    TranslateManager translator;
    private void Start()
    {
        translator = GameObject.FindWithTag("Translator").GetComponent<TranslateManager>();
        translator.translatorDel += Translate;
    }
    void Translate(LANGUAGE language, TMP_FontAsset font)
    {
        int Idx = (int)language;
        
        
        title.font = font;
        leftPage.font = font;
        subTitle.font = font;
        for(int i=0;i<subTxt.Length;i++)
        {
            subTxt[i].font = font;
            //Txt변경
        }

    }

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
