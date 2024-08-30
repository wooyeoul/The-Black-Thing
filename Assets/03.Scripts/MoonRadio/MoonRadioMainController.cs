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
    GameObject systemUI;

    private void OnEnable()
    {
        moonRadioOff.SetActive(false);
        moonRadioOn.SetActive(true);

        //systemUI∏¶ ≤®¡ÿ¥Ÿ.
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
        moonRadioMoon.SetActive(true);
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
