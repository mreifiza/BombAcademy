using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;

public enum health_status
{
    DIE = 0,
    CRITICAL,
    DAMAGED,
    FULL
}

public class SessionManager : PunBehaviour {

    //private static Dictionary<int, PhotonPlayer> listPlayer;

    //private int myNum;

    private int myId;
    
    private health_status _health;
    /// <summary>
    /// health dari seorang pemain
    /// </summary>
    public int health
    {
        get
        {
            return (int) _health;
        }
    }
    //private float _sessionTime;
    private List<BombBehaviour> _bombCollection;
    private int selectedBombIndex = -1;
    private bool skipUpdateEvaluation = false;

    public MySceneManager sm;

    public TypingController typingBombPrefab;
    public TapTapController tapBombPrefab;

    /// <summary>
    /// 
    /// </summary>
    [PunRPC]
    public void AddBomb(int levelType, int id)
    {
        if (myId != id)
            return;
        BombBehaviour newBomb;
        switch (levelType)
        {
            case BombBehaviour.LEVELTAP:
                {
                    newBomb = Instantiate(tapBombPrefab);
                    break;
                }
            case BombBehaviour.LEVELTYPE:
                {
                    newBomb = Instantiate(typingBombPrefab);
                    break;
                }
            case BombBehaviour.LEVELDRAW:
                {
                    return; //stub
                    //break;
                }
            default: return;
        }
        newBomb.Construct();
        _bombCollection.Add(newBomb);
    }

    /// <summary>
    /// 
    /// </summary>
    [PunRPC]
    public void AddBomb(int levelType, float timeLeft, int difficulty, int id)
    {
        if (myId != id)
            return;
        BombBehaviour newBomb;
        switch (levelType)
        {
            case BombBehaviour.LEVELTAP:
                {
                    newBomb = Instantiate(tapBombPrefab);
                    break;
                }
            case BombBehaviour.LEVELTYPE:
                {
                    newBomb = Instantiate(typingBombPrefab);
                    break;
                }
            case BombBehaviour.LEVELDRAW:
                {
                    return; //stub
                    //break;
                }
            default: return;
        }
        newBomb.Construct();
        _bombCollection.Add(newBomb);
    }

    public IEnumerator SelectBomb(int idx)
    {
        string levelname = "";
        int bombType = _bombCollection[idx].Type;
        switch (bombType)
        {
            case BombBehaviour.LEVELTAP:
                levelname = MySceneManager.SCENE_LEVELTAP;
                break;
            case BombBehaviour.LEVELTYPE:
                levelname = MySceneManager.SCENE_LEVELTYPE;
                break;
        }
        MySceneManager.ChangeLevel(bombType);
        while (!SceneManager.GetSceneByName(levelname).isLoaded)
            yield return 0;
        BombBehaviour bombGUI;
        GameObject g = GameObject.Find("GameController");
        bombGUI = g.GetComponent<BombBehaviour>();
        bombGUI.FeedBombInfo(_bombCollection[idx]);
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Session Manager: Awaken");
        enabled = false;
	}
	
    void OnEnable()
    {
        myId = PhotonNetwork.player.ID;
        _health = health_status.FULL;
        _bombCollection = new List<BombBehaviour>();
    }

	// Update is called once per frame
	void Update () {
        Debug.Log("health = " + _health);
        if (_health == health_status.DIE)
        {
            Debug.Log("die");
            return;
        }
        foreach (BombBehaviour b in _bombCollection)
        {
            if (b.isExploded)
                _health--;
        }
        if (skipUpdateEvaluation)
            return;
        if(selectedBombIndex != -1 && _bombCollection[selectedBombIndex].isFinished)
        {
            skipUpdateEvaluation = true;
            MySceneManager.ChangeLevel(MySceneManager.SCENE_LEVELTHROWBOMB);
        }
        if(_bombCollection.Count == 0)
        {
            AddBomb(UnityEngine.Random.Range(1, 2), myId);
            StartCoroutine(SelectBomb(0));
        }
	}

    private void FinalizeBomb(int idPlayer)
    {
        Dictionary<string, string> bombInfo = _bombCollection[selectedBombIndex].GetInfo();
        float time = System.Convert.ToSingle(bombInfo["timeLeft"]);
        int difficulty = System.Convert.ToInt32(bombInfo["difficulty"]);
        int type = System.Convert.ToInt32(bombInfo["type"]);
        this.photonView.RPC("AddBomb", PhotonTargets.Others, type, time, difficulty, idPlayer);
        selectedBombIndex = -1;
    }
}
