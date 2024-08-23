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
    int chapter;

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

    bool IsCurrentChapter(int chpater)
    {
        return this.chapter == chapter;
    }
    
}
