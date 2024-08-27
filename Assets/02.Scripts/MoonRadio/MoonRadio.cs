using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoonRadio : BaseObject
{
    [SerializeField]
    GameObject MoonRadioController;

    Animator blinkMoonRadioAnim;

    private void Start()
    {
        blinkMoonRadioAnim = GetComponent<Animator>();
        MoonRadioController = GameObject.Find("MoonRadio").transform.GetChild(0).gameObject;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }

        if (MoonRadioController.activeSelf == false)
        {
            //밤일 때 speed 1, 밤이 아니면 0
            blinkMoonRadioAnim.SetFloat("speed", 0f);
            MoonRadioController.SetActive(true);
        }
    }
}
