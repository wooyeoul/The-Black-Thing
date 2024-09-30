using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRadioMainController : MonoBehaviour
{

    #region Main Moon Radio
    [SerializeField]
    GameObject moonRadioOff;
    [SerializeField]
    GameObject moonRadioOn;
    #endregion

    [SerializeField]
    GameObject moonRadioMoon;
    [SerializeField]
    GameObject moonRadioEarth;

    [SerializeField]
    GameObject systemUI; //이거 델리게이트로 대체할 예정
    
    IPlayerInterface player;

    [SerializeField]
    GameObject popupUI;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<IPlayerInterface>();
    }

    private void OnEnable()
    {
        moonRadioOff.SetActive(false);
        moonRadioOn.SetActive(true);

        //systemUI를 꺼준다.
        systemUI = GameObject.Find("SystemUI");
        if(systemUI)
        {
            systemUI.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if(systemUI)
        {
            systemUI.SetActive(true);
        }
    }

    public void RadioOff()
    {
        if (moonRadioOff.activeSelf == false)
        {
            moonRadioOn.SetActive(false);
            moonRadioOff.SetActive(true);
        }
    }

    public void GoMoon()
    {
        if(player.GetMoonRadioIdx() <= 2)
        {
            moonRadioMoon.SetActive(true);
        }
        else
        {
            popupUI.SetActive(true);
        }
    }

    public void ClosePopup()
    {
        popupUI.SetActive(false);
    }

    public void GoEarth()
    {
        moonRadioEarth.SetActive(true);
    }

    public void GoMain()
    {
        this.gameObject.SetActive(false);
    }

    public void BackMoonRadio()
    {
        moonRadioOn.SetActive(true);
        moonRadioOff.SetActive(false);
    }
}
