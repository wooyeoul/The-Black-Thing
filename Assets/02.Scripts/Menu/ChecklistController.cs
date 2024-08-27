using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct checklist
{
    [SerializeField]
    public GamePatternState endedState;

    [SerializeField]
    public GameObject preicon;

    [SerializeField]
    public GameObject icon;

    [SerializeField]
    public GameObject note;
}
public class ChecklistController : MonoBehaviour
{
    private PlayerController pc;

    [SerializeField]
    GameObject checkList;
    [SerializeField]
    GameObject skipBut;
    [SerializeField]
    checklist[] checklists;

    TranslateManager translator;

    [SerializeField]
    TMP_Text[] phase; 
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerController>();
        pc.nextPhaseDelegate += NextPhase;
        translator = GameObject.FindWithTag("Translator").GetComponent<TranslateManager>();
        translator.translatorDel += Translate;

        InitPhase((GamePatternState)pc.GetAlreadyEndedPhase());
    }

    void Translate(LANGUAGE language)
    {
        //번역한다.
        Debug.Log("Checklist 번역합니다.\n");

        int Idx = (int)language;

        phase[0].text = DataManager.Instance.Settings.checklist.phase1[Idx];
        phase[1].text = DataManager.Instance.Settings.checklist.phase2[Idx];
        phase[2].text = DataManager.Instance.Settings.checklist.phase3[Idx];
        phase[3].text = DataManager.Instance.Settings.checklist.phase4[Idx];

    }
    
    private void InitPhase(GamePatternState state)
    {
        for (int idx = 0; idx < checklists.Length; idx++)
        {
            if (checklists[idx].note != null)
            {
                checklists[idx].note.SetActive(true);
            }
            if (checklists[idx].preicon != null)
            {
                checklists[idx].preicon.SetActive(false);
            }

            if (checklists[idx].endedState == state)
            {
                checklists[idx].icon.SetActive(true);
                break;
            }
        }
    }

    public void NextPhase(GamePatternState state)
    {

        if (state == GamePatternState.MainB || state == GamePatternState.MainA)
        {
            //SkipBut 사라짐
            //skipBut.SetActive(false);
            Debug.Log("Main 상관 없는 Phase, 체크하지 않는다.");
            return;
        }

        //reset
        if (state == GamePatternState.Watching)
        {
            //모두 꺼야함.
            for (int idx = 1; idx < checklists.Length; idx++)
            {
                checklists[idx].note.SetActive(false);
                checklists[idx].icon.SetActive(false);
            }
            checklists[0].icon.SetActive(true);

            return;
        }

        for (int idx = 0; idx < checklists.Length; idx++)
        {
            if (checklists[idx].preicon != null)
            {
                checklists[idx].preicon.SetActive(false);
            }

            if (checklists[idx].endedState == state)
            {
                if (checklists[idx].note != null)
                {
                    checklists[idx].note.SetActive(true);
                }

                checklists[idx].icon.SetActive(true);

                break;
            }
        }

        OnClickCheckListIcon();
    }

    public void OnClickCheckListIcon()
    {
        if (checkList.activeSelf == false)
        {
            checkList.SetActive(true);
            StartCoroutine(CloseAlter(checkList));
        }
        else
            checkList.SetActive(false);
    }

    IEnumerator CloseAlter(GameObject checkList)
    {
        yield return new WaitForSeconds(2f);
        checkList.SetActive(false);
    }
}
