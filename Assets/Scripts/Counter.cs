using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    public Canvas canvas;
    public int level = 0;
    public int colorlevel = 3;
    ChangeColor[] boxes;
    public GameObject boxprefabs;
    ChangeColor nextplay;
    public Text timeText;
    public Text levelText;

    public Button startButton;
    
    public bool gamePlaying { get; private set; }

    float timeLeft = 5f;

    GameObject spawnedBox;

    public void StartGame()
    {
        Debug.Log(gamePlaying);
        if (!gamePlaying)
        {
            gamePlaying = true;
            level = 0;
            colorlevel = 3;
            timeLeft = 5f;

            spawnedBox = Instantiate(boxprefabs, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, canvas.transform) as GameObject;
            spawnedBox.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            spawnedBox = Instantiate(boxprefabs, new Vector3(300.0f, 0.0f, 0.0f), Quaternion.identity, canvas.transform) as GameObject;
            spawnedBox.transform.localPosition = new Vector3(300.0f, 0.0f, 0.0f);

            spawnedBox = Instantiate(boxprefabs, new Vector3(-300.0f, 0.0f, 0.0f), Quaternion.identity, canvas.transform) as GameObject;
            spawnedBox.transform.localPosition = new Vector3(-300.0f, 0.0f, 0.0f);

            RandomizeAll();
            levelText.text = "Level : " + (level + 1);
        }
    }

    public bool isAllBoxesSame()
    {
        foreach (ChangeColor box in boxes)
        {
            if (box.GetComponent<Image>().color != boxes[0].GetComponent<Image>().color)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnNextLevel()
    {
        Vector3[] spawnposition = new Vector3[6] { new Vector3(0.0f, 300.0f, 0.0f), new Vector3(300.0f, 300.0f, 0.0f), new Vector3(-300.0f, 300.0f, 0.0f), new Vector3(0.0f, -300.0f, 0.0f), new Vector3(300.0f, -300.0f, 0.0f), new Vector3(-300.0f, -300.0f, 0.0f), };
        
        timeLeft = 5f;
        spawnedBox = Instantiate(boxprefabs, spawnposition[level], Quaternion.identity, canvas.transform) as GameObject;
        spawnedBox.transform.localPosition = spawnposition[level];

        boxes = FindObjectsOfType<ChangeColor>();

        if (level <= 8)
        {
            level++;
            levelText.text = "Level :" + (level + 1);
        }
          
        
        if (colorlevel < 5)
            colorlevel++;
        Debug.Log(level);
        RandomizeAll();
    }

    public void RandomizeAll()
    {
        boxes = FindObjectsOfType<ChangeColor>();
        foreach (ChangeColor barang in boxes)
        {
            barang.GantiWarnaRandom();
        }
        RestrictSameColor();
    }

    public void RestrictSameColor()
    {
        if (isAllBoxesSame())
        {
            RandomizeAll();
        }
    }


    
    void Awake ()
    {
 
        boxes = FindObjectsOfType<ChangeColor>();
        levelText.text = "Level :" + (level + 1);
        gamePlaying = false;
    }

    private void Update()
    {
        if (gamePlaying)
        {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, 5f);
            timeText.text = timeLeft.ToString("F2");

            if (timeLeft <= 0f)
            {
                gamePlaying = false;
                RestartLevel();
                startButton.gameObject.SetActive(true);
                foreach (ChangeColor box in boxes)
                {
                    Destroy(box.gameObject);    
                }

            }
        }

    }

    private void RestartLevel()
    {
        level = 0;
        RandomizeAll();
    }



}
