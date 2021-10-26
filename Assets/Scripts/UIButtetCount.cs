using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtetCount : MonoBehaviour
{
    private static UIButtetCount _instance;
    public static UIButtetCount Instance { get { return _instance; } }
    public Text bulletCountText;
    public int bulletCount = 99;
    public bool canLaunch = true;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        bulletCountText.text = "x " + bulletCount;
    }

    public void ChangeBulletCount(int amount)
    {
        bulletCount += amount;
        bulletCountText.text = "x " + bulletCount;
        if (bulletCount <= 0)
        {
            canLaunch = false;
        }
        else
        {
            canLaunch = true;
        }
    }
}
