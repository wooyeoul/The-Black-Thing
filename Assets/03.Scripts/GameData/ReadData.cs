using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        //데이터 로드 
        var loadedJson = Resources.Load<TextAsset>("Json/Chapters");

        if (loadedJson)
        {
            DataManager.Instance.ChapterList = JsonUtility.FromJson<Chapters>(loadedJson.ToString());
        }

        loadedJson = Resources.Load<TextAsset>("Json/Setting");

        if (loadedJson)
        {
            DataManager.Instance.Settings = JsonUtility.FromJson<SettingInfo>(loadedJson.ToString());
        }

        loadedJson = Resources.Load<TextAsset>("Json/PoemsData");

        if (loadedJson)
        {
            DataManager.Instance.PoemData = JsonUtility.FromJson<Poems>(loadedJson.ToString());
        }
    }

}
