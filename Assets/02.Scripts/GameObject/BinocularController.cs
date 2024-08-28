using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinocularController : BaseObject , IWatchingInterface
{
    [SerializeField]
    EWatching type;

    [SerializeField]
    GameObject alert;
   
    [SerializeField]
    List<GameObject> watching;
    
    int chapter; //-1로 바꿔줘야함.

    GameObject watchingBackground;
    GameObject screenBackground;
    GameObject phase;

    Dictionary<int,int> Idx = new Dictionary<int,int>();
    public bool IsCurrentPattern(EWatching curPattern)
    {
        return curPattern == type;
    }

    private void Start()
    {
        watchingBackground = GameObject.Find("Phase").gameObject;
        screenBackground = GameObject.FindWithTag("ObjectManager").gameObject.transform.GetChild(0).gameObject;
        Init();
    }

    void Init()
    {
        if (Idx.Count <= 0)
        {
            int i = 0, j = 0;
            foreach (string strVal in DataManager.Instance.Settings.watching.pattern)
            {
                EWatching enumVal;
                if (Enum.TryParse(strVal, true, out enumVal))
                {
                    if (enumVal == EWatching.Binocular)
                    {
                        Idx.Add(i, j++);
                    }
                }
                i++;
            }
        }
    }
    public void OpenWatching(int Chapter)
    {
        alert.SetActive(true);
        chapter = Chapter;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }

        if (alert.activeSelf)
        {
            screenBackground.SetActive(false);

            Debug.Log(chapter);
            //클로즈~
            phase = Instantiate(watching[Idx[chapter]], watchingBackground.transform);
        }
    }

    public void CloseWatching()
    {
        alert.SetActive(false);
    }
}
