using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public enum EVideoIdx
{
    SkipPhase,
    SkipSleeping,
    NoVideo
}
public class VideoPlayerController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    string[] path;

    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    RawImage videoImage;
    [SerializeField]
    GameObject[] loading;

    GameObject video;
    bool isVideoPrepared = false;

    private EVideoIdx eVideoIdx = EVideoIdx.NoVideo; // 현재 재생 중인 비디오 인덱스
    const string googleURL = "https://drive.google.com/uc?export=download&id=";

    private void Start()
    {
        video = videoPlayer.transform.parent.gameObject;
    }

    public void ShowVideo()
    {
        video.SetActive(true);
        loading[(int)eVideoIdx].SetActive(true);

        if (videoPlayer.isPrepared)
        {
            // 비디오가 준비된 경우 바로 재생 시작
            StartCoroutine(PlayVideo());
        }
        else
        {
            // 비디오 준비 완료 시 콜백 등록
            if(eVideoIdx == EVideoIdx.SkipPhase)
            {
                videoPlayer.loopPointReached += OnVideoFinished;
            }
        }
    }

    public async void CloseVideo(EVideoIdx Idx, bool looping = true)
    {

        await PreloadVideoAsync(Idx, looping);

        if (eVideoIdx == EVideoIdx.NoVideo) return;

        video.SetActive(false);
        loading[(int)eVideoIdx].SetActive(false);

    }
    // 비디오 미리 로드
    public async Task PreloadVideoAsync(EVideoIdx Idx, bool looping = true)
    {
        if (eVideoIdx != Idx)
        {
            eVideoIdx = Idx;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = googleURL + path[(int)Idx];
            videoPlayer.isLooping = looping;

            // 비동기 준비
            await PreloadVideoAsync((int)Idx);
        }
    }

    // 비디오 미리 로드 비동기 함수
    private async Task PreloadVideoAsync(int Idx)
    {
        // 오디오 설정
        var audioSource = videoPlayer.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, audioSource);
        }

        // 비디오 준비 (비동기)
        await PrepareVideoAsync();
    }

    // 비디오 준비 완료를 비동기로 대기하는 함수
    private Task PrepareVideoAsync()
    {
        var tcs = new TaskCompletionSource<bool>();

        videoPlayer.Prepare(); // 비디오 준비 시작
        StartCoroutine(CheckPrepared(tcs)); // 코루틴으로 준비 상태 체크

        return tcs.Task;
    }

    // 비디오 준비 상태를 체크하는 코루틴
    private IEnumerator CheckPrepared(TaskCompletionSource<bool> tcs)
    {
        while (!videoPlayer.isPrepared)
        {
            yield return null; // 비디오가 준비될 때까지 대기
        }

        // 준비 완료 시 Task를 성공 상태로 설정
        tcs.SetResult(true);

        isVideoPrepared = true;
    }
    // 비디오 재생
    IEnumerator PlayVideo()
    {
        if (isVideoPrepared)
        {
            // 미리 준비된 비디오가 있으면 바로 재생
            videoImage.texture = videoPlayer.texture;
            videoPlayer.Play();
            // 비디오 재생 동안 대기
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }

            isVideoPrepared = false;
        }


    }


    // 비디오 재생이 완료되면 호출되는 콜백
    private void OnVideoFinished(VideoPlayer vp)
    {
        // 비디오가 끝났을 때 GameManager의 메서드 호출
        gameManager.OnVideoCompleted();

        // 콜백 등록 해제
        if(eVideoIdx == EVideoIdx.SkipPhase)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}