using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/*오브젝트 풀에 사용할 BaseObject*/
public class BaseObject : MonoBehaviour
{
    static int nextValidID = 0;
    int id;

    [SerializeField]
    List<bool> chapter;

    public int ID
    {
        set
        {
            id = nextValidID++;
        }
        get
        {
            return id;
        }
    }

    public bool IsCurrentChapter(int chapter)
    {
        //chapter는 1씩 큼.
        return this.chapter[chapter-1];
    }
    
}
