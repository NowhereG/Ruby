using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Button playGame;
    public GameObject settingPanel;
    public AudioSource bgMusic;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)
        {
            if (!bgMusic.isPlaying)
            {
                bgMusic.Play();
            }
            bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        }
    }
    public void OnButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingClick()
    {
        settingPanel.SetActive(true);
        transform.gameObject.SetActive(false);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
