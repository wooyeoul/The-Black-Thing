using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum EChecklist
{
    Icon,
    Note
}

[System.Serializable]
public struct checklist
{
    [SerializeField]
    public GamePatternState patternState;

    [SerializeField]
    public EChecklist eChecklist;

    [SerializeField]
    public GameObject IconObject;

    [SerializeField]
    public List<GameObject> noteObjects;
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

    GameObject activeIcon;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerController>();
        pc.nextPhaseDelegate += NextPhase;
        translator = GameObject.FindWithTag("Translator").GetComponent<TranslateManager>();
        translator.translatorDel += Translate;
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
    
    private void InitPhase()
    {
        int Idx = (int)GamePatternState.NextChapter;

        foreach (var Object in checklists[Idx].noteObjects)
        {
            Object.SetActive(false);
        }
    }

    public void NextPhase(GamePatternState state)
    {
        int Idx = (int)state;

        if(state == GamePatternState.Watching)
        {
            InitPhase();
        }

        if (activeIcon)
        {
            activeIcon.SetActive(false);
        }

        activeIcon = checklists[Idx].IconObject;
        activeIcon.SetActive(true);

        foreach (var note in checklists[Idx].noteObjects)
        {
            if (note.activeSelf == false)
            {
                note.SetActive(true);
            }
        }

        if (checklists[Idx].eChecklist == EChecklist.Note)
        {
            OnClickCheckListIcon();
        }
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

    private void OnDisable()
    {
        checkList.SetActive(false);
    }
}
