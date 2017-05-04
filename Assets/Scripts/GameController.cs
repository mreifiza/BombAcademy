using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject keyboard, textAppear;
    public Text timeText;

    bool gamePlaying = false;
    float timeLeft = 5f;

    public GameObject timeBar;

    Dictionary dictionary;
    public Text bombText;

    int level = 1;
    public Text levelText;

    public Text textTyped;
    public Text boomText;

    private void Start()
    {
        dictionary = FindObjectOfType<Dictionary>();
    }

    public void StartGame()
    {
        if (!gamePlaying)
        {
            gamePlaying = true;
            keyboard.SetActive(true);
            textAppear.SetActive(true);
            levelText.text = "Level " + level;
            boomText.text = "";
            bombText.text = dictionary.words[level - 1, Random.Range(0, 49)];
        }
    }

    private void Update()
    {
        if (gamePlaying)
        {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, 5f);
            timeText.text = timeLeft.ToString("F2");
            timeBar.transform.localScale = new Vector3(timeLeft / 5f, 1, 1);

            if (timeLeft <= 0f)
            {
                boomText.text = "BOOM!";
                Transition();
                level = 1;
            }
        }
    }

    /* Alphabets in keyboard pressed */
    public void KeyPressed(char key)
    {
        if (textTyped.text.Length < 10)
            textTyped.text += key;
    }

    public void BackSpacePressed()
    {
        textTyped.text = textTyped.text.Remove(textTyped.text.Length - 1, 1);  
    }

    public void EnterPressed()
    {
        if (textTyped.text == bombText.text && timeLeft > 0f)
            UpdateLevel();
        else
        {
            StartCoroutine(WrongText());
            Invoke("RestoreToNormal", 0.3f);
        }
    }
    
    IEnumerator WrongText()
    {
        textTyped.color = Color.red;

        float wrongTime = 0f;
        while (wrongTime < 0.3f)
        {
            wrongTime += Time.deltaTime;
            textTyped.rectTransform.localPosition = new Vector3(Mathf.Lerp(-20f, 20f, Mathf.Sin(wrongTime * 57.29f)), 0f, 0f);
            Debug.Log(wrongTime);

            yield return null;
        }
    }
    
    private void RestoreToNormal()
    {
        textTyped.color = Color.white;
        textTyped.rectTransform.localPosition = Vector3.zero;
    }

    private void Transition()
    {
        gamePlaying = false;

        levelText.text = "Level " + level;
        timeText.text = "5.00";
        timeLeft = 5f;

        bombText.text = "";
        textTyped.text = "";

        keyboard.SetActive(false);
        textAppear.SetActive(false);
    }

    private void UpdateLevel()
    {
        if (level < 4)
            level++;
        
        bombText.text = dictionary.words[level - 1, Random.Range(0, dictionary.words.GetLength(1))];
        Transition();
    }

    public void EndGame()
    {
        level = 1;
        Transition();
        SceneManager.LoadScene(0);
    }

}
