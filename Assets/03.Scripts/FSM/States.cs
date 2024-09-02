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
        objectManager.SettingChapter(manager.Chapter);

        watching = objectManager.GetWatchingObject(pattern[manager.Chapter]);
        
        if (watching!=null)
        {
            watching.OpenWatching(manager.Chapter);
        }
        else
        {
            dot.ChangeState(DotPatternState.Defualt, "anim_mud");
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
public class MainA : MainDialogue
{
    
    //멤버 변수 대사 
    public override void Init()
    {
      
    }

    public override string GetData(int idx)
    {

        //데이터에 대한 애니메이션으로 변경한다.
        dot.ChangeState(DotPatternState.Main); //상태값, 애니메이션 키, 위치 값

        return null; //data[idx].Kor
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

public class MainB : MainDialogue
{
    //데이터를 가지고 있는다.

    public override void Init()
    {
    }

    public override string GetData(int idx)
    {

        //데이터에 대한 애니메이션으로 변경한다.
        dot.ChangeState(DotPatternState.Main); //상태값, 애니메이션 키, 위치 값

        return null; //data[idx].Kor
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
