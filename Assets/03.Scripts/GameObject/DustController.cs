using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    DustSpawner spawner;

    private void Start()
    {
        spawner = transform.parent.gameObject.GetComponent<DustSpawner>();
    }

    private void OnMouseDown()
    {
        if(spawner != null )
        {
            spawner.Deactive(this.gameObject);
        }
    }
}
