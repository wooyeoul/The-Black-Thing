using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/*½Ì±ÛÅæÀ¸·Î ±¸Çö ¿¹Á¤*/
public class DataManager : MonoBehaviour
{
    static DataManager instance;
    #region Json¿¡ ´ëÇÑ º¯¼ö 
    Chapters chapterList;

    public Chapters ChapterList
    {
        get
        {
            return chapterList;
        }
        set
        {
            chapterList = value;
        }
    }

    SettingInfo settings;

    public SettingInfo Settings
    {
        get
        {
            return settings;
        }
        set
        {
            settings = value;
        }
    }

    PoemData poemData;

    public PoemData PoemData
    {
        get
        {
            return poemData;
        }
        set
        {
            poemData = value;
        }
    }

    #endregion
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            //¾ÀÀÌ ²¨Áö³ª?
        }
    }

    public static DataManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance; 
        }
    }
}
