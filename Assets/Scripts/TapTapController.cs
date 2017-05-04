using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapTapController : BombBehaviour {

    public float startwait;
    public Canvas canvas;
    public int level = 1;
    public int colorlevel = 3;
    public Text timeText;
    int n_taps;
    public Text tapntimes;
    public Text levelText;
    public Button startButton;
    public Button tapButton;
    int tapCount;
    public bool gamePlaying { get; private set; }

    Image tapImage;

    //float timeLeft = 5f;
    /*
    public TapTapController()
    {
        Construct();
    }

    public TapTapController(float timeLeft, int difficulty)
    {
        Construct(timeLeft, difficulty);
    }
    */
    public override void Construct()
    {
        this.timeLeft = BombBehaviour.TIMEFULL;
        this.difficulty = BombBehaviour.EASY;
        this.type = BombBehaviour.LEVELTAP;
    }

    public override void Construct(float timeLeft, int difficulty)
    {
        this.timeLeft = timeLeft;
        this.difficulty = difficulty;
        this.type = BombBehaviour.LEVELTAP;
    }

    // Use this for initialization
    public override void StartGame()
    {
        Debug.Log(gamePlaying);
        if (!gamePlaying)
        {
            tapCount = 0;
            levelText.gameObject.SetActive(true);
            level = 1;
            StartCoroutine(SpawnLevel());
            levelText.text = "Level : " + (level);
        }
    }

    private bool alreadyFed = false;

    public override void FeedBombInfo(BombBehaviour bombFeeder)
    {
        if (alreadyFed || !enabled)
            return;
        Construct(bombFeeder);
        alreadyFed = true;
        StartGame();
    }

    void Start()
    {
        if(gameObject.scene.name != MySceneManager.SCENE_LEVELTAP)
        {
            enabled = false;
            return;
        }
        tapImage = tapButton.GetComponent<Image>();
        gamePlaying = false;
        tapButton.interactable = false;
        levelText.gameObject.SetActive(false);
    }

    IEnumerator SpawnLevel()
    {
        tapCount = 0;
        n_taps = Random.Range(level*3, level *5 );
        tapntimes.gameObject.SetActive(true);
        tapntimes.text = "Tap " + n_taps + " times";
        yield return new WaitForSeconds(startwait);
        tapntimes.text = "START!";
        yield return new WaitForSeconds(1);
        tapntimes.gameObject.SetActive(false);
        gamePlaying = true;
        tapImage.color = Color.green;
        tapButton.interactable = true;
        timeLeft = 5f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gamePlaying)
        {
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, 5f);
            timeText.text = timeLeft.ToString("F2");

            if (timeLeft <= 0f)
            {
                if (isTapCountSame())
                {
                    level++;
                    levelText.text = "Level : " + (level);
                    gamePlaying = false;
                    tapButton.interactable = false;
                    tapImage.color = Color.gray;
                    StartCoroutine(SpawnLevel());
                }
                else
                {
                    gamePlaying = false;
                    tapImage.color= Color.gray;
                    startButton.gameObject.SetActive(true);
                    tapButton.interactable = false;
                    levelText.text = "GAME OVER!";
                    _exploded = true;
                }

            }
        }

    }

    public void OnTapClick()
    {
        tapCount++;
        Debug.Log(tapCount);

    }

    public bool isTapCountSame()
    {
        if (n_taps == tapCount)
            return true;
        else
            return false;
    }
}
