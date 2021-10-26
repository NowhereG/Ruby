using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    private static BackGroundMusic _instance;
    public static BackGroundMusic Instance { get { return _instance; } }
    private AudioSource audioSource;
    AudioSource[] audioSources;
    List<AudioSource> audioSourcesList = new List<AudioSource>();
    private void Awake()
    {
        _instance = this;
        //获取场景所有的AudioSource组件
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource item in audioSources)
        {
            if (item.name != "MusicPlayer")
            {
                audioSourcesList.Add(item);
            }
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        //if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)
        //{
        //    if (!audioSource.isPlaying)
        //    {
        //        audioSource.Play();
        //    }
        //}
        //if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//开启音效
        //{
        //    foreach (AudioSource item in audioSourcesList)
        //    {
        //        if (item.tag == "Enemy")
        //        {
        //            item.volume = (PlayerPrefs.GetFloat("MusicEffectVolume", 1)/( GameObject.FindGameObjectsWithTag("Enemy").Length + 1));
        //        }
        //        else
        //        {
        //            item.volume = PlayerPrefs.GetFloat("MusicEffectVolume", 1);
        //        }
        //    }
        //}
        //else//关闭音效
        //{
        //    foreach (AudioSource item in audioSourcesList)
        //    {
        //        item.volume = 0;
        //    }
        //}
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else//关闭音乐
        {
            audioSource.Pause();
        }

        if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//开启音效
        {
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
        else//关闭音效
        {
            foreach (AudioSource item in audioSourcesList)
            {
                item.volume = 0;
            }
        }
    }
    public void ChangeEnemyVolume()
    {
        foreach (AudioSource item in audioSourcesList)
        {
            if (item.tag == "Enemy" && item.volume!=0)
            {
                item.volume += (item.volume * (1 / 7f));
            }
        }
    }
}
