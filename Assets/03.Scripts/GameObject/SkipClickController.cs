using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkipClickController : MonoBehaviour
{
    float fadeSpeed = 2.0f;
    bool isAlreadyClick = false;

    [SerializeField]
    Button exit;

    private void Start()
    {
        StartCoroutine(FadeIn(exit.GetComponent<TMP_Text>()));
    }
    IEnumerator FadeIn(TMP_Text text)
    {
        isAlreadyClick = true;
        while (text.alpha < 1)
        {
            text.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        isAlreadyClick = false;
    }
    IEnumerator FadeOut(TMP_Text text)
    {
        while (text.alpha > 0)
        {
            text.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}
