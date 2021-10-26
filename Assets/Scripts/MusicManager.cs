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
        //��ȡ�������е�AudioSource���
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource item in audioSources)
        {
            if (item.name != "MusicPlayer")
            {
                audioSourcesList.Add(item);
            }
        }

        //if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)//��������
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
        //else//�ر�����
        //{
        //    musicToggleCloseUI.isOn = true;
        //    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        //    musicSlider.gameObject.SetActive(false);
        //    bgMusic.Stop();
        //}

        //if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//������Ч
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
        //else//�ر���Ч
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
        if (PlayerPrefs.GetInt("MusicToggle", 1) == 1)//��������
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
        else//�ر�����
        {
            musicToggleCloseUI.isOn = true;
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            musicSlider.gameObject.SetActive(false);
            bgMusic.Pause();
        }

        if (PlayerPrefs.GetInt("MusicEffectToggle", 1) == 1)//������Ч
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
        else//�ر���Ч
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
    //�������ֿ���
    public void OnMusicOpen(bool isOn)
    {
        if (isOn)
        {
            //1������0�����
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
            //1������0�����
            PlayerPrefs.SetInt("MusicToggle", 0);
            //musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
            musicSlider.gameObject.SetActive(false);
            bgMusic.Pause();
        }
    }
    //��Ч����
    public void OnMusicEffectOpen(bool isOn)
    {
        if (isOn)
        {
            //1������0�����
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
            //1������0�����
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
    //��������
    public void OnMusicVolumeChange(float value)
    {
        //value��0-1��ȡֵ
        PlayerPrefs.SetFloat("MusicVolume", value);
        bgMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
    //��Ч����
    public void OnMusicEffectVolumeChange(float value)
    {
        //value��0-1��ȡֵ
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
    //ÿ�޺�һ�������˾͵�������������޸Ļ����˵�����
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
