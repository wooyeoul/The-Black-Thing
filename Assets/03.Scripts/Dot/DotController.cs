using Assets.Script.TimeEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class DotController : MonoBehaviour
{
    private DotState currentState; //현재 상태
    private Dictionary<DotPatternState, DotState> states;
    private float position;
    private string dotExpression; //CSV에 의해서 string 들어옴
    private string animKey; //CSV에 의해서 string으로 들어옴 파싱 해줘야한다.

    [SerializeField]
    GameObject mainAlert;
    [SerializeField]
    GameObject playAlert;

    [SerializeField]
    GameObject[] play;

    [SerializeField]
    private int chapter;

    [SerializeField]
    private GameManager manager;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject eyes;

    [SerializeField]
    private Animator eyesAnim;

    [SerializeField]
    GameObject dust;

    public GameObject Dust
    {
        get { return dust; }
    }
    public GameObject Eyes
    {
        get { return eyes; }
    }

    public Animator Animator
    { get { return animator; } }
    
    public Animator EyesAnim
    { get { return eyesAnim; } }

    public int Chapter
    {
        get { return chapter; }
        set { chapter = value; }
    }

    public float Position
    {
        get { return position; }
        set { position = value; }
    }

    public string AnimKey
    {
        get { return animKey; }
        set { animKey = value; }
    }

    public string DotExpression
    {
        get { return dotExpression; }
        set { dotExpression = value; }
    }

    void Awake()
    {

        animator = GetComponent<Animator>();

        Position = -1;
        dotExpression = "";

        states = new Dictionary<DotPatternState, DotState>();
        states.Clear();
        states.Add(DotPatternState.Defualt, new Idle());
        states.Add(DotPatternState.Phase, new Phase());
        states.Add(DotPatternState.Main, new Main());
        states.Add(DotPatternState.Sub, new Sub());
        states.Add(DotPatternState.Tirgger, new Trigger());
    }
    void Start()
    {
        chapter = manager.Chapter;

        animator.keepAnimatorStateOnDisable = true; //애니메이션 유지
    }

    private void OnMouseDown()
    {
        if (mainAlert.activeSelf)
        {
            mainAlert.SetActive(false);
            //main 배경화면을 트리거한다.
            manager.StartMain();
        }

        if(playAlert.activeSelf)
        {
            playAlert.SetActive(false);
            //같이 책을 읽을래? 라는 문구 뜨고 안읽는다고하면 총총총 sleep으로
            for (int i = 0; i < play.Length; i++)
            {
                play[i].SetActive(true);
            }
        }
    }
    public void TriggerMain(bool isActive)
    {
        mainAlert.SetActive(isActive);
        /*여기서 OnClick 함수도 연결해준다.*/
        //OutPos 가 있다면 해당 Position으로 바껴야함.
    }
    public void TriggerPlay(bool isActive)
    {
        playAlert.SetActive(isActive);
        /*여기서 OnClick 함수도 연결해준다.*/
        //OutPos 가 있다면 해당 Position으로 바껴야함.
    }

    public void GoSleep()
    {
        Trigger phase= (Trigger)currentState;

        if(phase!=null)
        {
            phase.GoSleep(this);
        }
    }

    public void EndPlay()
    {
        manager.NextPhase();
    }

    public void ChangeState(DotPatternState state = DotPatternState.Defualt, string OutAnimKey = "", float OutPos = -1, string OutExpression = "")
    {
        if (states == null) return;

        if (states.ContainsKey(state) == false)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit(this); //이전 값을 나가주면서, 값을 초기화 시킨다.
        }

        /*Main으로 넘어가기 전에 anim_default가 뜬다.*/

        animator.SetInteger("DotState", (int)state); //현재 상태를 변경해준다.
        position = OutPos; //이전 위치를 초기화함, 그렇게 하면 모든 상태로 입장했을 때 -1이 아니여서 랜덤으로 뽑지않는다.

        dotExpression = OutExpression; //Update, Main에서만 사용하기 때문에 다른 곳에서는 사용하지 않음.
        animKey = OutAnimKey;
        chapter = manager.Chapter;
        //OutPos 가 있다면 해당 Position으로 바껴야함.
        currentState = states[state];
        currentState.Enter(this); //실행
    }

}
