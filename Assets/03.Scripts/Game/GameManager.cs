using Assets.Script.TimeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
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
    Sleeping, //Sleeping 단계
    NextChapter, //Sleeping 단계가 끝나면 기다리든가, 아님 Skip을 눌러서 Watching으로 넘어갈 수 있음. 
    End,//이 단계로 넘어가면 오류, 다음단계 0으로 이동해야함.
    /*Trigger 용도*/
    SubA,
    SubB
};

public class GameManager : MonoBehaviour
{
    private GameState activeState;
    private ObjectManager objectManager;
    private ScrollManager scrollManager;
    private Dictionary<GamePatternState, GameState> states;
    private PlayerController pc;
    private GamePatternState currentPattern;
    private SITime time;
    [SerializeField] GameObject mainpanel;

    [SerializeField]
    GameObject skipPhase;

    [SerializeField]
    private DotController dot;
    public GamePatternState Pattern
    {
        get { return currentPattern; }
    }
    public int Chapter
    {
        get { return pc.GetChapter(); }
    }

    public ObjectManager ObjectManager
    {
        get { return objectManager; }
    }

    public ScrollManager ScrollManager
    {
        get { return scrollManager; }
    }

    public GameState CurrentState
    {
        get { return activeState; }
    }

    public string Time
    {
        get { return time.ToString(); }
    }

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
        states[GamePatternState.NextChapter] = new NextChapter();
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
        pc.nextPhaseDelegate += ChangeGameState;
        objectManager = GameObject.FindWithTag("ObjectManager").gameObject.GetComponent<ObjectManager>();
        scrollManager = GameObject.FindWithTag("MainCamera").gameObject.GetComponent<ScrollManager>();
        InitGame();
    }

    public void GoSleep()
    {
        dot.GoSleep();
    }
        
    public void NextPhase()
    {
        pc.NextPhase();
    }
    public void ChangeGameState(GamePatternState patternState)
    {
        if (states == null) return;

        if(states.ContainsKey(patternState) == false)
        {
            Debug.Log("없는 패턴 입니다.");
            return; 
        }

        StartCoroutine(ChangeState(patternState));
    }

    public void StartMain()
    {
        MainDialogue mainState= (MainDialogue)activeState;
        string fileName = "main_ch" + Chapter;
        if (mainState != null)
        {
            mainState.StartMain(this, fileName);
            mainpanel.SetActive(true);
        }
    }
    //코루틴으로 한다.
    IEnumerator ChangeState(GamePatternState patternState)
    {
        if (activeState != null)
        {
            activeState.Exit(this); //미리 정리한다.
        }
        currentPattern=patternState;
        activeState = states[patternState];
        activeState.Enter(this, dot);

        //C#에서 명시적 형변환은 강제, as 할지말지를 결정.. 즉, 실패 유무를 알고 싶다면, as를 사용한다.
        ILoadingInterface loadingInterface = activeState as ILoadingInterface;
     
        if (loadingInterface != null)
        {
            skipPhase.SetActive(true);
            
            yield return new WaitForSeconds(5.0f);

            skipPhase.SetActive(false);
        }

        yield return null;
    }

    private void InitGame()
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
        GameObject background = Resources.Load<GameObject>("Background/"+time.ToString());
        Instantiate<GameObject>(background, objectManager.transform);
        //리소스 폴더에 있는 모든 오브젝트를 가져와서 풀을 모두 채운다.
        objectManager.LoadObject(time.ToString(), pc.GetChapter());
        objectManager.SettingChapter(pc.GetChapter());
        foreach (var state in states)
        {
            state.Value.Init();
        }
        string path = Path.Combine(Application.dataPath + "/AssetBundles/" + time.ToString());

        objectManager.InitMainBackground(path);

        GamePatternState patternState = (GamePatternState)pc.GetAlreadyEndedPhase();
        currentPattern = patternState;
        activeState = states[patternState];
        activeState.Enter(this,dot);
    }


}
