using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;

public class MySceneManager : PunBehaviour {

    public static SessionManager sessionManager;
    private static MySceneManager instance;

    private static bool isEnteredPlayMode = false;
    public static bool IsEnteredPlayMode
    {
        get{
            return isEnteredPlayMode;
        }
    }

    public SessionManager _sessionManager;
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_PLAYERINFO = "PLAYER_INFO";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_ROOMWIZARD = "Room";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_WAITINGROOM = "Inside Room";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_LEVELEMPTY = "LEVEL_EMPTY";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_LEVELTHROWBOMB = "LEVEL_THROWBOMB";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_LEVELTAP = "LEVEL_TAP";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_LEVELTYPE = "LEVEL_TYPE";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_LEVELCOLOR = "LEVEL_COLOR";
    /// <summary>
    /// 
    /// </summary>
    public const string SCENE_PERSISTENT = "PersistentScene";
    
    private static string currentlyLoadedNonLevelScene = SCENE_ROOMWIZARD ;
    private static string currentlyLoadedLevelScene = ""; 

    public static string currentLevel
    {
        get
        {
            return currentlyLoadedLevelScene;
        }
    }

    void Start()
    {
        sessionManager = _sessionManager;
    }

    public static void enterPlayMode()
    {
        isEnteredPlayMode = true;
        sessionManager.enabled = true;
    }

    //private string _tempSceneName = ""; //cuma buat digunakan sama method ILoad
    private IEnumerator ILoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //while (!SceneManager.GetSceneByName(sceneName).isLoaded) ;
        if (sceneName.Contains("LEVEL"))
            currentlyLoadedLevelScene = sceneName;
        else
            currentlyLoadedNonLevelScene = sceneName;
        Debug.Log("Current frame: " + Time.frameCount);
        yield return 0;
        Debug.Log("Current frame: " + Time.frameCount + ", trying to set active" + sceneName +"... will it fail?");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    } 

    private static void Load(string sceneName)
    {
        instance.StartCoroutine("ILoad", sceneName);        
    }

    private static void Unload(string sceneName)
    {
        Debug.Log("Unloading scene" + sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
        if (sceneName.Contains("LEVEL"))
            currentlyLoadedLevelScene = "";
        else
            currentlyLoadedNonLevelScene = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScreen(string sceneName)
    {
        Debug.Log(string.Format("unloading " + currentlyLoadedNonLevelScene));
        if (sceneName.Contains("LEVEL") || sceneName.Contains("PLAYER") || sceneName.Contains("Persistent"))
            return;
        Unload(currentlyLoadedNonLevelScene);
        Load(sceneName);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    } 

    /// <summary>
    /// 
    /// </summary>
    public static void InitialLevelSceneLoad()
    {
        Unload(currentlyLoadedNonLevelScene);
        Load(SCENE_PLAYERINFO);
        Load(SCENE_LEVELEMPTY);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(SCENE_LEVELEMPTY));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneLevel"></param>
    public static void ChangeLevel(string sceneLevel)
    {
        Unload(currentlyLoadedLevelScene);
        Load(sceneLevel);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneLevel));
    }

    public static void ChangeLevel(int bombType)
    {
        switch (bombType) { 
            case BombBehaviour.LEVELTAP:
                ChangeLevel(SCENE_LEVELTAP);
                break;
            case BombBehaviour.LEVELTYPE:
                ChangeLevel(SCENE_LEVELTYPE);
                break;
            case BombBehaviour.LEVELDRAW:
                ChangeLevel(SCENE_LEVELTAP);
                break;
        }
    }

    void Awake()
    {
        currentlyLoadedNonLevelScene = SceneManager.GetActiveScene().name;
        instance = this;
    }
}
