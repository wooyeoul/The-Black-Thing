using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEventComponent : MonoBehaviour
{
    [SerializeField]
    MoonRadioEarthController earthController;

    void ExitEvents(){
        earthController.Send2MoonButEventExit();
        this.GetComponent<Animator>().SetBool("isGoing",false);
        this.transform.parent.GetComponent<Animator>().SetBool("isGoing",true);
        //textbox isSetActive(true)s
    }
}
