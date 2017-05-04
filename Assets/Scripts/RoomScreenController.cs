using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomScreenController : MonoBehaviour
{
    public GameObject mainRoomScreen;

    public GameObject howToPlayScreen;
    public Button backButton;
    public Button howToPlayButton;
    public Button joinRoomButton;
    public Button createRoomButton;

    public GameObject[] instructionPages;

    int pageNumber;
    public Text pageNumberText;

    private void Start()
    {
        // Initialize page number
        pageNumber = 0;
    }

    public void ChangePage(bool next)
    {
        instructionPages[pageNumber].SetActive(false);

        if (next)
        {
            if (pageNumber < 3)
                pageNumber++;
        }
        else
        {
            if (pageNumber > 0)
            pageNumber--;
        }

        instructionPages[pageNumber].SetActive(true);

        pageNumberText.text = (pageNumber + 1) + "/4";
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        howToPlayScreen.SetActive(true);
        backButton.enabled = false;
        howToPlayButton.enabled = false;
        joinRoomButton.interactable = false;
        createRoomButton.interactable = false;
    }

    public void HowToPlayClosePressed()
    {
        howToPlayScreen.SetActive(false);
        backButton.enabled = true;
        howToPlayButton.enabled = true;
        joinRoomButton.interactable = true;
        createRoomButton.interactable = true;
    }
	
}
