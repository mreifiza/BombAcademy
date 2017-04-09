using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool isValid = false;
    public bool valid
    {
        get
        {
            return isValid;
        }
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
