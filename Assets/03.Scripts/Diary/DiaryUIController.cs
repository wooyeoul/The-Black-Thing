using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiaryUIController : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    GameObject guideUI;
    [SerializeField]
    GameObject openDiaryUI;
    [SerializeField]
    GameObject closeDiaryUI;

    public void SetActiveGuide()
    {
        guideUI.SetActive(true);
        SetActive();
    }

    public void SetActive()
    {
        closeDiaryUI.SetActive(true);
    }


    public void OnClickGuide()
    {
        if(guideUI.activeSelf)
        {
            guideUI.SetActive(false);
        }
    }
    public void OnClickOpen()
    {
        if (closeDiaryUI.activeSelf)
        {
            closeDiaryUI.SetActive(false);
        }
        openDiaryUI.SetActive(true);
    }

    public void Exit()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject gameObject = button.transform.parent.gameObject;
        if(gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
