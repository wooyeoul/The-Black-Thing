using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoonRadio : BaseObject
{
    [SerializeField]
    GameObject MoonRadioController;

    Animator blinkMoonRadioAnim;

    private void Start()
    {
        blinkMoonRadioAnim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if(MoonRadioController.activeSelf == false)
        {
            //π„¿œ ∂ß speed 1, π„¿Ã æ∆¥œ∏È 0
            blinkMoonRadioAnim.SetFloat("speed", 0f);
            MoonRadioController.SetActive(true);
        }
    }
}
