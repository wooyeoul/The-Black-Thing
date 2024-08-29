using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public static MusicManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return Instance;
        }
    }

    public void AdjustBGMVolume(float volume)
    {

    }

    public void AdjustSEVolume(float volume)
    {

    }
}
