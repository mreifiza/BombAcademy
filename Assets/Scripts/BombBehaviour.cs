using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon;


/// <summary>
/// Kelas dasar untuk semua level game
/// </summary>
public abstract class BombBehaviour : PunBehaviour
{
    protected float timeLeft;
    protected int type;
    protected int difficulty;

    public int Type
    {
        get
        {
            return type;
        }
    }

    public const int EASY = 0;
    public const int MEDIUM = 1;
    public const int HARD = 2;
    public const int HARDEST = 3;

    public const float TIMEFULL = 5.0f;

    public const int LEVELTYPE = 1;
    public const int LEVELTAP = 2;
    public const int LEVELDRAW = 3;

    protected bool _isFinished = false;

    /// <summary>
    /// 
    /// </summary>
    public bool isFinished
    {
        get
        {
            return _isFinished;
        }
    }

    protected bool _exploded = false;
    public bool isExploded
    {
        get
        {
            return _exploded;
        }
    }

    /// <summary>
    /// Buat sebuah bomb baru yang timenya full, dan difficulty nya dimulai dari termudah.
    /// </summary>
    abstract public void Construct();

    /// <summary>
    /// Buat sebuah bomb baru yang waktunya tinggal sisa _timeleft_, dan kesulitan ada di level _difficulty_
    /// </summary>
    /// <param name="timeLeft"> waktu timer bomb</param>
    /// <param name="difficulty"> kesulitan level bomb. Direkomendasikan untuk memakai opsi BombBehaviour.[EASY|MEDIUM|HARD|HARDEST]</param>
    abstract public void Construct(float timeLeft, int difficulty);

    /// <summary>
    /// 
    /// </summary>
    abstract public void StartGame();

    /// <summary>
    /// 
    /// </summary>
    abstract public void FeedBombInfo(BombBehaviour bombFeeder);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="another"></param>
    public void Construct(BombBehaviour another)
    {
        timeLeft = another.timeLeft;
        type = another.type;
        difficulty = another.difficulty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetInfo()
    {
        Dictionary<string, string> ret = new Dictionary<string, string>();
        ret.Add("type", type.ToString());
        ret.Add("timeLeft", timeLeft.ToString());
        ret.Add("difficulty", difficulty.ToString());
        return ret;
    }

    // Update is called once per frame
    void Update()
    {

    }
}



