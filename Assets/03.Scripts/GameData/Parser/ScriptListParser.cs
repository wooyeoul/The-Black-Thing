using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Assets.Script.DialClass;
using System;

public class ScriptListParser : MonoBehaviour
{
    [SerializeField] 
    protected List<ScriptList> ScriptLists;
    void Start()
    {
        TextAsset dialogueData = Resources.Load<TextAsset>("CSV/ScriptList");

        if (dialogueData == null)
        {
            Debug.LogError("Dialogue file not found in Resources folder.");
            return;
        }
        Debug.Log("Dialogue file loaded successfully.");
        string[] lines = dialogueData.text.Split('\n');
        LoadScriptList(lines);
    }

    public void LoadScriptList(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            string[] parts = ParseCSVLine(line);
            Debug.Log($"Parsed line {i}: {string.Join(", ", parts)}");

            if (parts.Length >= 6)
            {
                ScriptList entry = new ScriptList
                {
                    ID = int.Parse(parts[0]),
                    GameState = int.Parse(parts[1]),
                    ScriptKey = parts[2],
                    AnimState = parts[3],
                    DotAnim = parts[4],
                    DotPosition = int.Parse(parts[5])
                };
                ScriptLists.Add(entry);
            }
        }
    }

    public List<ScriptList> scripts
    {
        get { return ScriptLists; }
        set { ScriptLists = value; }
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
    
}
