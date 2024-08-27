using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MypageUIController : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject editPopup;
    [SerializeField]
    [Tooltip("Nickname Input")]
    GameObject closePopup;
    [SerializeField]
    [Tooltip("Nickname Alert")]
    GameObject alterPopup;
    [SerializeField]
    GameObject nameSetting;

    [SerializeField]
    List<Button> pushAlert;
    [SerializeField]
    List<Button> languageAlert;

    [SerializeField]
    Slider seSlider;
    [SerializeField]
    Slider musicSlider;


    string userID = "";
    string userName = "";
    float musicVolume = 0.5f;
    float seVolume = 0.5f;
    int pageIdx = 0;
    bool isEnableAlert = true;
    bool isKorean = true;

    PlayerController player;

    #region Nickname Section
    [SerializeField]
    TMP_Text nicknameTxt;
    [SerializeField]
    TMP_Text closeTxt;
    #endregion


    [SerializeField]
    List<GameObject> popupPage;

    [SerializeField]
    GameObject prevBut;

    [SerializeField]
    GameObject nextBut;
    [SerializeField]
    List<Color> colors;

    List<List<string>> popupPageName;

    TranslateManager translator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        popupPageName = new List<List<string>>();

        Init();
    }

    private void OnEnable()
    {
        //켜질때마다
        pageIdx = 0;

        for(int i=0;i<popupPage.Count;i++)
        {
            //모두 정리
            popupPage[i].SetActive(false);
        }
        prevBut.SetActive(false);
        popupPage[pageIdx].SetActive(true);

        if(popupPageName.IsUnityNull() == false)
        {
            int Idx = (int)player.GetLanguage();
            nextBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx + 1][Idx];
        }
    }

    void Init()
    {
        isEnableAlert = player.GetisPushNotificationEnabled();
        isKorean = player.GetLanguage() == LANGUAGE.KOREAN;
        userName = player.GetNickName();
        nameSetting.GetComponent<TMP_Text>().text = userName;
        musicVolume = player.GetMusicVolume();
        seVolume = player.GetAcousticVolume();
        seSlider.value = seVolume;
        musicSlider.value = musicVolume;

        //delegate 연결,실제 음악 델리게이트 연결
        seSlider.onValueChanged.AddListener( OnValueChangeSE );
        seSlider.onValueChanged.AddListener( player.SetSEVolume);
       // seSlider.onValueChanged.AddListener( MusicManager.Instance.AdjustSEVolume);

        musicSlider.onValueChanged.AddListener(OnValueChangedBGM);
        musicSlider.onValueChanged.AddListener(player.SetBGMVolume);
        // musicSlider.onValueChanged.AddListener(MusicManager.Instance.AdjustBGMVolume);  


        List<string> setting = new List<string>();
        setting.Add(DataManager.Instance.Settings.menuMyPage.settings[0]);
        setting.Add(DataManager.Instance.Settings.menuMyPage.settings[1]);

        List<string> community = new List<string>();
        community.Add(DataManager.Instance.Settings.menuMyPage.community[0]);
        community.Add(DataManager.Instance.Settings.menuMyPage.community[1]);

        List<string> credit = new List<string>();
        credit.Add(DataManager.Instance.Settings.menuMyPage.credit[0]);
        credit.Add(DataManager.Instance.Settings.menuMyPage.credit[1]);

        popupPageName.Add(setting);
        popupPageName.Add(community);
        popupPageName.Add(credit);

        int Idx = (int)player.GetLanguage();
        nextBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx + 1][Idx];
        EnablePushAlertColor();
        EnableLanguageColor();
    }
    public void OnValueChangedBGM(float value)
    {
        musicVolume=value;   
        //델리게이트 float 값 주어서 값이 변경될 때 음악도 변경
    }

    public void OnValueChangeSE(float value)
    {
        seVolume = value;
        //델리게이트 float 값을 주어서 값이 변경될 때 음악도 변경
    }

    public void StoreName()
    {
        userName = "";
        //InputField로부터 text를 가져온다.
        //alter팝업을 띄운다
        //이름을 바꾼다

        //1. EditPopup으로부터 실제 저장할 Nickname을 가져온다.
        //popup_namesetting - NameInput - TextBoxInput으로부터 text를 가져온다.
        userName = nicknameTxt.text;

        //4. 셋팅
        player.SetNickName(userName);

        //2. closePopup <nickname>을 찾아서 Nickname으로 수정한다.
        string edit = closeTxt.text.Replace("<nickname>", userName); //<nickname>을 제거 후 붙여넣기
        closeTxt.text = edit;

        //3. 1.5초 뒤 꺼진다. (후처리)
        editPopup.SetActive(false);
        closePopup.SetActive(true);
        nameSetting.GetComponent<TMP_Text>().text = userName;
        Invoke("CloseAlter", 1.5f);
    }

    /*Edit과 CancelEdit이 같이 사용*/
    public void ToggleEditName()
    {
        if (editPopup.activeSelf)
        {
            editPopup.gameObject.SetActive(false);
            return;
        }
        editPopup.gameObject.SetActive(true);
    }

    void CloseAlter()
    {
        closePopup.SetActive(false);
    }

    public void NextPage()
    {
        pageIdx++;
        
        //한계 설정, 페이지 개수에 따라서 페이지를 넘을 경우 조절
        if (pageIdx >= popupPage.Count)
        {
            pageIdx = popupPageName.Count - 1;
            return;
        }
        //다음 페이지가 없을 경우에는 버튼을 없애버림
        if (pageIdx+1>=popupPageName.Count)
        {
            nextBut.SetActive(false);
        }

        //현재 페이지를 보여주고, 이전 페이지를 없앰
        popupPage[pageIdx].SetActive(true);
        popupPage[pageIdx-1].SetActive(false);
        prevBut.SetActive(true);

        if (nextBut.activeSelf)
        {
            //활성화 되어있는 경우에, 현재 페이지 앞 뒤에 대한 이름 명으로 변경
            if (popupPage.Count > 0)
            {
                int Idx = (int)player.GetLanguage();
                nextBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx + 1][Idx];
            }
        }
        if (prevBut.activeSelf)
        {
            if (popupPage.Count > 0)
            {
                int Idx = (int)player.GetLanguage();
                prevBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx - 1][Idx];
            }
        }
    }

    public void PrePage()
    {
        pageIdx--;
        if (pageIdx < 0)
        {
            pageIdx = 0;
            return;
        }
        if (pageIdx-1 < 0)
        {
            prevBut.SetActive(false);
        }

        popupPage[pageIdx].SetActive(true);
        popupPage[pageIdx + 1].SetActive(false);
        nextBut.SetActive(true);

        if (prevBut.activeSelf)
        {
            if (popupPage.Count > 0)
            {
                int Idx = (int)player.GetLanguage();
                prevBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx - 1][Idx];
            }
        }
        if (nextBut.activeSelf)
        {
            if (popupPage.Count > 0)
            {
                int Idx = (int)player.GetLanguage();
                nextBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx + 1][Idx];
            }
        }
    }

    public void OnPushAlert()
    {
        isEnableAlert = true;
        EnablePushAlertColor();
        player.SetisPushNotificationEnabled(isEnableAlert);
    }

    public void OffPushAlert()
    {
        alterPopup.SetActive(true);
    }

    public void Off()
    {
        isEnableAlert = false;
        EnablePushAlertColor();
        player.SetisPushNotificationEnabled(isEnableAlert);
        alterPopup.SetActive(false);
    }

    public void On()
    {
        isEnableAlert = true; 
        EnablePushAlertColor();
        player.SetisPushNotificationEnabled(isEnableAlert);
        alterPopup.SetActive(false);
    }

    public void SetKorean()
    {
        isKorean = true;
        player.SetLanguage(LANGUAGE.KOREAN);
        EnableLanguageColor();
    }

    public void SetEnglish()
    {
        isKorean = false;
        player.SetLanguage(LANGUAGE.ENGLISH);
        EnableLanguageColor();
    }
    public void Exit()
    {
        this.gameObject.SetActive(false);
    }

    void EnablePushAlertColor()
    {
        int Idx = Convert.ToInt32(isEnableAlert) % 2;
        pushAlert[Idx].GetComponent<Image>().color = colors[0]; //0번이 활성화
        pushAlert[(Idx + 1) % 2].GetComponent<Image>().color = colors[1]; //1번 비활성화
    }

    void EnableLanguageColor()
    {
        int Idx = Convert.ToInt32(isKorean) % 2;
        languageAlert[Idx].GetComponent<TMP_Text>().color = colors[0]; //0번이 활성화
        languageAlert[(Idx + 1) % 2].GetComponent<TMP_Text>().color = colors[1]; //1번 비활성화
        
        if (prevBut.activeSelf)
        {
            if (popupPage.Count > 0)
            {
                int LangIdx = (int)player.GetLanguage();
                prevBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx - 1][LangIdx];
            }
        }
        if (nextBut.activeSelf)
        {
            if (popupPage.Count > 0)
            {
                int LangIdx = (int)player.GetLanguage();
                nextBut.GetComponent<TMP_Text>().text = popupPageName[pageIdx + 1][LangIdx];
            }
        }
    }
}
