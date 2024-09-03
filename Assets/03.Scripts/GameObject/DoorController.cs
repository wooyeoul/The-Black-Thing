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
    [SerializeField]
    Collider2D targetCollider;

    Animator animator;
    private void OnEnable()
    {
       if(dot == null)
        {
            dot = GameObject.FindWithTag("DotController").gameObject;
        }

       if(isDoorOpen == false)
        {
            Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(targetCollider.bounds.center, targetCollider.bounds.size, 0);

            foreach (Collider2D collider in overlappingColliders)
            {
                if (collider != targetCollider && collider.gameObject==dot)
                {
                    dot.SetActive(false);
                }
            }
        }
       else
        {
            if(dot)
            {
                dot.SetActive(true);
            }
        }
    
    }
    private void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
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
