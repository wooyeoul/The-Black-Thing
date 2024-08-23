using System;
using System.Collections.Generic;

/*Sub 성공 챕터 유무에 따라 아키텍처 타입이 달라짐*/

[System.Serializable]
public struct ArcheType
{
    public int sun;//해와 달 중 구분
    public int moon; 
    public int actively; //적극과 소극으로 구분
    public int negative;
}
[System.Serializable]
public class PlayerInfo
{
    public int id; //player 구분 키 값
    public string nickname;
    //player 입장 시간 - 입장한 날짜까지 전부 들고 있는다. 
    public  DateTime datetime;
    //현재 진행 중인 챕터
    public int chapter;
    
    public float bgmVolume=50f;
    public float acousticVolume=50f;

    public int alreadyEndedPhase=0; //0일때는 아직 진행 안함
    public bool isDiaryCheck = false;
    public bool isPushNotificationEnabled = true;
    public LANGUAGE language; //

    public List<bool> subSuccessOrNot;
    public ArcheType archeType;

    public PlayerInfo()
    {
        if(subSuccessOrNot == null)
        {
            subSuccessOrNot = new List<bool>();
        }
        id =0;
        nickname="default";
        chapter=1;
        datetime = DateTime.Now;
        bgmVolume=50.0f;
        acousticVolume=50.0f;
        alreadyEndedPhase=0;
        isDiaryCheck=false;
        language = LANGUAGE.KOREAN;
        isPushNotificationEnabled = true;
    }

    public PlayerInfo(int id,string nickname,int chapter){
        this.id= id;
        this.nickname= nickname;
        this.chapter= chapter;
    }

    public bool IsDiaryCheck { get => isDiaryCheck; set=>isDiaryCheck = value;}
    public int AlreadyEndedPhase { get=>alreadyEndedPhase; set=>alreadyEndedPhase = value;}

    public float BgmVolume{ get=>bgmVolume; set=>bgmVolume = value; }
    public float AcousticVolume { get=>acousticVolume; set=>acousticVolume=value; }
    public int CurrentChapter { get => chapter; set => chapter = value; }
    public DateTime Datetime { get => datetime; set => datetime = value;}
    public string Nickname { get => nickname; set => nickname = value; }
    public int Id { get => id; set => id = value; }
}
