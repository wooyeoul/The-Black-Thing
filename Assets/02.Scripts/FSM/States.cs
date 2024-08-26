using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watching : GameState
{
    ObjectManager objectManager = null;

    public override void Enter(GameManager manager)
    {
        if(objectManager == null)
        {
            objectManager = manager.ObjectManager;
        }
        
        objectManager.SettingChapter(manager.Chapter);
    }

    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class MainA : GameState
{

    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class Thinking : GameState
{

    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class MainB : GameState
{

    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class Writing : GameState
{

    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class Play : GameState
{
    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}

public class Sleeping : GameState
{
    public override void Enter(GameManager manager)
    {

    }
    public override void Update(GameManager manager)
    {

    }
    public override void Exit(GameManager manager)
    {

    }
}
