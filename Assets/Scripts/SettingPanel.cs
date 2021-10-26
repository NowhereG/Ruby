using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{

    public Toggle musicToggleOpenUI;
    public Toggle musicToggleCloseUI;
    public Toggle musicEffectToggleOpenUI;
    public Toggle musicEffectToggleCloseUI;
    public Slider musicSlider;
    public Slider musicEffectSlider;
    public AudioSource bgMusic;
    public GameObject controlPanel;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)//开启音乐
        {
            musicToggleOpenUI.isOn = true;
            musicSlider.gameObject.SetActive(true);
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
            if (!bgMusic.isPlaying)
            {
                bgMusic.Play();
            }
        }
        else//关闭音乐
        {
            musicToggleCloseUI.isOn = true;
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            musicSlider.gameObject.SetActive(false);
            bgMusic.Stop();
        }

        if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//开启音效
        {
            musicEffectToggleOpenUI.isOn = true;
            musicEffectSlider.gameObject.SetActive(true);
            musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        }
        else//关闭音效
        {
            musicEffectToggleCloseUI.isOn = true;
            musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            musicEffectSlider.gameObject.SetActive(false);
        }
    }
    //背景音乐开关
    public void OnMusicOpen(bool isOn)
    {
        if (isOn)
        {
            //1代表开，0代表关
            PlayerPrefs.SetInt("MusicToggle", 1);
            musicSlider.gameObject.SetActive(true);
            bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
            if (!bgMusic.isPlaying)
            {
                bgMusic.Play();
            }
        }
    }
    public void OnMusicClose(bool isOn)
    {
        if (isOn)
        {
            //1代表开，0代表关
            PlayerPrefs.SetInt("MusicToggle", 0);
            //musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            musicSlider.gameObject.SetActive(false);
            bgMusic.Pause();
        }
    }
    //音效开关
    public void OnMusicEffectOpen(bool isOn)
    {
        if (isOn)
        {
            //1代表开，0代表关
            PlayerPrefs.SetInt("MusicEffectToggle", 1);
            musicEffectSlider.gameObject.SetActive(true);
            musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        }
    }
    public void OnMusicEffectClose(bool isOn)
    {
        if (isOn)
        {
            //1代表开，0代表关
            PlayerPrefs.SetInt("MusicEffectToggle", 0);
            //musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            musicEffectSlider.gameObject.SetActive(false);
        }
    }
    //音乐音量
    public void OnMusicVolumeChange(float value)
    {
        //value：0-1的取值
        PlayerPrefs.SetFloat("MusicVolume", value);
        bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
    //音效音量
    public void OnMusicEffectVolumeChange(float value)
    {
        //value：0-1的取值
        PlayerPrefs.SetFloat("MusicEffectVolume", value);
    }
    public void OnExitClick()
    {
        controlPanel.SetActive(true);
        transform.gameObject.SetActive(false);
    }

}
