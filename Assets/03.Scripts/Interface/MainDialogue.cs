using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MainDialogue : GameState
{
    //대사

    protected GameObject background = null;
    protected DotController dot = null;

    public override void Enter(GameManager manager, DotController dot = null)
    {

        //실제로는 뭉치가 먼저 뜬다.
        //dot State 변경 -> 클릭 시 아래 두개 고정 및 SetMain 설정.
        this.dot = dot;
        dot.TriggerMain(true);
        dot.ChangeState(DotPatternState.Defualt, "anim_default");
    }

    //준현아 여기에 함수 만들어놓을게 파라미터랑 리턴값 등 너가 필요한대로 바꿔
    public abstract string GetData(int index);

    public void StartMain(GameManager manager)
    {
        //대사를 로드했음 좋겠음.
        //배경화면을 로드한다.
        //카메라를 0,0,10에서 정지시킨다.움직이지 못하게한다.

        manager.ScrollManager.StopCamera(true);
        background = manager.ObjectManager.SetMain("main_door_open");
        //파라미터로 배경값을 전달하면 된다.
        //Day 7을 제외하곤 모두 배경값을 Enter에서 수정하면 되고, 데이 7일때만 변경해준다.
    }
    public override void Exit(GameManager manager)
    {
        dot.TriggerMain(false);
        manager.ScrollManager.StopCamera(false);
        if (background)
        {
            background.SetActive(false);
        }
    }
}
