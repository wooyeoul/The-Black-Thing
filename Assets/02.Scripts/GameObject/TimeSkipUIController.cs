using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSkipUIController : MonoBehaviour
{
    [SerializeField]
    GameObject popup;

    [SerializeField]
    PlayerController playerController;

    private void Start()
    {
        if(playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
    }

    public void OnClick()
    {
        if (popup.activeSelf == false)
        {
            popup.SetActive(true);
        }
    }

    public void NoClick()
    {
        popup.SetActive(false);
    }

    public void YesClick()
    {
        popup.SetActive(false);
        playerController.NextPhase();
    }
}
