using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ClickType
{
    Bread,
    Radio,
    Hourglass
}
public class ClickAnimController : BaseObject
{
    // Start is called before the first frame update

    Animator animator;

    [SerializeField]
    [Tooltip("재사용을 위한 ClickObject Type")]
    ClickType Type;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 마우스가 UI 위에 있을 때는 이 함수가 동작하지 않도록 함
            return;
        }
        //animator.SetFloat("speed", 1.0f);
    }
}
