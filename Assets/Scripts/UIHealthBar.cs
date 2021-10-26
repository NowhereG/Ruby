using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //��Ҫ�ı��С��ͼƬ
    public Image maskImg;
    //ԭʼͼƬ���
    private float maskSize;
    //�������ģʽ
    public static UIHealthBar instance { get; private set; }

    public bool hasTask;
    public bool isCompletedTask;


    void Awake()
    {
        instance = this;
        //��ȡԭʼ���
        maskSize = maskImg.rectTransform.rect.width;
    }
    /// <summary>
    /// �ı�HealthBar�Ŀ��
    /// </summary>
    /// <param name="fillPercent">��ǰ����ֵռ�������İٷֱ�</param>
    public void ChangeHealthBar(float fillPercent)
    {
        //���հٷֱȸı�ͼƬ���
        maskImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maskSize * fillPercent);
    }

}
