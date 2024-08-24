using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Android;
using Assets.Script.TimeEnum;
//여기서 게임 상태 정의 
//하나의 큰 유한 상태 머신 만들 예정
public enum GamePatternState
{
    Watching = 0, //Watching 단계
    MainA, // Main 다이얼로그 A 단계
    Thinking, // Thinking 단계
    MainB, // Main 다이얼로그 B 단계
    Writing, // Writing 단계
    Play, //Play 단계
    Sleeping //Sleeping 단계
};

public class GameManager : MonoBehaviour
{
    private GameState activeState;
    private ObjectManager objectManager;
    private Dictionary<GamePatternState, GameState> states;
    PlayerController pc;

    SITime time;

    GameManager()
    {
        states = new Dictionary<GamePatternState, GameState>();

        states[GamePatternState.Watching] = new Watching();
        states[GamePatternState.MainA] = new MainA();
        states[GamePatternState.Thinking] = new Thinking();
        states[GamePatternState.MainB] = new MainB();
        states[GamePatternState.Writing] = new Writing();
        states[GamePatternState.Play] = new Play();
        states[GamePatternState.Sleeping] = new Sleeping();
    }
    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    private void Start()
    {
        //Player 단계를 가져온다.
        pc = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerController>();
        objectManager = GameObject.FindWithTag("ObjectManager").gameObject.GetComponent<ObjectManager>();
       
        ChangeGameState((GamePatternState)pc.GetAlreadyEndedPhase());
        InitBackground();
    }

    public void ChangeGameState(GamePatternState patternState)
    {
        if (states == null) return;

        if(states.ContainsKey(patternState) == false)
        {
            Debug.Log("없는 패턴 입니다.");
            return; 
        }
        activeState = states[patternState];
        Debug.Log($"현재 Phase 단계 :{patternState}");
        activeState.Enter(this);
    }

    private void InitBackground()
    {
        //배경을 업로드한다.
        Int32 hh = Int32.Parse(DateTime.Now.ToString(("HH"))); //현재 시간을 가져온다
       

        if (hh >= (int)STime.T_DAWN && hh < (int)STime.T_MORNING) //현재시간 >= 3 && 현재시간 <7
        {
            time = SITime.Dawn;
        } //현재시간 >= 7&& 현재시간 <4
        else if (hh >= (int)STime.T_MORNING && hh < (int)STime.T_EVENING)
        {
            time = SITime.Morning;
        }
        else if (hh >= (int)STime.T_EVENING && hh < (int)STime.T_NIGHT)
        {
            time = SITime.Evening;
        }
        else
        {
            time = SITime.Night;
        }

        //해당 백그라운드로 변경한다.
        //GameObject background = Resources.Load<GameObject>("Background/"+time);
        //Instantiate<GameObject>(background, objectManager.transform);
        //리소스 폴더에 있는 모든 오브젝트를 가져와서 풀을 모두 채운다.
        Debug.Log(time.ToString());
        objectManager.loadObject("Night");
    }
}
