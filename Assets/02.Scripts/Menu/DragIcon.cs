using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DragIcon : MonoBehaviour
{

    [SerializeField]
    TMP_Text titleText;
    [SerializeField]
    TMP_Text subText;
    [SerializeField]
    Image image;
    [SerializeField]
    GameObject lockObject;
    int chapter;
    string title;
    Sprite sprite;
    string subTitle;

    public void Settings(int chapter,string title, string source,string subTitle)
    {
        this.chapter=chapter;
        this.title=title;
        this.sprite = Resources.Load<Sprite>(source);
        this.subTitle=subTitle;

        titleText.text=this.title;
        subText.text=this.subTitle;
        image.sprite=this.sprite;
    }

    public bool isLocking()
    {
        return lockObject!=null;
    }
    public void DestoryLock()
    {
        if(lockObject!=null)
            Destroy(lockObject);
    }
}
