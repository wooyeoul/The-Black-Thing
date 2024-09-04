namespace Assets.Script.DialClass
{
    [System.Serializable]
    public class DialogueEntry
    {
        public int Main;
        public int ScriptKey;
        public int LineKey;
        public string Background;
        public string Actor;
        public string AnimState;
        public string DotBody;
        public string DotExpression;
        public string TextType;
        public string KorText;
        public string EngText;
        public string NextLineKey;
        public string AnimScene;
        public string AfterScript;
        public string Deathnote;
    }

    [System.Serializable]
    public class SubDialogueEntry
    {
        public int Sub;
        public int ScriptKey;
        public int LineKey;
        public string Color;
        public string Actor;
        public string AnimState;
        public string DotAnim;
        public string TextType;
        public string KorText;
        public string EngText;
        public string NextLineKey;
        public string Deathnote;
        public string AfterScript;
    }

    [System.Serializable]
    
    public class ScriptList
    {
        public int ID;
        public string ScriptType;
        public int ScriptNumber;
        public string ScriptKey;
        public string AnimState;
        public string DotAnim;
        public int DotPosition;
    }

    [System.Serializable]

    public class MoonRadidDial
    {
        public int ID;
        public int MoonNumber;
        public string Actor;
        public string KorText;
        public string EngText;
    }

    [System.Serializable]

    public class SkipSleeping
    {
        public int ID;
        public string Actor;
        public string KorText;
        public string EngText;
    }
}

