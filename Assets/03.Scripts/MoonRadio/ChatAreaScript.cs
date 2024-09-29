using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatAreaScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    TMP_Text CharacterText;
    bool isFirst = false;
    IChatInterface parent;
    public void SettingText(string text, IChatInterface parent)
    {
        CharacterText.text = text;
        this.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFirst)
        {
            if (parent != null)
            {
                parent.RunScript();
                isFirst = true;
                return;
            }
        }
    }
}
