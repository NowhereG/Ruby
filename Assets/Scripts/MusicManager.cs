using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    //private static MusicManager _instance;
    //public static MusicManager Instance { get { return _instance; } }
    public AudioSource bgMusic;
    public Toggle musicToggleOpenUI;
    public Toggle musicToggleCloseUI;
    public Toggle musicEffectToggleOpenUI;
    public Toggle musicEffectToggleCloseUI;
    public Slider musicSlider;
    public Slider musicEffectSlider;
    AudioSource[] audioSources;
    List<AudioSource> audioSourcesList = new List<AudioSource>();

    //private float EnemyCount=7f;
    //public GameObject controlPanel;

    //private void Awake()
    //{
    //    _instance = this;
    //    if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)
    //    {
    //        if (!bgMusic.isPlaying)
    //        {
    //            bgMusic.Play();
    //        }
    //        bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        //获取场景所有的AudioSource组件
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource item in audioSources)
        {
            if (item.name != "MusicPlayer")
            {
                audioSourcesList.Add(item);
            }
        }

        //if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)//开启音乐
        //{
        //    musicToggleOpenUI.isOn = true;
        //    musicSlider.gameObject.SetActive(true);
        //    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        //    bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        //    if (!bgMusic.isPlaying)
        //    {
        //        bgMusic.Play();
        //    }
        //}
        //else//关闭音乐
        //{
        //    musicToggleCloseUI.isOn = true;
        //    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        //    musicSlider.gameObject.SetActive(false);
        //    bgMusic.Stop();
        //}

        //if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//开启音效
        //{
        //    musicEffectToggleOpenUI.isOn = true;
        //    musicEffectSlider.gameObject.SetActive(true);
        //    musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        //    foreach (AudioSource item in audioSourcesList)
        //    {
        //        if (item.tag == "Enemy")
        //        {

        //            item.volume = (PlayerPrefs.GetFloat("MusicEffectVolume", 1) / (GameObject.FindGameObjectsWithTag("Enemy").Length + 1));
        //        }
        //        else
        //        {
        //            item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        //        }
        //        //item.volume = musicEffectSlider.value;
        //        //item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        //    }
        //}
        //else//关闭音效
        //{
        //    musicEffectToggleCloseUI.isOn = true;
        //    musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        //    musicEffectSlider.gameObject.SetActive(false);
        //    foreach (AudioSource item in audioSourcesList)
        //    {
        //        item.volume = 0;
        //        //item.Stop();
        //    }
        //}
    }
    private void Update()
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
            bgMusic.Pause();
        }

        if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//开启音效
        {
            musicEffectToggleOpenUI.isOn = true;
            musicEffectSlider.gameObject.SetActive(true);
            musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            foreach (AudioSource item in audioSourcesList)
            {
                if (item.tag == "Enemy")
                {

                    item.volume = (PlayerPrefs.GetFloat("MusicEffectVolume", 1) / (GameObject.FindGameObjectsWithTag("Enemy").Length + 1));
                }
                else
                {
                    item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
                }
                //item.volume = musicEffectSlider.value;
                //item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            }
        }
        else//关闭音效
        {
            musicEffectToggleCloseUI.isOn = true;
            musicEffectSlider.value = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            musicEffectSlider.gameObject.SetActive(false);
            foreach (AudioSource item in audioSourcesList)
            {
                item.volume = 0;
                //item.Stop();
            }
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
            foreach (AudioSource item in audioSourcesList)
            {
                //item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
                //item.volume = musicEffectSlider.value;
                //item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
                if (item.tag == "Enemy")
                {
                    item.volume = (PlayerPrefs.GetFloat("MusicEffectVolume", 1) / (GameObject.FindGameObjectsWithTag("Enemy").Length + 1));
                }
                else
                {
                    item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
                }
            }
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
            foreach (AudioSource item in audioSourcesList)
            {
                item.volume = 0;
                //item.Stop();
            }
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
        foreach (AudioSource item in audioSourcesList)
        {
            if (item.tag == "Enemy")
            {
                item.volume = (PlayerPrefs.GetFloat("MusicEffectVolume", 1) / (GameObject.FindGameObjectsWithTag("Enemy").Length + 1));
            }
            else
            {
                item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
            }
            
        }
    }
    public void OnExitClick()
    {
        Time.timeScale = 1;
        transform.gameObject.SetActive(false);
    }


    //private void GetAudioSourceList()
    //{
    //    AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();
    //    foreach (AudioSource item in audioSources)
    //    {
    //        if (item.name != "MusicPlayer")
    //        {
    //            item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
    //        }
    //    }
    //}
    //每修好一个机器人就调用这个方法，修改机器人的音量
    //public void SelectEnemyCount()
    //{
    //    if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
    //    {
    //        EnemyCount--;
    //    }
    //    foreach (AudioSource item in audioSourcesList)
    //    {
    //        if (item.tag == "Enemy")
    //        {
    //            item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1) * 1 / EnemyCount;
    //        }
    //    }
    //}
}
