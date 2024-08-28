using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWatchingInterface
{
    public bool IsCurrentPattern(EWatching curPattern);
    public void OpenWatching(int Chapter);
    public void CloseWatching();
}
