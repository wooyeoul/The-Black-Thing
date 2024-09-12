using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    
    public void OnValueChanged(float value)
    {
        if(value == 1f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
