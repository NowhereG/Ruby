using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAmmo : MonoBehaviour
{
    public GameObject partical;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            //×Óµ¯ÊýÁ¿+5
            UIButtetCount.Instance.ChangeBulletCount(5);
            GameObject.Instantiate(partical, transform.position, Quaternion.identity);
            EffectManager.Instance.PlayEffect(0);
            Destroy(gameObject);
            
        }
    }
}
