using System;

[System.Serializable]
public class PlayerInfo
{
    public int _id; //player 구분 키 값
    public string _nickname;
    //player 입장 시간 - 입장한 날짜까지 전부 들고 있는다. 
    public  DateTime _datetime;
    //현재 진행 중인 챕터
    public int _chapter;
    
    public float _bgmVolume=50f;
    public float _acousticVolume=50f;

    public int _alreadyEndedPhase=0; //0일때는 아직 진행 안함
    public bool _isDiaryCheck = false;

    public PlayerInfo()
    {
        _id=0;
        _nickname="default";
        _chapter=1;
        _datetime = DateTime.Now;
        _bgmVolume=50.0f;
        _acousticVolume=50.0f;
        _alreadyEndedPhase=0;
        _isDiaryCheck=false;
    }
    public PlayerInfo(int id,string nickname,int chapter){
        _id=id;
        _nickname=nickname;
        _chapter=chapter;
    }

    public bool isDiaryCheck { get => _isDiaryCheck; set=>_isDiaryCheck = value;}
    public int AlreadyEndedPhase { get=>_alreadyEndedPhase; set=>_alreadyEndedPhase = value;}

    public float BgmVolume{ get=>_bgmVolume; set=>_bgmVolume = value; }
    public float AcousticVolume { get=>_acousticVolume; set=>_acousticVolume=value; }
    public int CurrentChapter { get => _chapter; set => _chapter = value; }
    public DateTime Datetime { get => _datetime; set => _datetime = value;}
    public string Nickname { get => _nickname; set => _nickname = value; }
    public int Id { get => _id; set => _id = value; }
}
