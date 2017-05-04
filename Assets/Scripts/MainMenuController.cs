using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject optionScreen;
    public GameObject titleScreen;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = AudioManager.instance.masterVolume;
        musicSlider.value = AudioManager.instance.musicVolume;
        sfxSlider.value = AudioManager.instance.sfxVolume;
    }

    public void OptionPressed()
    {
        titleScreen.SetActive(false);
        optionScreen.SetActive(true);
    }

    public void BackToTitlePressed()
    {
        optionScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioManager.instance.SetMasterVolume(value);
    }

    public void ChangeMusicVolume(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
    }

    public void ChangeSFXVolume(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
    }

    public void ToRoomSelectScene()
    {
        SceneManager.LoadScene(1);
    }
        
}
