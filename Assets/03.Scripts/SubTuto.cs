using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTuto : MonoBehaviour
{
    [SerializeField] SubPanel subPanel;
    [SerializeField] TouchGuide touch;
    
    public string prefabPath = "TouchGuide"; 
    public Vector3 guide1 = new Vector3(-810, -145, 0);
    public Vector3 guide2 = new Vector3(-1095, -195, 0);

    // Update is called once per frame
    public void tutorial_2(GameObject selectedDot, int determine)
    {
        GameObject touchguide = Resources.Load<GameObject>(prefabPath);
        if (touchguide != null)
        {
            // 인스턴스화 및 활성화
            GameObject instance = Instantiate(touchguide, subPanel.gameObject.transform);
            instance.transform.localPosition = guide1;
            instance.SetActive(true);
            touch = instance.GetComponent<TouchGuide>();
        }
        else
        {
            Debug.LogError("프리팹을 찾을 수 없습니다!");
        }
        touch.tuto2(selectedDot, determine);
    }
    
}
