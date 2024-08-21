using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    GameObject MenuBut;
    [SerializeField]
    GameObject Icon;

    [SerializeField]
    GameObject DayProgressUI;
    [SerializeField]
    GameObject MenuDefault;
    [SerializeField]
    GameObject Helper;
    [SerializeField]
    GameObject MyPageUI;
    [SerializeField]
    GameObject TimeUI;
    [SerializeField]
    GameObject Default;
    [SerializeField]
    GameObject Replay;
    #region 챕터 변수
    [SerializeField]
    GameObject checkList;

    [SerializeField]
    GameObject dragIcon;

    [SerializeField]
    GameObject dragScroller;
    float dragScrollWidth = 0.0f;
    #endregion


    public void onMenu()
    {
        MenuDefault.SetActive(true);
        TimeUI.SetActive(false);
        checkList.SetActive(false);
        
        /* if (!Icon.activeSelf)
         {
             TimeUI.SetActive(false);
             //checklist의 부모
             checkList.transform.parent.gameObject.SetActive(false);
             Icon.transform.parent.gameObject.SetActive(true);
             this.gameObject.GetComponent<Animator>().SetBool("isDowning", false);
         }
         else
         {
             Icon.SetActive(false);
             this.gameObject.GetComponent<Animator>().SetBool("isDowning", true);
         }*/
    }

    public void offMenu()
    {
        if (Icon.activeSelf)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isDowning", true);
            Icon.SetActive(false);
        }
    }

    public void MenuoffExit()
    {
        //if (!SkipController.is_end)
        //    TimeUI.SetActive(true);
        checkList.transform.parent.gameObject.SetActive(true);
        Icon.transform.parent.gameObject.SetActive(false);
    }
    public void MenuAniExit()
    {
        Icon.SetActive(true);
    }
    public void onDayProgressUI()
    {
        //DayProgressUI on,.,
        DayProgressUI.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickHelper()
    {
        Helper.SetActive(true);
        MenuDefault.SetActive(false);
    }

    public void onClickMypage()
    {
        MyPageUI.SetActive(true);
        MenuDefault.SetActive(false);
    }

    IEnumerator CloseAlter(GameObject checkList)
    {
        yield return new WaitForSeconds(2f);
        checkList.SetActive(false);
    }
    public void onClickCheckListIcon()
    {
        if (checkList.activeSelf == false)
        {
            checkList.SetActive(true);
            StartCoroutine(CloseAlter(checkList));
        }
        else
            checkList.SetActive(false);
    }

    public void onlyskipoff()
    {
        Default.SetActive(true);
        TimeUI.SetActive(false);
    }
    public void skipoff()
    {
        Debug.Log("꺼");
        Default.SetActive(false);
        TimeUI.SetActive(false);
    }

    public void skipon()
    {
        Debug.Log("켜");
        Default.SetActive(true);
        TimeUI.SetActive(true);
    }
    public void replayON()
    {
        TimeUI.SetActive(false);
        Replay.SetActive(true);
    }
//사이즈 조절(ProgrssUI)만 수행 예정
//챕터를 전달해서, setActive 예정
/*
    public void OnUpdatedProgress(int chapter)
    {
        dragScrollWidth = dragScroller.GetComponent<RectTransform>().rect.width; //원래위치?
        //chapter에 맞는 Dictionary 생성을 여기서 하고, ProgressUIController는 그걸 SetActive하는 용도로 사용하자.
        //1~4일차 =>필수 
        for (int i = 1; i <= 3; i++)
        {
            if (prograssUI.ContainsKey(i) == false)
            {
                GameObject icon = Instantiate(dragIcon, dragScroller.transform.GetChild(0));
                icon.name = chapterList.chapters[i].chapter;
                DragIcon curIconScript = icon.GetComponent<DragIcon>();
                curIconScript.Settings(chapterList.chapters[i].id, chapterList.chapters[i].title, chapterList.chapters[i].mainFilePath, "서브 타이틀 주세요");
                prograssUI[i] = icon;
                icon.GetComponent<Button>().onClick.AddListener(DayProgressUI.GetComponent<ProgressUIController>().onClickdragIcon);

                //ProgressBar의 길이 조절을 위함.
                dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width, dragScroller.GetComponent<RectTransform>().rect.height);
            }
        }

        //5일차~14일까지
        for (int i = 4; i <= chapter + 1; i++)
        {
            if (i >= 15) continue;
            if (prograssUI.ContainsKey(i) == false)
            {
                GameObject icon = Instantiate(dragIcon, dragScroller.transform.GetChild(0));
                icon.name = chapterList.chapters[i].chapter;
                DragIcon curIconScript = icon.GetComponent<DragIcon>();
                curIconScript.Settings(chapterList.chapters[i].id, chapterList.chapters[i].title, chapterList.chapters[i].mainFilePath, "서브 타이틀 주세요");
                prograssUI[i] = icon;
                icon.GetComponent<Button>().onClick.AddListener(DayProgressUI.GetComponent<ProgressUIController>().onClickdragIcon);

                //ProgressBar의 길이 조절을 위함.

                dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width + dragIcon.GetComponent<RectTransform>().rect.width, dragScroller.GetComponent<RectTransform>().rect.height);
            }
        }

        foreach (var progress in prograssUI)
        {
            if (progress.Key <= chapter)
            {
                progress.Value.GetComponent<DragIcon>().DestoryLock();
            }
        }
        float val = (chapter * dragIcon.GetComponent<RectTransform>().rect.width) / (dragScroller.GetComponent<ScrollRect>().content.rect.width - dragScrollWidth);
        //중앙 위치 계산
        dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition = (val / val) - 0.2f; //민감도 1로 만든다음, 0.2f를 해서 알림뜨는 문제를 해결
    }*/
}
