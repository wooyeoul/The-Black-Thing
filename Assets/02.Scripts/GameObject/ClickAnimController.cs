using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        animator.SetFloat("speed", 1.0f);
    }
}
