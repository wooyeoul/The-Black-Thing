using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateManager : MonoBehaviour
{
    public delegate void TranslateLanaguageDelegate(LANGUAGE language);

    public TranslateLanaguageDelegate translatorDel;

    public void Translate(LANGUAGE language)
    {
        translatorDel(language);
    }
}
