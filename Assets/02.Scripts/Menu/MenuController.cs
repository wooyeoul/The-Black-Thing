using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    GameObject MenuBut;
    [SerializeField]
    GameObject ExitBut;


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
    GameObject checkList;

    [SerializeField]
    GameObject Screens;

    Animator MenuButAnim;

    bool isOpening = false;

    private void Start()
    {
        MenuButAnim = GetComponent<Animator>();
    }

    public void onMenu()
    {
        MenuBut.GetComponent<Button>().enabled = false;
        isOpening = !isOpening;
        MenuButAnim.SetFloat("speed", 1f);
        if (isOpening)
        {
            Screens.SetActive(false);
            TimeUI.SetActive(false);
            checkList.SetActive(false);
            MenuDefault.SetActive(true);
            MenuButAnim.SetBool("isDowning", true);
            ExitBut.GetComponent<Button>().enabled = true;
        }
        else
        {
            Screens.SetActive(true);
            MenuDefault.transform.GetChild(1).gameObject.SetActive(false);
            MenuButAnim.SetBool("isDowning", false);
        }
    }

    public void offMenu()
    {
        if(ExitBut != null)
        {
            ExitBut.GetComponent<Button>().enabled = false;
            onMenu();
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
        MenuBut.GetComponent<Button>().enabled = true;
        if (isOpening)
        {
            MenuDefault.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            TimeUI.SetActive(true);
            checkList.SetActive(true);
            MenuDefault.SetActive(false);
        }
    }
    public void onDayProgressUI()
    {
        //DayProgressUI on,.,
        DayProgressUI.SetActive(true);
    }

    public void onClickHelper()
    {
        Helper.SetActive(true);
    }

    public void onClickMypage()
    {
        MyPageUI.SetActive(true);
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
        Debug.Log("��");
        Default.SetActive(false);
        TimeUI.SetActive(false);
    }

    public void skipon()
    {
        Debug.Log("��");
        Default.SetActive(true);
        TimeUI.SetActive(true);
    }
    public void replayON()
    {
        TimeUI.SetActive(false);
        //Replay.SetActive(true);
    }
//������ ����(ProgrssUI)�� ���� ����
//é�͸� �����ؼ�, setActive ����
/*
    public void OnUpdatedProgress(int chapter)
    {
        dragScrollWidth = dragScroller.GetComponent<RectTransform>().rect.width; //������ġ?
        //chapter�� �´� Dictionary ������ ���⼭ �ϰ�, ProgressUIController�� �װ� SetActive�ϴ� �뵵�� �������.
        //1~4���� =>�ʼ� 
        for (int i = 1; i <= 3; i++)
        {
            if (prograssUI.ContainsKey(i) == false)
            {
                GameObject icon = Instantiate(dragIcon, dragScroller.transform.GetChild(0));
                icon.name = chapterList.chapters[i].chapter;
                DragIcon curIconScript = icon.GetComponent<DragIcon>();
                curIconScript.Settings(chapterList.chapters[i].id, chapterList.chapters[i].title, chapterList.chapters[i].mainFilePath, "���� Ÿ��Ʋ �ּ���");
                prograssUI[i] = icon;
                icon.GetComponent<Button>().onClick.AddListener(DayProgressUI.GetComponent<ProgressUIController>().onClickdragIcon);

                //ProgressBar�� ���� ������ ����.
                dragScroller.GetComponent<RectTransform>().sizeDelta = new Vector2(dragScroller.GetComponent<RectTransform>().rect.width, dragScroller.GetComponent<RectTransform>().rect.height);
            }
        }

        //5����~14�ϱ���
        for (int i = 4; i <= chapter + 1; i++)
        {
            if (i >= 15) continue;
            if (prograssUI.ContainsKey(i) == false)
            {
                GameObject icon = Instantiate(dragIcon, dragScroller.transform.GetChild(0));
                icon.name = chapterList.chapters[i].chapter;
                DragIcon curIconScript = icon.GetComponent<DragIcon>();
                curIconScript.Settings(chapterList.chapters[i].id, chapterList.chapters[i].title, chapterList.chapters[i].mainFilePath, "���� Ÿ��Ʋ �ּ���");
                prograssUI[i] = icon;
                icon.GetComponent<Button>().onClick.AddListener(DayProgressUI.GetComponent<ProgressUIController>().onClickdragIcon);

                //ProgressBar�� ���� ������ ����.

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
        //�߾� ��ġ ���
        dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition = (val / val) - 0.2f; //�ΰ��� 1�� �������, 0.2f�� �ؼ� �˸��ߴ� ������ �ذ�
    }*/
}
