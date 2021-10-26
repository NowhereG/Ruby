using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager Instance { get { return _instance; } }
    private AudioSource audioSource;
    //音效list 0：收集音效
    public List<AudioClip> audioClips = new List<AudioClip>();
    private void Start()
    {
        _instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayEffect(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
}
