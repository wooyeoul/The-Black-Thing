using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class BinocularExitController : MonoBehaviour
{

    GameObject camera;

    private void OnMouseDown()
    {
        //screen을 키고 parent를 destroy 
        Destroy(this.transform.parent.gameObject);
        camera = GameObject.FindWithTag("MainCamera");

        if (camera)
        {
            camera.GetComponent<ScrollManager>().StopCamera(false);
        }
    }


}
