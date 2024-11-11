using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NicknamePopup : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_InputField nameInputField;
    // Start is called before the first frame update
    private void OnEnable()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void SaveNickname()
    {
        playerController.SetNickName(nameInputField.text);
        playerController.WritePlayerFile();
    }
}
