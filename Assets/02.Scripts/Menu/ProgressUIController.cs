using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressUIController : MonoBehaviour
{
    [SerializeField]
    int chapter = 0; //현재 day가 무엇인지 확인하기 위해서 serializeField로 변경...
    //1Day로 바꿀 예정
    [SerializeField]
    bool isInstant;
    
    [SerializeField]
    GameObject dragScroller;
    // Update is called once per frame

    [SerializeField]
    GameObject alter;
    [SerializeField]
    GameObject detailed_popup;
    [SerializeField]
    GameObject menu_default;
    float width=0;

    bool isFirst=true;
    //현재 생성된 개수를 알아야함 
    PlayerController player;
    //임시 타이틀 배열
    #region 상세 팝업을 위한 변수
    [SerializeField]
    GameObject day_progress;
    [SerializeField]
    List<GameObject> phases;
    #endregion

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
    }

    public void Scroll()
    {
        float val=dragScroller.GetComponent<ScrollRect>().horizontalNormalizedPosition;
        if(val>=1f)
        {
            alter.SetActive(true);
        }
    }

    public void onClickdragIcon(){
        GameObject day=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        if(day.GetComponent<DragIcon>().isLocking()){
            alter.SetActive(true);
        }else{
            dragScroller.transform.parent.gameObject.SetActive(false); //메인 다이얼로그 진행
            detailed_popup.SetActive(true); //서브 다이얼로그 설정(진행바)

            int findChapter=1;
            foreach(KeyValuePair<int, GameObject> item in MenuController.prograssUI) {
                if(item.Value.name==day.name){
                    findChapter=item.Key;
                    break;
                }
            }
            detailed_popup.GetComponent<ChapterProgressManager>().PassData(MenuController.chapterList.chapters[findChapter],player);
        }
    }

    public void canceled(){
        alter.SetActive(false);
    }
    
    public void exit(){
        //현재 게임 오브젝트가 DayProgress_Default이면, DayProgressUI SetActive한다.

        if(detailed_popup.activeSelf){
            detailed_popup.SetActive(false);
            dragScroller.transform.parent.gameObject.SetActive(true);
        }else{
            menu_default.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }

}
