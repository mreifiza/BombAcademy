using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPressed : MonoBehaviour {
    Button mybutton;
    Counter counter;
    ChangeColor changecolor;
    public GameObject boxprefabs;
	// Use this for initialization
	
    void Start()
    {
        counter = FindObjectOfType<Counter>();
        mybutton = GetComponent<Button>();
        mybutton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        counter.StartGame();
        gameObject.SetActive(false);
    }


}


