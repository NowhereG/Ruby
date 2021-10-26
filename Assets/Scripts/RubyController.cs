using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    //刚体组件
    private Rigidbody2D rgd;
    private Vector2 position;
    //速度
    public int speed = 3;
    private float h;
    private float v;
    //最大血量
    private int maxHealth = 5;
    //当前血量
    private int currentHealth;
    //是否处于无敌状态
    private bool isInvincible;
    //无敌时间
    private float invincibleTime=2.5f;
    //已经经过的时间
    private float time;
    //受伤需要停留0.5秒,不能移动
    private float hurtTime;
    private bool isHurt;
    //获取动画组件
    private Animator animator;
    //ruby朝向
    private Vector2 lookDirection;
    private Vector2 move;
    //子弹预制体
    public GameObject bulletPrefab;
    public int CurrentHealth { get { return currentHealth; } }
    public int MaxHealth { get { return maxHealth; } }
    //获取UIHealthBar对象
    //public UIHealthBar healthBar;
    //获取射线碰撞信息
    RaycastHit2D hit;
    //控制攻击，受伤音效
    public AudioSource audioSource;
    public AudioClip playerHit;
    public AudioClip playerAttack;
    //走路音效AudioSource
    public AudioSource walkAudioSorce;
    public AudioClip playerWalk;

    private Vector3 bornPosition;

    private bool isLaunch;

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        lookDirection = new Vector2(0, -1);
        //audioSource = GetComponent<AudioSource>();
        bornPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt&&!isLaunch)
        {
            position = transform.position;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            move = new Vector2(h, v);
            if (!Mathf.Approximately(h, 0) || !Mathf.Approximately(v, 0))
            {
                lookDirection = move;
                lookDirection.Normalize();
                if (!walkAudioSorce.isPlaying)
                {
                    walkAudioSorce.clip = playerWalk;
                    walkAudioSorce.Play();
                }
            }
            else
            {
                if (walkAudioSorce.isPlaying)
                {
                    walkAudioSorce.Stop();
                }
            }
            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);

            //position.x += h * speed * Time.deltaTime;
            //position.y += v * speed * Time.deltaTime;
            position += speed * move * Time.deltaTime;
            rgd.MovePosition(position);
        }
        
        //无敌状态持续2秒
        if (isInvincible)
        {
            
            time -= Time.deltaTime;
            //经过两秒
            if (time <= 0)
            {
                //不在无敌状态
                isInvincible = false;
            }
        }
        //发射子弹
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UIButtetCount.Instance.canLaunch)
            {
                Launch();
            }
        }

        //与NPC对话
        if (Input.GetKeyDown(KeyCode.T))
        {
            hit = Physics2D.Raycast(rgd.position + Vector2.up*0.2f, lookDirection, 1.5f,LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCDialog npcDialog = hit.collider.GetComponent<NPCDialog>();
                npcDialog.ShowDialog();

                //Debug.Log("当前射线碰撞到的游戏物体是："+hit.collider.gameObject);
                //Debug.Log("Ruby正在和Jambi交谈");
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        //如果是伤害
        if (amount < 0)
        {
            //处于无敌状态
            if (isInvincible)
            {
                return;
            }
            //不处于无敌状态
            isHurt = true;
            rgd.Sleep();
            //StartCoroutine("HurtTime");
            Invoke("HurtTime", 0.5f);
            isInvincible = true;
            time = invincibleTime;
            PlayRubyMusic(playerHit);
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp( currentHealth + amount, 0, maxHealth);
        //Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            Invoke("ReLife", 0.6f);
            //ReLife();
        }
        //healthBar.ChangeHealthBar(currentHealth / (float)maxHealth);
        UIHealthBar.instance.ChangeHealthBar(currentHealth / (float)maxHealth);
    }


    public void ReLife()
    {
        transform.position = bornPosition;
        currentHealth = MaxHealth;
        UIHealthBar.instance.ChangeHealthBar(currentHealth / (float)maxHealth);
    }
    private void Launch()
    {
        if (UIHealthBar.instance.hasTask)
        {
            isLaunch = true;
            rgd.Sleep();
            Invoke("LaunchTime", 0.517f);
            GameObject go = Instantiate(bulletPrefab, rgd.position + Vector2.up * 0.5f, Quaternion.identity);
            BulletController bullet = go.GetComponent<BulletController>();
            bullet.Launch(lookDirection, 300);
            //子弹数量-1
            UIButtetCount.Instance.ChangeBulletCount(-1);
            animator.SetTrigger("Launch");
            PlayRubyMusic(playerAttack);
        }
        
    }

    public void PlayRubyMusic(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    //IEnumerable HurtTime()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    isHurt = false;
    //}
    private void HurtTime()
    {
        isHurt = false;
        rgd.WakeUp();
    }
    private void LaunchTime()
    {
        isLaunch = false;
        rgd.WakeUp();
    }
}
