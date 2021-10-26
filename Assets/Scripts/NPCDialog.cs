using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialog : MonoBehaviour
{
    public GameObject dialog;
    private float displayTime = 4f;
    private float time;

    public Text dialogText;

    private AudioSource audioSource;
    private bool hasPlayed;
    // Start is called before the first frame update
    void Start()
    {
        time = -1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                dialog.SetActive(false);
            }
        }
    }

    public void ShowDialog()
    {
        //GameObject gameObject1 = GameObject.FindGameObjectWithTag("Enemy");
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            dialogText.text = "Ruby，谢谢你帮我修好了机器人！";
            if (!hasPlayed)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
        //if (UIHealthBar.instance.isCompletedTask)
        //{
            
        //}
        time = displayTime;
        dialog.SetActive(true);
        UIHealthBar.instance.hasTask = true;
        
    }
}
