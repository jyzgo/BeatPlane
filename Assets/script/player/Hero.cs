using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public int hp = 50;
    public GameObject explosion;
    public Sprite[] skinSprite;
    private bool isdeed = false;

    private bool isSafe = true;
    private float upspeed = 2;

    private float colorAlpha = 0;

    private SpriteRenderer render;

    private PlayerController controller;

    public AudioClip[] audioClip;
    private AudioSource audio;
    private int audioTimer = 0;

    public GameObject safeObj;
    public GameObject injuredObj;
    private bool isSafeStatus = false;
    private int safeTimer = 0;

    // 
    public GameObject[] rewards;

    void Start()
    {
        //audio = GetComponent<AudioSource>();
        controller = FindObjectOfType<PlayerController>();
        render = GetComponent <SpriteRenderer>();
        SetSafeStatus(60);
    }

    void FixedUpdate()
    {
        /*if (GameManage.level_up_sound == false)
        {
            if (audioTimer == 4)
            {
                audio.clip = audioClip[GameManage.bulletNumber - 1];
                audio.Play();
                audioTimer = 0;
            }
            audioTimer++;            
        }*/
        
        if (isSafeStatus)
        {
            safeTimer--;
            if(safeTimer <= 0){
                isSafeStatus = false;
                safeObj.SetActive(false);
                injuredObj.SetActive(false);
                GameManage.isSafeIng = false;
            }            
        }
        if (GameManage.bulletLevel - 1 < skinSprite.Length)
        {
            render.sprite = skinSprite[GameManage.bulletLevel - 1];
        }        

        if (isSafe && transform.position.y < -2.5)
        {
            transform.Translate(Vector3.up * upspeed * Time.deltaTime);
        }
        else
        {
            isSafe = false;
        }

        if (colorAlpha > 0)
        {
            colorAlpha -= 0.05f;
            render.color = Color.Lerp(Color.white, Color.red, colorAlpha);
        }
        
    }

    private void PlaneDie()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        // 死亡后生产子弹
        GameObject rewardObj = rewards[GameManage.bulletNumber];
        Instantiate(rewardObj, transform.position, transform.rotation);
        Reward re = null;
        if (rewardObj != null)
        {
            re = rewardObj.GetComponent<Reward>();
            re.setWay(1);
        }
        if(GameManage.bulletLevel > 1){
            Instantiate(rewards[0], transform.position, transform.rotation);
            if (rewards[0] != null)
            {
                re = rewards[0].GetComponent<Reward>();
                re.setWay(1);
                re.setSpeed(0.4f);
            }
        }
        re = null;
    }

    public void SetSafeStatus(int timer)
    {
        isSafeStatus = true;
        safeTimer = timer;
        if(timer < 100){
            injuredObj.SetActive(true);
        }
        else
        {
            safeObj.SetActive(true);
        }
        GameManage.isSafeIng = true;
    }

    public void Injured()
    {
        if (isSafe || isSafeStatus)
        {
            return;
        }
        hp -= 10;
        SetSafeStatus(50);
        GameManage.playerHp = hp / 10;
        controller.setHpSprite();
        if (hp <= 0 && !isdeed)
        {
            isdeed = true;
            PlaneDie();
            Destroy(this.gameObject);
            GameManage.InitPlayerBulletNumber();
        }
        else
        {
            colorAlpha = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isSafeStatus)
        {
            return;
        }
        if (other.tag == "boss")
        {
            Injured();
        }

        if (other.tag == "monster")
        {
            Injured();
        }

        if (other.tag == "bullet-monster")
        {
            Injured();
            Destroy(other.gameObject);
        }

        if (other.tag == "bullet-boss")
        {
            Injured();
            Destroy(other.gameObject);
        }
    }

}
