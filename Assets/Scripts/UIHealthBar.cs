using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //需要改变大小的图片
    public Image maskImg;
    //原始图片宽度
    private float maskSize;
    //单例设计模式
    public static UIHealthBar instance { get; private set; }

    public bool hasTask;
    public bool isCompletedTask;


    void Awake()
    {
        instance = this;
        //获取原始宽度
        maskSize = maskImg.rectTransform.rect.width;
    }
    /// <summary>
    /// 改变HealthBar的宽度
    /// </summary>
    /// <param name="fillPercent">当前生命值占总生命的百分比</param>
    public void ChangeHealthBar(float fillPercent)
    {
        //按照百分比改变图片宽度
        maskImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maskSize * fillPercent);
    }

}
