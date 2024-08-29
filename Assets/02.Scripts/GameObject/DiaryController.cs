using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryController : BaseObject, ISleepingInterface
{

    
    bool isClicked = true;
    bool isDiaryUpdated = false;
    [SerializeField]
    GameObject light;
    [SerializeField]
    GameObject alert;
    PlayerController playerController;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        if(playerController == null)
        {
            playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }
        isClicked = playerController.GetIsDiaryCheck(); //다이어리를 읽었는지 가져온다.
        isDiaryUpdated = playerController.GetIsUpdatedDiary();
        //다이어리가 업데이트 되어있지만, 클릭하지 않았을 경우 다이어리는 지속적으로 불빛이 들어온다.
        if (isDiaryUpdated)
        {
            if(isClicked == false)
            {
                OpenSleeping();
                return;
            }
        }

        //클릭했거나, 업데이트가 안됐으면 아무 의미없음
    }
    public void OpenSleeping()
    {
        //Play에서 다이어리가 업데이트
        //다이어리가 업데이트 되었기 때문에 Sleeping으로 들어올땐 항상 다이어리 불빛이 들어온다.
        //다이어리 불빛이 들어온다.    
        if(light.activeSelf == false)
        {
            light.SetActive(true);
            isDiaryUpdated = true;

            if(playerController)
            {
                Debug.Log("플레이어 컨트롤러 여기까지 안와");
                //플레이어 정보도 업데이트 한다.
                playerController.SetIsUpdatedDiary(isDiaryUpdated);
            }
        }
    }

    public void OnMouseDown()
    {
        //클릭했을 때 현재 뭉치가 외출 중인가, Sleeping인가에 따라서 마우스 클릭을 막아야한다.
        GamePatternState CurrentPhase = (GamePatternState)playerController.GetAlreadyEndedPhase();

        if(CurrentPhase != GamePatternState.Watching && CurrentPhase != GamePatternState.Sleeping)
        {
            OpenAlert();
            return;
        }

        if(CurrentPhase == GamePatternState.Watching)
        {
            //AtHome일 때 return;
            string WatchState = DataManager.Instance.Settings.watching.pattern[playerController.GetChapter()];

            EWatching watch;
            if (Enum.TryParse(WatchState,true, out watch))
            {
                if(watch == EWatching.StayAtHome)
                {
                    return;
                }
            }
        }

        isClicked = true;
        //플레이어 정보도 업데이트 한다.
        light.SetActive(false);
        playerController.SetIsDiaryCheck(isClicked);
    }

    public void OpenAlert()
    {
        if (alert.activeSelf == false)
        {
            alert.SetActive(true);
            StartCoroutine(CloseAlter(alert));
        }
    }

    IEnumerator CloseAlter(GameObject alert)
    {
        yield return new WaitForSeconds(1.5f);
        alert.SetActive(false);
    }
}
