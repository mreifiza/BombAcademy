using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : BombBehaviour {

    private bool isValid = false;
    public bool valid
    {
        get
        {
            return isValid;
        }
    }
    /*
    public DrawingController()
    {
        Construct();
    }

    public DrawingController(float timeLeft, int difficulty)
    {
       Construct(timeLeft, difficulty);
    }*/

    public override void FeedBombInfo(BombBehaviour bombFeeder)
    {
        Construct(bombFeeder);
    }

    public override void Construct()
    {
        this.timeLeft = BombBehaviour.TIMEFULL;
        this.difficulty = BombBehaviour.EASY;
        this.type = BombBehaviour.LEVELDRAW;
    }

    public override void Construct(float timeLeft, int difficulty)
    {
        this.timeLeft = timeLeft;
        this.difficulty = difficulty;
        this.type = BombBehaviour.LEVELDRAW;
    }

    public override void StartGame()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Pindahin posisi sprite player tersembunyi ke kordinat posisi input
        if (Input.GetMouseButton(0))
        {
            isValid = true;
            Vector3 _temp = transform.position;
            Vector3 _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _moveTo = new Vector3(_pos.x, _pos.y);
            transform.position = Vector3.Lerp(_temp, _moveTo, Time.deltaTime*10);
        }
        else
            isValid = false;
	}

}
