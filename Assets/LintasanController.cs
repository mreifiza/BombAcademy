using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintasanController : MonoBehaviour {
    private bool isValid = false;
    private bool isInputDown = false;
    public PlayerController player;
    private Collider2D col;

    void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
    }

	// Use this for initialization
	void Update () {
        isInputDown = player.valid;
        Debug.Log(string.Format("valid = {0}", isValid && isInputDown));
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.CompareTag("Player"))
        {
            isValid = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isValid = false;
        }
    }
}
