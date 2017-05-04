using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InsideRoomController : Photon.PunBehaviour
{
    [Tooltip("Text placeholder for displaying the name of the room upon joining room")]
    public Text roomNameText;

    [Tooltip("Text placeholder for displaying the status of the room: open or private")]
    public Text roomStatusText;

    public GameObject[] playerSlot;

    private Vector3 playerSlotPosition = new Vector3(0f, 380f, 0f);
    
    private void Start()
    {
        roomNameText.text = PhotonNetwork.room.Name;
        roomStatusText.text = (PhotonNetwork.room.IsVisible) ? "Open Room" : "Private Room";

        if (PhotonNetwork.player.NickName == "")
            PhotonNetwork.player.NickName = "Anonymous";

        Transform name;

        /* Display all players' nickname in the player slot */
        int playerCount = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            playerSlot[playerCount].transform.Find("Empty Text").gameObject.SetActive(false);
            name = playerSlot[playerCount].transform.Find("Name");
            name.gameObject.SetActive(true);
            if (player.NickName != "")
                name.gameObject.GetComponent<Text>().text = player.NickName;
            else
                name.gameObject.GetComponent<Text>().text = "Anonymous";
            playerCount++;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        MySceneManager.enterPlayMode();
        MySceneManager.InitialLevelSceneLoad();

        //PhotonNetwork.LoadLevel(3);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName);

        /* Disable Empty Text and show player's name upon joining room */
        playerSlot[PhotonNetwork.room.PlayerCount - 1].transform.Find("Empty Text").gameObject.SetActive(false);
        Transform name = playerSlot[PhotonNetwork.room.PlayerCount - 1].transform.Find("Name");
        name.gameObject.SetActive(true);
        if (other.NickName != "")
            name.gameObject.GetComponent<Text>().text = other.NickName;
        else
            name.gameObject.GetComponent<Text>().text = "Anonymous";
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName);

        /* Disable player's name and show empty text when player leaves room */
        Transform name;
        name = playerSlot[PhotonNetwork.room.PlayerCount].transform.Find("Name");
        name.gameObject.SetActive(false);
        playerSlot[PhotonNetwork.room.PlayerCount].transform.Find("Empty Text").gameObject.SetActive(true);

        /* Shift all players upward to fill the player slot from above */
        int playerCount = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            playerSlot[playerCount].transform.Find("Empty Text").gameObject.SetActive(false);
            name = playerSlot[playerCount].transform.Find("Name");
            if (player.NickName != "")
                name.gameObject.GetComponent<Text>().text = player.NickName;
            else
                name.gameObject.GetComponent<Text>().text = "Anonymous";
            playerCount++;
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom() was called.");

        // Load Join Room Screen
        SceneManager.LoadScene(1);
    }

}
