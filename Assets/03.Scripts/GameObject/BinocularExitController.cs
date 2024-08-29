using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class BinocularExitController : MonoBehaviour
{
    GameObject screen;
    GameObject camera;

    private void OnEnable()
    {
        screen = GameObject.FindWithTag("ObjectManager").gameObject.transform.GetChild(0).gameObject;
    }

    private void OnMouseDown()
    {
        //screen을 키고 parent를 destroy 
        screen.SetActive(true);
        Destroy(this.transform.parent.gameObject);
        camera = GameObject.FindWithTag("MainCamera");

        if (camera)
        {
            camera.GetComponent<ScrollManager>().StopCamera(false);
        }
    }


}
