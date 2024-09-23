using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Assets.Script.DialClass;
using System.IO;

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
            Watch(manager, dot);
            return;
        }

        if (objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }
        objectManager.SettingChapter(manager.Chapter);

        watching = objectManager.GetWatchingObject(pattern[manager.Chapter]);
        
        if (watching!=null)
        {
            if (dot)
            {
                dot.gameObject.SetActive(false);
            }
            watching.OpenWatching(manager.Chapter);
        }
        //Stay일 때 뭉치 등장
    }

    public void Watch(GameManager manager, DotController dot = null)
    {
        ScriptList script = dot.GetSubScriptList(GamePatternState.Watching);

        Debug.Log(1);
        if (script != null)
        {
            Debug.Log(2);
            DotPatternState dotPattern;
            if (Enum.TryParse(script.AnimState, true, out dotPattern))
            {
                Debug.Log(3);
                dot.ChangeState(dotPattern, script.DotAnim, script.DotPosition);
            }
        }
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

    public override void Enter(GameManager manager, DotController dot = null)
    {
        ScriptList scriptList = dot.GetMainScriptList(0);
        dot.ChangeState(DotPatternState.Default, scriptList.DotAnim, scriptList.DotPosition);

        base.Enter(manager, dot);
    }

}

public class Thinking : GameState, ILoadingInterface
{
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {
        ScriptList script = dot.GetSubScriptList(GamePatternState.Watching);

        if (script != null)
        {
            DotPatternState dotPattern;
            if (Enum.TryParse(script.AnimState, true, out dotPattern))
            {
                dot.ChangeState(dotPattern, script.DotAnim, script.DotPosition);
            }
            //sub 끝나고, Think 수행
        }
        else
        {
            Think(manager,dot);
        }

    }

    public void Think(GameManager manager, DotController dot = null)
    {
        //Default값 랜덤으로 사용예정
        DotAnimState anim = (DotAnimState)UnityEngine.Random.Range(0, (int)DotAnimState.anim_eyesblink);
        manager.ObjectManager.PlayThinking();
        dot.ChangeState(DotPatternState.Default, anim.ToString());
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

    public override void Enter(GameManager manager, DotController dot = null)
    {
        ScriptList scriptList = dot.GetMainScriptList(1);
        dot.ChangeState(DotPatternState.Default, scriptList.DotAnim, scriptList.DotPosition);

        base.Enter(manager, dot);
    }
}

public class Writing : GameState, ILoadingInterface
{
    public override void Init()
    {
    }

    public override void Enter(GameManager manager, DotController dot = null)
    {
        ScriptList script = dot.GetSubScriptList(GamePatternState.Watching);

        if (script != null)
        {
            DotPatternState dotPattern;
            if (Enum.TryParse(script.AnimState, true, out dotPattern))
            {
                dot.ChangeState(dotPattern, script.DotAnim, script.DotPosition);
            }
            //sub가 끝나고, Write 함수 호출
        }
        else
        {
            Write(manager, dot);
        }
    }

    public void Write(GameManager manager, DotController dot = null)
    {
        manager.ObjectManager.PlayThinking();
        dot.ChangeState(DotPatternState.Phase, "anim_diary");
    }

    public override void Exit(GameManager manager)
    {

    }
}

public class Play : GameState, ILoadingInterface
{
    DotController dot =null;

    const int pos = 18;
    const string anim = "anim_trigger_play";
    public override void Init()
    {
    }
    public override void Enter(GameManager manager, DotController dot = null)
    {
        this.dot = dot;
        manager.ObjectManager.PlayThinking();
        manager.ScrollManager.StopCameraByPlayPhase(true);
        //카메라 고정
        dot.TriggerPlay(true);
        dot.ChangeState(DotPatternState.Tirgger, anim, pos);
    }
    public override void Exit(GameManager manager)
    {
        manager.ScrollManager.StopCameraByPlayPhase(false);
    }
}

public class Sleeping : GameState
{
    ISleepingInterface sleeping;
    DotController dot;
    public override void Init()
    {
    }


    public override void Enter(GameManager manager, DotController dot = null)
    {

        ScriptList script = dot.GetSubScriptList(GamePatternState.Watching);

        if (script != null)
        {
            DotPatternState dotPattern;
            if (Enum.TryParse(script.AnimState, true, out dotPattern))
            {
                dot.ChangeState(dotPattern, script.DotAnim, script.DotPosition);
            }
            //sub가 끝나면 Sleeping에 대한 동작을 수행하겠지...
        }
        else
        {
            Sleep(manager, dot);
        }
    }

    public void Sleep(GameManager manager, DotController dot)
    {
        this.dot = null;

        if (objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }

        if (sleeping == null)
        {
            sleeping = objectManager.GetSleepingObject();
        }

        manager.ObjectManager.PlayThinking();
        sleeping.OpenSleeping();
        this.dot = dot;
        dot.ChangeState(DotPatternState.Tirgger, "anim_sleep", 10);
        dot.Dust.SetActive(true);
    }
    public override void Exit(GameManager manager)
    {
        this.dot.Dust.SetActive(false);
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