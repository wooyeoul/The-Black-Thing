using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTuto : MonoBehaviour
{
    [SerializeField] SubPanel subPanel;
    // Start is called before the first frame update
    void Start()
    { 
        subPanel = GameObject.Find("SubPanel").GetComponent<SubPanel>();
    }

    // Update is called once per frame
    public void tutorial_2(GameObject selectedDot, int determine)
    { 
        GameObject door = GameObject.Find("fix_door");
        Debug.Log(door);
        door.transform.GetChild(1).GetComponent<DoorController>().open();
        if (determine == 0)
        {
            subPanel.dotballoon(selectedDot);
        }
        else
        {
            subPanel.playerballoon(selectedDot);
        }
    }
}
