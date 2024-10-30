using Assets.Script.DialClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tutorial
{
    public class Sub : GameState
    {
        const int pos = 3;
        const string anim = "anim_default";
        public Sub()
        {

        }

        //멤버 변수 대사 
        public override void Init()
        {
             
        }

        public override void Enter(GameManager manager, DotController dot = null)
        {
            dot.ChangeState(DotPatternState.Default, anim, pos);
            GameObject door = GameObject.Find("fix_door");
            Debug.Log(door);
            door.transform.GetChild(1).GetComponent<DoorController>().close();
            manager.ScrollManager.MoveCamera(new Vector3((float)5.70, 0, -10), 2);
            manager.ScrollManager.stopscroll();
            InvokeHelper.Instance.InvokeAfterDelay(substart, 2f);
        }

        public override void Exit(GameManager manager)
        {

        }
        public void substart()
        {
            Debug.Log("아 모르겠다");
        }
    }
    public class Main: MainDialogue
    {

        //멤버 변수 대사 
        public override void Init()
        {

        }

        public override void Enter(GameManager manager, DotController dot = null)
        {
            Debug.Log("튜토리얼 메인 시작");
        }

        public override void Exit(GameManager manager)
        {

        }
    }
};