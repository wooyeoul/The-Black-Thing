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
    BoxCollider2D doorCollider;

    public void Awake()
    {
        dot = GameObject.FindWithTag("DotController");
        doorCollider = this.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        if (dot == null)
        {
            dot = GameObject.FindWithTag("DotController");
        }

        if (isDoorOpen == false)
        {
            Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(targetCollider.bounds.center, targetCollider.bounds.size, 0);

            foreach (Collider2D collider in overlappingColliders)
            {
                if (collider != targetCollider && collider.gameObject == dot)
                {
                    SpriteRenderer dotRenderer = dot.GetComponent<SpriteRenderer>();
                    Color color = dotRenderer.color;
                    color.a = 0f; 
                    dotRenderer.color = color;
                    dot.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        else
        {
            if (dot.GetComponent<BoxCollider2D>().enabled == false)
            {
                SpriteRenderer dotRenderer = dot.GetComponent<SpriteRenderer>();
                Color color = dotRenderer.color;
                color.a = 255f;
                dotRenderer.color = color;
                dot.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void Start()
    {
       
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }


        if (isDoorOpen)
        {
            //열려있을 경우, 닫아야함
            close();
        }
        else
        {
            //닫아있는 경우, 열어야함
            open();
        }
    }
    public void close()
    {
        int OpenIdx = Animator.StringToHash("isOpening");
        animator = this.transform.parent.GetComponent<Animator>();
        animator.SetFloat(Animator.StringToHash("speed"), 1.0f);
        animator.SetBool(OpenIdx, false);
    }
    public void open()
    {
        int OpenIdx = Animator.StringToHash("isOpening");
        animator = this.transform.parent.GetComponent<Animator>();
        animator.SetFloat(Animator.StringToHash("speed"), 1.0f);
        animator.SetBool(OpenIdx, true);
    }

    public void Touch()
    {
        if (isDoorOpen)
        {
            //열려있을 경우, 닫아야함
            close();
        }
        else
        {
            //닫아있는 경우, 열어야함
            open();
        }
    }

    public void DisableTouch()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = false; // 문에 대한 터치/클릭 비활성화
        }
    }

    public void EnableTouch()
    {
        if (doorCollider != null)
        {
            doorCollider.enabled = true; // 문에 대한 터치/클릭 활성화
        }
    }
}
