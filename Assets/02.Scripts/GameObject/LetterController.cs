using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : BaseObject, IWatchingInterface
{
    [SerializeField]
    EWatching type;

    [SerializeField]
    GameObject alert;

    [SerializeField]
    GameObject noteUI;

    [SerializeField]
    GameObject note;

    int chapter = 0;

    public bool IsCurrentPattern(EWatching curPattern)
    {
        return curPattern == type;
    }

    public void OpenWatching(int Chapter)
    {
        alert.SetActive(true);
        chapter = Chapter;
    }

    private void OnMouseDown()
    {
        if(alert.activeSelf)
        {
            if(noteUI != null)
            {
                noteUI.SetActive(true);
            }
            else
            {
                noteUI = Instantiate(note, GameObject.Find("Canvas").transform);
            }
        }
    }

    public void CloseWatching()
    {
        alert.SetActive(false);
    }
}
