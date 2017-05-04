using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameInputField : MonoBehaviour
{

    static string nicknamePrefKey = "nickname";

    private void Start()
    {
        string savedNickname = "";

        InputField nicknameInputField = GetComponent<InputField>();

        if (PlayerPrefs.HasKey(nicknamePrefKey))
            savedNickname = PlayerPrefs.GetString(nicknamePrefKey);

        if (nicknameInputField != null)
            nicknameInputField.text = savedNickname;

        PhotonNetwork.playerName = savedNickname;
    }

    public void SetPlayerNickname(string value)
    {
        PhotonNetwork.playerName = value;
        PlayerPrefs.SetString(nicknamePrefKey, value);
    }

}
