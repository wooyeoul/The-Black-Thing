using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            if(dot)
            {
                dot.gameObject.SetActive(false);
            }
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
       
        //데이터에 대한 애니메이션으로 변경한다., fixedPos 은 건드리지말길!!! 위치 값인데 항상 고정
        dot.ChangeState(DotPatternState.Main, "body_default1", fixedPos, "face_null");
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
        //Default값 랜덤으로 사용예정
        DotAnimState anim = (DotAnimState)UnityEngine.Random.Range(0, (int)DotAnimState.anim_eyesblink);
        manager.ObjectManager.PlayThinking();

        Debug.Log(anim.ToString());
        dot.ChangeState(DotPatternState.Defualt, anim.ToString());
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
        
        //데이터에 대한 애니메이션으로 변경한다.,fixedPos 은 건드리지말길!!! 위치 값인데 항상 고정
        dot.ChangeState(DotPatternState.Main, "body_default1", fixedPos, "face_null");
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
        manager.ObjectManager.PlayThinking();
        dot.ChangeState(DotPatternState.Phase, "anim_diary");
    }

    public override void Exit(GameManager manager)
    {

    }
}

public class Play : GameState
{
    DotController dot =null;

    const int pos = 18;
    const string anim = "anim_trigger_play";
    public override void Init()
    {
    }
    public override void Enter(GameManager manager, DotController dot = null)
    {
        manager.ObjectManager.PlayThinking();
        this.dot = dot;
        dot.TriggerPlay(true);
        dot.ChangeState(DotPatternState.Tirgger, anim, pos);
    }
    public override void Exit(GameManager manager)
    {
        if(dot)
        {
            dot.TriggerPlay(false);
        }
        //자러가는 애니메이션 여기에 추가한다.
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

        dot.ChangeState(DotPatternState.Tirgger, "anim_sleep", 10);

        manager.ObjectManager.PlayThinking();
        sleeping.OpenSleeping();
        
    }

    public override void Exit(GameManager manager)
    {

    }
}

public class NextChapter : GameState
{
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {

        //다음 챕터로 넘어가는 달나라를 띄운다.
        if (objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }

        manager.ObjectManager.SkipSleeping(true);
    }

    public override void Exit(GameManager manager)
    {
        manager.ObjectManager.SkipSleeping(false);
    }
}