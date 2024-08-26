using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectManager : MonoBehaviour
{
    //모든 상태가 오브젝트들을 공유한다.
    private ObjectPool pool;

    public ObjectManager()
    {
        pool = new ObjectPool();
    }

    public void LoadObject(string path, int chapter)
    {
        GameObject[] obj = Resources.LoadAll<GameObject>(path);
        foreach (GameObject obj2 in obj)
        {
            //Instantiate를 통해서 InsertMemory내 삽입
            GameObject newObj = Instantiate(obj2, this.transform.GetChild(0));
            pool.InsertMemory(newObj);
        }
    }

    //한 챕터를 넘겼을 때 호출되는 함수, 즉 Phase Watching일 때 호출한다. 
    public void SettingChapter(int chapter)
    {

        //모든 Obj를 가져와서 검사해야한다.
        List<GameObject> values = pool.GetValues();

        foreach (GameObject value in values)
        {
            if (value.GetComponent<BaseObject>().IsCurrentChapter(chapter))
            {
                value.SetActive(true);
            }
            else
            {
                value.SetActive(false);
            }
        }

    }
}
