using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField]
    bool isDoorOpen;

    Animator animator;

    private void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        animator.SetFloat("speed", 1.0f);

        if (isDoorOpen)
        {
            //열려있을 경우, 닫아야함
            animator.SetBool("isOpening", false);
        }
        else
        {
            //닫아있는 경우, 열어야함
            animator.SetBool("isOpening", true);
        }
    }
}
