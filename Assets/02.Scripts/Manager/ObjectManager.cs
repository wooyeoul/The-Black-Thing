using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //모든 상태가 오브젝트들을 공유한다.
    private ObjectPool pool;

    public ObjectManager()
    {
        pool = new ObjectPool();
    }

    public void loadObject(string path)
    {
        GameObject[] obj = Resources.LoadAll<GameObject>(path);

        foreach (GameObject obj2 in obj)
        {
            //Instantiate를 통해서 InsertMemory내 삽입

            GameObject newObj = Instantiate(obj2, this.transform.GetChild(0));
            pool.InsertMemory(newObj);

        }
    }

}
