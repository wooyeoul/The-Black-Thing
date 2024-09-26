using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatAreaScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text CharacterText;
    bool isFirst = false;
    ChatInterface parent;
    public void SettingText(string text)
    {
        CharacterText.text = text;
        parent = GameObject.Find("Content").GetComponent<ChatInterface>();
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
