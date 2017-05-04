using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeColor : EventTrigger
{
	Color[] warna = new Color[5] {Color.red, Color.blue, Color.green,Color.yellow,Color.cyan };
	int currentcolor;
	int i;
    Color color;
    Image box_image;
    Counter counter;

    public void Initialization()
    {
        box_image = GetComponent<Image>();
        counter = FindObjectOfType<Counter>(); // save objects(functions) from Counter.cs into variable named counter
    }

    public void GantiWarnaRandom()
    {
        Initialization();
        currentcolor = Random.Range(0, counter.colorlevel);
        box_image.color = warna[currentcolor];
    }

    public void GantiWarna()
    {
        if (currentcolor == counter.colorlevel - 1)
        {
            currentcolor = 0;
            box_image.color = warna[currentcolor];
        }
        else
        {
            currentcolor++;
            box_image.color = warna[currentcolor];
        }
    }

    void Start () 
	{
        Initialization();

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
      if (counter.gamePlaying)
        {
            GantiWarna();

            if (counter.isAllBoxesSame())
            {
                Debug.Log("All boxes are same. Next Level");
                if (counter.level <= 9)
                    counter.SpawnNextLevel();
                else
                    counter.RandomizeAll();
            }
        }  


    }

    
    
}