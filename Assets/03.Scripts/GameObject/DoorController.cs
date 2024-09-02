using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    bool isDoorOpen = true;
    [SerializeField]
    GameObject dot;

    Animator animator;

    private void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
        dot = GameObject.FindWithTag("DotController").gameObject;   
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }

        animator.SetFloat("speed", 1.0f);

        if (isDoorOpen)
        {
            dot.SetActive(false);
            //열려있을 경우, 닫아야함
            animator.SetBool("isOpening", false);
        }
        else
        {
            dot.SetActive(true);
            //닫아있는 경우, 열어야함
            animator.SetBool("isOpening", true);
        }
    }
}
