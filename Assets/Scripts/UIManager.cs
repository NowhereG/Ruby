using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject settingPanel;
    public Sprite[] musicSprites;
    private bool isOn;
    public Image imgMusic;
    public Sprite[] pauseSprites;
    private bool isPause=false;
    public Image imgPause;
    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicToggle", 1) == 0 && PlayerPrefs.GetInt("MusicEffectToggle", 1) == 0)
        {
           isOn = false;
        }
        else
        {
           isOn = true;
        }
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("MusicToggle", 1) == 0 && PlayerPrefs.GetInt("MusicEffectToggle", 1) == 0)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }
        ChangeMusicSprite();
    }
    public void OnRetryClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnMusicClick()
    {
        isOn = !isOn;
        PlayerPrefs.SetInt("MusicToggle", isOn ? 1 : 0);
        PlayerPrefs.SetInt("MusicEffectToggle", isOn ? 1 : 0);
        ChangeMusicSprite();
    }
    private void ChangeMusicSprite()
    {
        if (isOn)
        {
            imgMusic.sprite = musicSprites[0];
        }
        else
        {
            imgMusic.sprite = musicSprites[1];
        }
    }
    public void OnPauseClick()
    {
        isPause = !isPause;
        //PlayerPrefs.SetInt("MusicToggle", isPause ? 0 : 1);
        //PlayerPrefs.SetInt("MusicEffectToggle", isPause ? 0 : 1);
        if (isPause)
        {
            Time.timeScale = 0;
            imgPause.sprite = pauseSprites[0];//continue
        }
        else
        {
            Time.timeScale = 1;
            imgPause.sprite = pauseSprites[1];
        }
    }
    public void OnSettingClick()
    {
        Time.timeScale = 0;
        settingPanel.SetActive(true);
    }
}
