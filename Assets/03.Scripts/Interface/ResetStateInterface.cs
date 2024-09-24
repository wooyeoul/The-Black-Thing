using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetStateInterface
{
    public abstract void ResetState(GameManager manager, DotController dot = null);

}
