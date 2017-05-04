using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypingController : BombBehaviour
{

    public GameObject keyboard, textAppear;
    public Text timeText;

    bool gamePlaying = false;
    //float timeLeft = 5f;

    public GameObject timeBar;

    private class TypingDictionary
    {
        public static string[,] words = {
            {
                "save", "addy", "pear", "bene", "ogma", "mill", "yank", "geom", "shin", "pize",
                "lind", "ymha", "tour", "bure", "fear", "keek", "conj", "sant", "elli", "como",
                "lura", "sent", "gola", "raid", "pain", "dive", "cats", "save", "kure", "hewn",
                "buff", "nono", "leap", "sken", "bine", "gale", "reno", "evil", "horn", "circ",
                "wynn", "pleb", "sura", "alta", "peen", "bilk", "dunt", "kook", "raze", "luca", "coal", "rose"
            },
            {
                "huffy", "tonic", "folio", "dolph", "stool", "crony", "humor", "lords", "yodel", "gales",
                "faker", "alike", "bites", "kazan", "kirov", "sayer", "amber", "footy", "ludie", "rater",
                "hofer", "samia", "libri", "wigan", "sense", "klong", "swiss", "adnah", "doorn", "iyyar",
                "eager", "metol", "drank", "worth", "miser", "paget", "kyoga", "desde", "bayle", "gamma",
                "inure", "hades", "nomad", "ethyl", "biddy", "ether", "lumpy", "payne", "kyros", "dozer", "booze", "sakes"
            },
            {
                "employ", "shacko", "canula", "lesbos", "menial", "trashy", "banjul", "shrove", "scutch", "orange",
                "swiple", "gothic", "strong", "amatol", "kermes", "somnus", "sennar", "vilely", "ghibli", "orchil",
                "bailey", "maungy", "ginned", "flocci", "vernal", "immune", "bagnio", "briton", "aveiro", "cloete",
                "piquet", "febris", "betook", "colors", "coleus", "kendra", "aimful", "jansen", "tipple", "leaker",
                "couple", "diesel", "unhoed", "eduard", "teazle", "blunge", "oxygen", "prearm", "fowler", "tither", "merkel", "melody"
            },
            {
                "kristin", "testify", "moseley", "pompano", "kilobar", "whining", "alcaide", "cutover", "heizing", "myocoel",
                "incluse", "regroup", "georama", "ephesus", "felsite", "analogy", "locoman", "unopted", "outpush", "indoors",
                "slavkov", "strozza", "penaria", "airplot", "sheriff", "amyelia", "unlocal", "smoothy", "castoff", "cockily",
                "unicorn", "behoofs", "maureen", "coronis", "leporid", "tolbert", "peptise", "effulge", "retiree", "untrite",
                "absinth", "whangee", "ciliate", "herbert", "subduct", "unsling", "shingle", "lampion", "unmeted", "lockbox", "glimpse", "cameron"
            }
        };
    }
    public Text bombText;

    public Text levelText;

    public Text textTyped;
    public Text boomText;

    /**
    public TypingController()
    {
        Construct();
    }

    public TypingController(float timeLeft, int difficulty)
    {
        Construct(timeLeft, difficulty);
    }*/

    public override void Construct()
    {
        this.timeLeft = BombBehaviour.TIMEFULL;
        this.difficulty = BombBehaviour.EASY;
        this.type = BombBehaviour.LEVELTYPE;    
    }

    public override void Construct(float timeLeft, int difficulty)
    {
        this.timeLeft = timeLeft;
        this.difficulty = difficulty;
        this.type = BombBehaviour.LEVELTYPE;
    }

    private void Start()
    {
        //dictionary = FindObjectOfType<TypingDictionary>();
        if (gameObject.scene.name != MySceneManager.SCENE_LEVELTYPE)
        {
            enabled = false;
        }
    }

    public override void StartGame()
    {
        if (!gamePlaying)
        {
            gamePlaying = true;
            keyboard.SetActive(true);
            textAppear.SetActive(true);
            levelText.text = "Level " + difficulty;
            boomText.text = "";
            bombText.text = TypingDictionary.words[difficulty, UnityEngine.Random.Range(0, 49)];
        }
    }

    //private TypingController bombToExtract = null;
    private bool alreadyFed = false;

    public override void FeedBombInfo(BombBehaviour bombFeeder)
    {
        if (alreadyFed || !enabled)
            return;
        Construct(bombFeeder);
        alreadyFed = true;
        StartGame();
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
                _exploded = true;
                Transition();
                difficulty = 1;
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

        levelText.text = "Level " + difficulty;
        timeText.text = "5.00";
        timeLeft = 5f;

        bombText.text = "";
        textTyped.text = "";

        keyboard.SetActive(false);
        textAppear.SetActive(false);
    }

    private void UpdateLevel()
    {
        if (difficulty < 4)
            difficulty++;
        
        bombText.text = TypingDictionary.words[difficulty - 1, UnityEngine.Random.Range(0, TypingDictionary.words.GetLength(1))];
        Transition();
    }

    public void EndGame()
    {
        difficulty = 1;
        Transition();
        SceneManager.LoadScene(0);
    }


}
