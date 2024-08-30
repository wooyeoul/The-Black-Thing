using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EWatching
{
    Binocular,
    Letter,
    StayAtHome,
    None
}
public class Watching : GameState
{
    
    //뭉치의 외출 여부를 알아야한다.
    List<EWatching> pattern = new List<EWatching>();
    IWatchingInterface watching = null;

    public override void Init()
    {
        if (pattern.Count <= 0)
        {
            if (DataManager.Instance.Settings == null) return;

            foreach (string strVal in DataManager.Instance.Settings.watching.pattern)
            {
                EWatching enumVal;
                if (Enum.TryParse(strVal, true, out enumVal))
                {
                    Debug.Log(enumVal.ToString());
                    pattern.Add(enumVal);
                }
            }
        }
    }
    public override void Enter(GameManager manager, DotController dot = null)
    {
        if (pattern[manager.Chapter] == EWatching.None)
        {
            return;
        }

        if(objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }

        watching = objectManager.GetWatchingObject(pattern[manager.Chapter]);

        if(watching!=null)
        {
            watching.OpenWatching(manager.Chapter);
        }
        else
        {
            //Dot뭉치 watching 표현
            dot.ChangeState(DotPatternState.Phase, "anim_watching",1.5f);
        }
        //Stay일 때 뭉치 등장
    }

    public override void Exit(GameManager manager)
    {
        if(watching != null)
        {
            watching.CloseWatching();
        }
    }
}

//MainA/MainB 인터페이스 사용해서 함수 하나 연결할 수 있도록 하면 좋겠슴.
public class MainA : GameState
{
    //멤버 변수 대사 
    public override void Init()
    {
      
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {
        //대사를 로드했음 좋겠음.
    }

    //현재 idx, 넘겨줬을때 대사를 주는 그러한 함수 

    public override void Exit(GameManager manager)
    {

    }
}

public class Thinking : GameState
{
    public override void Init()
    {
    }
    public override void Enter(GameManager manager, DotController dot = null)
    {

    }

    public override void Exit(GameManager manager)
    {

    }
}

public class MainB : GameState
{
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {
    }

    public override void Exit(GameManager manager)
    {

    }
}

public class Writing : GameState
{
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {

    }

    public override void Exit(GameManager manager)
    {

    }
}

public class Play : GameState
{
    public override void Init()
    {
    }
    public override void Enter(GameManager manager, DotController dot = null)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class Sleeping : GameState
{
    ISleepingInterface sleeping;
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {
        if (objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }

        if(sleeping == null)
        {
            sleeping = objectManager.GetSleepingObject();
        }

        sleeping.OpenSleeping();
        
    }

    public override void Exit(GameManager manager)
    {

    }
}
