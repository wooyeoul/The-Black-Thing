using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinocularController : MonoBehaviour , IWatchingInterface
{
    [SerializeField]
    GameObject alert;
   
    [SerializeField]
    List<GameObject> watching;
    
    int phaseIdx = 0; //-1로 바꿔줘야함.

    GameObject watchingBackground;
    GameObject screenBackground;
    GameObject phase;

    private void Start()
    {
        watchingBackground = GameObject.Find("Phase").gameObject;
        screenBackground = GameObject.FindWithTag("ObjectManager").gameObject.transform.GetChild(0).gameObject;
    }
    public void OpenWatching(int Chapter)
    {
        alert.SetActive(true);
        phaseIdx += 1;
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
            //클로즈~
            phase = Instantiate(watching[phaseIdx], watchingBackground.transform);
        }
    }
}
