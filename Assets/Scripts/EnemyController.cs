using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rgd;
    private Vector2 position;
    public int speed = 6;
    public bool isVertical;
    private float time=0;
    private float timeChange=3.0f;
    private int direction = 1;
    //动画组件
    private Animator animator;
    private bool isBroken;

    public ParticleSystem smokeEffect;

    public AudioSource audioSource;
    //0.hit 1.hit 2.fixed
    public AudioClip[] audioClips;
    
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.SetFloat("MoveX", direction);
        //animator.SetBool("IsVertical", isVertical);
        PlayMoveAnimation();
        isBroken = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBroken)
        {
            position = transform.position;
            if (isVertical)
            {
                //垂直移动
                position.y += speed * Time.deltaTime * direction;
            }
            else
            {
                //横向移动
                position.x += speed * Time.deltaTime * direction;
            }
            //每3秒换方向
            time += Time.deltaTime;
            if (time >= timeChange)
            {
                time = 0;
                direction = -direction;
                //animator.SetFloat("MoveX", direction);
                PlayMoveAnimation();
            }
            rgd.MovePosition(position);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //direction = -direction;
        //animator.SetFloat("MoveX", direction);
        //PlayMoveAnimation();
        RubyController ruby = collision.collider.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }

    private void PlayMoveAnimation()
    {
        if (isVertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }

    public void Fix()
    {
        BackGroundMusic.Instance.ChangeEnemyVolume();
        //被修好了
        gameObject.tag = "FixedEnemy";
        smokeEffect.Stop();
        isBroken = false;
        rgd.simulated = false;
        animator.SetTrigger("Fixed");
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (audioSource.volume != 0)
        {
            int randomNum = Random.Range(0, 2);
            audioSource.volume *= 4f;
            audioSource.PlayOneShot(audioClips[randomNum]);
            Invoke("PlayFixedSound", 1f);
            Invoke("ChangeBackVolume", 2f);
        }
    }

    private void PlayFixedSound()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    private void ChangeBackVolume()
    {
        audioSource.volume /= 4f;
    }
}
