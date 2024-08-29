using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 문제: 위에 검은 배경으로 덮어버리면 원래배경에서 스크롤, 클릭이 안됨 -> 검은 배경 Raytarget 을 제거하면 되지만 이러면 OpticMoving을 못씀 ...
public class MungchiClick : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isinoptic = false;
    Vector3 vel = Vector3.zero;
    [SerializeField]
    GameObject exit;

    [SerializeField]
    SpriteRenderer opticBackground;
    [SerializeField]
    float speed=1f;

    public void OnMouseDown()
    {
        Animator[] animator = this.GetComponentsInChildren<Animator>();

        for (int i = 0; i < animator.Length; i++)
        {
            animator[i].SetTrigger("Trigger");
            animator[i].SetBool("BoolAni", true);
        }
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(FadeOutBino());
    }

    IEnumerator FadeOutBino(){
        
        Color initColor = opticBackground.color;

        while(initColor.a > 0.1f)
        {
            initColor.a -= Time.deltaTime * speed ;
            opticBackground.color = initColor;
            yield return null; //다음 프레임에서 실행
        }
        yield return new WaitForSeconds(1f);
        exit.SetActive(true);
    }

    void Start()
    {
        isinoptic = false;
    }

    // Update is called once per frame
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "optic")
        {
            isinoptic = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "optic")
        {
            isinoptic = false;
        }
    }

}
