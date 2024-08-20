using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ScrollManager : MonoBehaviour
{
    // 상수 : 이동 관련
    private const float DirectionForceReduceRate = 0.935f; // 감속비율
    private const float DirectionForceMin = 0.001f; // 설정치 이하일 경우 움직임을 멈춤

    // 변수 : 이동 관련
    private bool _userMoveInput; // 현재 조작을 하고있는지 확인을 위한 변수
    private Vector3 _startPosition;  // 입력 시작 위치를 기억
    private Vector3 _directionForce; // 조작을 멈췄을때 서서히 감속하면서 이동 시키기 위한 변수

    // 컴포넌트
    private Camera _camera;

    [SerializeField]
    [Tooltip("LimitValue (minVal,maxVal)")]
    Vector2 camLimitValue;

    [SerializeField]
    [Tooltip("Scroll Speed")]

    [Range(0f, 1f)]
    float scollSpd;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 포지션 이동
        ControlCameraPosition();

        // 조작을 멈췄을때 감속
        ReduceDirectionForce();

        // 카메라 위치 업데이트
        UpdateCameraPosition();

    }

    private void ControlCameraPosition()
    {
        //World 좌표 값을 가져온다.
        var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) //입력이 처음 들어왔을 때
        {
            CameraPositionMoveStart(mouseWorldPosition); //누른 시작값 전달
        }
        else if (Input.GetMouseButton(0)) //입력이 진행 중일 때
        {
            CameraPositionMoveProgress(mouseWorldPosition); //이동 방향을 구하기 위해서 현재 좌표값
        }
        else
        {
            CameraPositionMoveEnd();
        }
    }
    private void CameraPositionMoveStart(Vector3 startPosition)
    {
        _userMoveInput = true;
        _startPosition = startPosition;
        _directionForce = Vector2.zero;
    }
    private void CameraPositionMoveProgress(Vector3 targetPosition)
    {
        if (!_userMoveInput)
        {
            CameraPositionMoveStart(targetPosition);
            return;
        }

        //이전 위치에서 현재 위치를 빼서 방향을 구한다.
        _directionForce = _startPosition - targetPosition;
    }
    private void CameraPositionMoveEnd()
    {
        _userMoveInput = false;
    }
    private void ReduceDirectionForce()
    {
        // 조작 중일때는 아무것도 안함 -> 감속하지 않아도 된다., 스크롤 느낌을 주기위해서 감속을 사용
        if (_userMoveInput)
        {
            return;
        }

        // 감속 수치 적용, 현재 방향성에서 속도를 조금씩 약하게 주면서 감속해준다.
        _directionForce *= DirectionForceReduceRate;

        // 작은 수치가 되면 강제로 멈춤
        if (_directionForce.magnitude < DirectionForceMin)
        {
            _directionForce = Vector3.zero;
        }
    }
    private void UpdateCameraPosition()
    {
        // 이동 수치가 없으면 아무것도 안함
        if (_directionForce == Vector3.zero)
        {
            return;
        }

        var currentPosition = transform.position; //현재 위치
        var targetPosition = currentPosition + _directionForce; //힘을 더해주면 목표 위치가 나온다.

        targetPosition.x = Mathf.Clamp(targetPosition.x, camLimitValue.x, camLimitValue.y);
        targetPosition.y = 0;
        targetPosition.z = -10f;

        transform.position = Vector3.Lerp(currentPosition, targetPosition, scollSpd);
    }
}
