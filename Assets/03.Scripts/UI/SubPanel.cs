using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Script.DialClass;

public class SubPanel : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController pc;
    [SerializeField] private DotController dot;
    [SerializeField] private SubDialogue sub;

    [SerializeField] private TextMeshProUGUI DotTextUI;
    [SerializeField] private TextMeshProUGUI PlayTextUI;
    [SerializeField] private TextMeshProUGUI InputTextUI;

    // 리스트로 묶은 Dot 게임 오브젝트들
    [SerializeField] private List<GameObject> dotObjects = new List<GameObject>();

    // 리스트로 묶은 PR_TB 게임 오브젝트들
    [SerializeField] private List<GameObject> prTbObjects = new List<GameObject>();

    [SerializeField] private GameObject SubTwoSelection;

    public int dialogueIndex = 0;  // Current dialogue index
    public int Day = 0;  // Current day

    void OnEnable()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        InitializePanels();
    }

    public void InitializePanels()
    {
        Transform parentTransform = this.transform;
        for (int i = 0; i < dotObjects.Count; i++)
        {
            GameObject instantiatedDot = Instantiate(dotObjects[i], parentTransform);
            instantiatedDot.SetActive(false);
            instantiatedDot.AddComponent<CanvasGroup>();
            dotObjects[i] = instantiatedDot;
        }

        for (int i = 0; i < prTbObjects.Count; i++)
        { 
            GameObject instantiatedPrTb = Instantiate(prTbObjects[i], parentTransform);
            instantiatedPrTb.SetActive(false);
            instantiatedPrTb.AddComponent<CanvasGroup>();
            prTbObjects[i] = instantiatedPrTb;
        }

        GameObject instantiatedSubTwoSelection = Instantiate(SubTwoSelection, parentTransform);
        instantiatedSubTwoSelection.SetActive(false);
        instantiatedSubTwoSelection.AddComponent<CanvasGroup>();
        SubTwoSelection = instantiatedSubTwoSelection;
    }


    void ShowSelection(string options)
    {
        string[] selections = options.Split('|');
        for (int i = 0; i < selections.Length; i++)
        {
            Button button = SubTwoSelection.transform.GetChild(i).GetComponent<Button>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = selections[i];
            int index = i;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnSelectionClicked(index));
        }
    }

    public void OnSelectionClicked(int index)
    {
        var currentEntry = sub.GetData(dialogueIndex);
        if (currentEntry.NextLineKey != null)
        {
            string[] nextKeys = currentEntry.NextLineKey.Split('|');

            if (index < nextKeys.Length && int.TryParse(nextKeys[index], out int nextLineKey))
            {
                int nextIndex = sub.currentDialogueList.FindIndex(entry => (entry as DialogueEntry)?.LineKey == nextLineKey);

                if (nextIndex != -1)
                {
                    dialogueIndex = nextIndex;
                }
                else
                {
                    Debug.Log("Next LineKey not found in dialogue list. Ending dialogue.");
                    DialEnd();
                    return;
                }
            }
            else
            {
                Debug.Log("Invalid NextLineKey index or parse failure. Ending dialogue.");
                DialEnd();
                return;
            }
        }
        else
        {
            Debug.Log("Current entry is null. Ending dialogue.");
            DialEnd();
            return;
        }

        //ShowNextDialogue();
    }

    public void DialEnd()
    {
        PanelOff();
        sub.currentDialogueList.Clear();
        dialogueIndex = 0;
    }
    void PanelOff()
    {
        List<GameObject>[] panels = { dotObjects, prTbObjects };
        foreach (List<GameObject> panel in panels)
        {
            foreach (GameObject go in panel)
            {
                go.SetActive(false);
            }
        }
    }

    public void ShowNextDialogue()
    {
        PanelOff();
        if (dialogueIndex >= sub.currentDialogueList.Count)
        {
            DialEnd();
            return;
        }
        string textType = sub.GetData(dialogueIndex).TextType;
        string actor = sub.GetData(dialogueIndex).Actor;
        string korText = sub.GetData(dialogueIndex).Text;

        switch (textType)
        {
            case "text":
                if (actor == "Dot")
                {
                    if (korText.Contains("<nickname>"))
                    {
                        if (pc)
                        {
                            korText = korText.Replace("<nickname>", pc.GetNickName());
                        }
                    }

                    // if 컬러가 검은 색이면 dotObjects의 B를 가져오고 아니면 각 시간에 맞는 말풍선을
                    // AND if Dotcontroller.transform 으로 x좌표가 음수면 L 를 아니면 R을 플레이어는 그 반대로
                    //각 조건에 맞는 말풍선 켜지게끔
                    DotTextUI.text = $"{korText}";
                    //StartCoroutine(FadeIn(DotPanel.GetComponent<CanvasGroup>(), 0.5f, DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>())); -> 페이드 인 나중에 추가
                    //RegisterNextButton(DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>()); -> 버튼 등록은 나중에
                }
                else if (actor == "Player")
                {
                    PlayTextUI.text = $"{korText}";
                    //StartCoroutine(FadeIn(PlayPanel.GetComponent<CanvasGroup>(), 0.5f, PlayPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>())); -> 페이드 인 나중에 추가
                    //RegisterNextButton(DotPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>()); -> 버튼 등록은 나중에
                }
                break;
        }

    }

    IEnumerator FadeIn(CanvasGroup canvasGroup, float duration, Button button)
    {
        float counter = 0f;
        button.interactable = false;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
        button.interactable = true;
    }

    void RegisterNextButton(Button button)
    {
        button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(NextDialogue); -> NextDialougue 함수 만들어야함
    } 

}