using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Assets.Script.DialClass;
using Assets.Script.TimeEnum;
using UnityEngine.XR;
using Unity.VisualScripting;
using System;

public class MoonRadioParser : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] List<MoonRadidDial> MoonRadios;
    [SerializeField] public List<object> currentDialogueList = new List<object>();
    private LANGUAGE CurrentLanguage;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset dialogueData = Resources.Load<TextAsset>("Dial/moonradio");
        CurrentLanguage = playerController.GetLanguage();
        if (dialogueData == null)
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
            return;
        }
        Debug.Log("Dialogue file loaded successfully.");
        string[] lines = dialogueData.text.Split('\n');
        LoadMoonRadioDial(lines);
    }

    public void LoadMoonRadioDial(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] parts = ParseCSVLine(line);
            //Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

            if (parts.Length >= 5)
            {
                MoonRadidDial entry = new MoonRadidDial
                {
                    ID = int.Parse(parts[0]),
                    MoonNumber = int.Parse(parts[1]),
                    Actor = parts[2],
                    KorText = ApplyLineBreaks(parts[3]),
                    EngText = ApplyLineBreaks(parts[4]),
                };
                string displayedText = CurrentLanguage == playerController.GetLanguage() ? entry.KorText : entry.EngText;
                entry.KorText = displayedText;
                MoonRadios.Add(entry);
            }
        }
    }

    string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string value = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value.Trim());
                value = "";
            }
            else
            {
                value += c;
            }
        }

        if (!string.IsNullOrEmpty(value))
        {
            result.Add(value.Trim());
        }
        return result.ToArray();
    }
    string ApplyLineBreaks(string text)
    {
        return text.Replace(@"\n", "\n");
    }
}
