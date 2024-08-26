using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class GameState
{ 
    public abstract void Enter(GameManager manager);
    public abstract void Update(GameManager manager);
    public abstract void Exit(GameManager manager);
}
