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
            // ���콺�� UI ���� ���� ���� �� �Լ��� �������� �ʵ��� ��
            return;
        }

        animator.SetFloat("speed", 1.0f);

        if (isDoorOpen)
        {
            //�������� ���, �ݾƾ���
            animator.SetBool("isOpening", false);
        }
        else
        {
          
            //�ݾ��ִ� ���, �������
            animator.SetBool("isOpening", true);
        }
    }
}