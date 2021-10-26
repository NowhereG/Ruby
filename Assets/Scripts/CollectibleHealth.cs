using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject partical;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null)
        {
            //当当前ruby血量小于满血时加血
            if (rubyController.CurrentHealth < rubyController.MaxHealth)
            {
                GameObject.Instantiate(partical , transform.position, Quaternion.identity);
                //其他游戏物体触发会提示没有接收者
                collision.SendMessage("ChangeHealth", 1);
                Destroy(gameObject);
                rubyController.PlayRubyMusic(audioClip);
            }
            
        }
        
    }
}
