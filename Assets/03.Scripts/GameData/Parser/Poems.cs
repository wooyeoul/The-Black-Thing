using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Text
{
    public string textKor;
    public string textEng;
    internal string text;
}

[System.Serializable]
public class PoemData
{
    public int id;
    public List<Text> text;
}

[System.Serializable]
public class Poems
{
        public List<PoemData> poems;
}
