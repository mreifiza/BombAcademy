using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PunLauncher : Photon.PunBehaviour
{

    [Tooltip("Room Name Input Field when creating room")]
    public InputField roomNameInputField;

    [Tooltip("Room Password Input Field when creating room")]
    public InputField passwordInputField;

    [Tooltip("Password Input Field when finding room inside a lobby (after clicking join room button in main screen)")]
    public InputField findRoomInputField;

    [Tooltip("Warning text, wiggle this text if there is no private room found.")]
    public Text warningText;

    [Tooltip("Open Toggle in create room options")]
    public Toggle openToggle;

    [Tooltip("Room select screen after clicking Join Room Button")]
    public GameObject roomSelectScreen;

    [Tooltip("Main screen game object")]
    public GameObject mainRoomScreen;

    [Tooltip("Create room screen game object")]
    public GameObject createRoomScreen;

    [Tooltip("Room Scroll Viewport parent for storing room prefab objects")]
    public GameObject roomScrollViewport;

    [Tooltip("Room slot when currently in lobby (displaying rooms)")]
    public GameObject roomPrefab;

    private string roomName;
    // private string password;

    private string gameVersion = "0.0.1";

    // bool isConnecting = false;
    bool createRoom = false;
    bool createPrivateRoom = false;
    bool displayRoom = false;

    bool findRoom = false;
    string roomToSearch;

    RoomInfo[] rooms;

    private void Awake()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.automaticallySyncScene = true;
    }

    public void BackToRoomMain()
    {
        if (PhotonNetwork.insideLobby)
            PhotonNetwork.LeaveLobby();

        displayRoom = false;
        createRoom = false;
        roomSelectScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        mainRoomScreen.SetActive(true);
    }

    private void Connect()
    {
        // isConnecting = true;

        if (PhotonNetwork.connected)
            Invoke("OnReceivedRoomListUpdate", 0f);
        else
            PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    public void ToCreateRoomScreen()
    {
        mainRoomScreen.SetActive(false);
        createRoomScreen.SetActive(true);
    }

    public void DisplayRooms()
    {
        displayRoom = true;
        Connect();
        mainRoomScreen.SetActive(false);
        roomSelectScreen.SetActive(true);
    }

    public void JoinRoom(string roomNameJoin)
    {
        Debug.Log("Room button clicked, JoinRoom() was called. " + roomNameJoin + " room was joined.");
        PhotonNetwork.JoinRoom(roomNameJoin);
    }

    public void FindRoom()
    {
        Debug.Log("FindRoom() was called.");
        roomToSearch = findRoomInputField.text;
        findRoom = true;
        Connect();
    }

    public void CreateRoom()
    {
        if (roomNameInputField.text == "")
        {
            Debug.Log("Room Name Input Field empty. Assigning default name.");
            roomName = "Let's Have Fun!";
        }
        else
        {
            Debug.Log("Room name set.");
            roomName = roomNameInputField.text;
        }

        if (!openToggle.isOn)
        {
            // password = passwordInputField.text;
            createPrivateRoom = true;
        }

        createRoom = true;
        Connect();
    }

    public void ToggleValueChanged()
    {
        if (openToggle.isOn)
            passwordInputField.interactable = false;
        else
            passwordInputField.interactable = true;
    }

    public override void OnReceivedRoomListUpdate()
    {
        Debug.Log("OnReceivedRoomListUpdate() was called.");

        bool roomExists = false;

        /*
        if (isConnecting)
        {
            rooms = PhotonNetwork.GetRoomList();
            foreach (RoomInfo room in rooms)
            {
                Debug.Log("Available room(s): " + room.Name);
                if (room.Name == roomName)
                {
                    Debug.Log("Room exists.");
                    roomExists = true;
                }
            }
        }
        */

        if (createRoom)
        {
            rooms = PhotonNetwork.GetRoomList();
            foreach (RoomInfo room in rooms)
            {
                Debug.Log("Available room(s): " + room.Name);
                if (room.Name == roomName)
                {
                    Debug.Log("Room exists.");
                    roomExists = true;
                }
            }

            if (!roomExists)
            {
                Debug.Log("Room hasn't existed. Creating room.");
                if (!createPrivateRoom)
                    PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4, IsVisible = true }, null);
                else
                {
                    PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 4, IsVisible = false }, null);
                    createPrivateRoom = false;
                }
            }
        }

        if (displayRoom)
        {
            Debug.Log("Displaying rooms in lobby.");

            // Testing purpose
            /*
            string[] roomNames = new string[] { "Hello From", "The Other Side", "End of the World", "Modern Sakura", "Galaxy" };
            roomScrollViewport.GetComponent<RectTransform>().sizeDelta = new Vector2(690f, 210f * roomNames.Length);
            int roomIndex = 0;
            foreach (string roomName in roomNames)
            {
                float yOffset = 200f * roomIndex;
                Debug.Log("Available room(s): " + roomName);
                GameObject room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
                room.transform.SetParent(roomScrollViewport.transform);
                room.transform.localPosition = new Vector3(0, - 110f - yOffset, 0f);
                room.transform.localScale = new Vector3(1f, 1f, 1f);
                room.transform.Find("Name").GetComponent<Text>().text = roomName;
                room.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(roomName); });
                roomIndex++;
            }
            */
            
            rooms = PhotonNetwork.GetRoomList();
            roomScrollViewport.GetComponent<RectTransform>().sizeDelta = new Vector2(690f, 210f * rooms.Length);
            int roomIndex = 0;
            foreach (RoomInfo room in rooms)
            {
                Debug.Log("Available room(s): " + room.Name);

                // Instantiate Room Display and position it accordingly 
                GameObject roomObject = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
                float yOffset = 200f * roomIndex;
                roomObject.transform.SetParent(roomScrollViewport.transform);
                roomObject.transform.localPosition = new Vector3(-9f, -110f - yOffset, 0f);
                roomObject.transform.localScale = new Vector3(1f, 1f, 1f);

                // Change name display to the room's appropriate name
                roomObject.transform.Find("Name").GetComponent<Text>().text = room.Name;

                // Add listener to the room button, join the room when clicked
                roomObject.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(room.Name); });
                roomIndex++;
            }
          
        }

        if (findRoom)
        {
            PhotonNetwork.JoinRoom(roomToSearch);
            findRoom = false;
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() was called.");
        MySceneManager.LoadScreen(MySceneManager.SCENE_WAITINGROOM);
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Fail to join private room, room doesn't exist.");
        warningText.gameObject.SetActive(true);
        Invoke("RestoreToNormal", 0.8f);
    }

    private void RestoreToNormal()
    {
        warningText.gameObject.SetActive(false);
    }
}
