using UnityEngine;
using System.Collections;

public enum BulletType {
    HerroBullet,
    MonsterBullet,
    BossBullet
}

public class Bullet : MonoBehaviour {

    public BulletType type = BulletType.MonsterBullet;

    public float downSpeed = 2f;
    public float upSpeed = 6f;
    public float leftSpeed = 0f;
    public float rightSpeed = 0f;
    public int order = 1;
    public int way = 1;// 1 右， -1 左

    public bool isDestroy = true;
    public int hurtHp = 10;

    private float tempUpSpeed = 6f;

    private float timer = 0f;
    public float timerInterval = 10f;
    public float waitTimeinval = 0f;
    
    private bool isRotate = false;

    private Vector3 speedVector3;

    public int bulletNumber = 1;

    private GameObject player;
    private GameObject targetMonster;

    public Sprite[] skinSprite;
    private SpriteRenderer render;
    private int spriteIndex = 0;

    private float myx = 0f, myy = 0f;
    private float xangle = 0;

    private int num_5_index = 1;

	// Use this for initialization
	void Start () {
        tempUpSpeed = upSpeed;

        if (bulletNumber == 3 || bulletNumber == 5 || bulletNumber == 51)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            render = GetComponent<SpriteRenderer>();
            myx = transform.position.x;
            myy = transform.position.y;
        }

        if (bulletNumber == 5)
        {
            xangle = order == 4 ? 5f : order == 5 ? - 5f : 0;
            transform.Rotate(0, 0, xangle, Space.Self);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (type == BulletType.HerroBullet)
        {
            switch (bulletNumber)
            {
                case 1:
                    PlayerBullet1();
                    break;
                case 2:
                    PlayerBullet2();
                    break;
                case 3:
                    PlayerBullet3();
                    break;
                case 4:
                    PlayerBullet4();
                    break;
                case 5:
                    PlayerBullet5();
                    break;
                case 51:
                    PlayerBullet51();
                    break;
            }

            if (transform.position.y > 3.5 || Mathf.Abs(transform.position.x) > 2.12)
            {
                if(this.tag != "Untagged"){
                    Destroy(this.gameObject);
                }
            }
        }
        else if (type == BulletType.MonsterBullet)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
            if (leftSpeed > 0)
            {
                if(!isRotate){
                    isRotate = true;
                    transform.Rotate(0, 0, -15, Space.Self);
                }
                transform.Translate(Vector3.left * leftSpeed * Time.deltaTime);
            }
            if (rightSpeed > 0)
            {
                if (!isRotate)
                {
                    isRotate = true;
                    transform.Rotate(0, 0, 15, Space.Self);
                }
                transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);
            }
            if (transform.position.y < -3.65 || Mathf.Abs(transform.position.x) > 2.12)
            {
                Destroy(this.gameObject);
            }
        }
        else if (type == BulletType.BossBullet)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
            if (transform.position.y < -3.65 || Mathf.Abs(transform.position.x) > 2.12)
            {
                Destroy(this.gameObject);
            }
        }
        
	}

    // 英雄子弹，1号弹
    void PlayerBullet1()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(order > 3 && order < 6){
            int angle = 10;
            if (xangle >= angle && way == 1)
            {
                way = -1;
            }
            else if (xangle <= -angle && way == -1)
            {
                way = 1;
            }
            xangle = xangle + 5 * way;
            transform.Rotate(0, 0, xangle, Space.Self);
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            return;
        }
        else if(order > 5)
        {
            timer++;
            if (timer > timerInterval)
            {
                waitTimeinval = 0;
                leftSpeed = 0f;
                rightSpeed = 0f;
                timer = 0;
                downSpeed = 1;
                upSpeed = 10f;
            }
            else if (waitTimeinval != 0)
            {
                upSpeed = 0f;
            }
        }

        y = y + (upSpeed - downSpeed) * Time.deltaTime;
        x = x + (rightSpeed - leftSpeed) * Time.deltaTime;
        speedVector3 = new Vector3(x, y, 0);
        transform.position = speedVector3;
    }

    // 英雄子弹，2号弹
    void PlayerBullet2()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        timer++;
        if (timer > timerInterval)
        {
            waitTimeinval = 0;
            leftSpeed = 0f;
            rightSpeed = 0f;
            timer = 0;
            downSpeed = 0;
            upSpeed = tempUpSpeed;
        }
        else if (waitTimeinval != 0)
        {
            upSpeed = 0f;
        }
        
        y = y + (upSpeed - downSpeed) * Time.deltaTime;
        x = x + (rightSpeed - leftSpeed) * Time.deltaTime;
        speedVector3 = new Vector3(x, y, 0);
        transform.position = speedVector3;

    }

    // 英雄子弹，3号弹
    void PlayerBullet3()
    {
        /*float x = transform.position.x;
        float y = transform.position.y;

        if (skinSprite.Length > 0)
        {
            render.sprite = skinSprite[spriteIndex];
            spriteIndex++;
            if (spriteIndex >= skinSprite.Length)
            {
                spriteIndex = 0;
            }
        }*/

        //transform.Rotate(0, 0, 30, Space.Self);
        timerInterval = 6 - GameManage.bulletTempLevel;
        if (timer < timerInterval)
        {
            timer++;
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }
        else
        {
            targetMonster = Utils.GetTargetMonster(transform.position);
            if (targetMonster != null)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    targetMonster.transform.position,
                    Time.deltaTime * upSpeed
                );
                transform.RotateAround(targetMonster.transform.position,
                    Vector3.forward,
                    Time.deltaTime * 50
                );
            }
            else
            {
                transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            }
        }
    }

    // 英雄子弹，4号弹
    void PlayerBullet4()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (waitTimeinval > 0)
        {
            timer++;
            if (timer > timerInterval)
            {
                waitTimeinval = 0;
                leftSpeed = 0f;
                rightSpeed = 0f;
                timer = 0;
                downSpeed = 0;
                upSpeed = tempUpSpeed;
            }
            else if (waitTimeinval != 0)
            {
                upSpeed = 0f;
            }
        }

        y = y + (upSpeed - downSpeed) * Time.deltaTime;
        x = x + (rightSpeed - leftSpeed) * Time.deltaTime;
        speedVector3 = new Vector3(x, y, 0);
        transform.position = speedVector3;
    }

    // 英雄子弹，5号弹
    void PlayerBullet5()
    {
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);

        
    }

    // 5号子弹的尾火
    void PlayerBullet51()
    {
        if (skinSprite.Length > 0)
        {
            render.sprite = skinSprite[spriteIndex];
            spriteIndex++;
            if (spriteIndex >= skinSprite.Length)
            {
                spriteIndex = 0;
            }
        }
    }






    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "magic" && GameManage.currMagic == MagicType.Magic4 && this.tag != "bullet-player")
        {
            Destroy(other.gameObject);
        }

        Debug.Log(this.tag);
        if(!isPlayerBullet()){
            if (other.tag == "monster" || other.tag == "boss")
            {
                Destroy(this.gameObject);
            }
        }
        

        if (other.tag == "Player" && isEnemyBullet())
        {
            other.gameObject.SendMessage("injured");
            Destroy(this.gameObject);
        }

        if (isPlayerBullet() && isBulletMonster())
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }*/

    }

    public void DestorySelf()
    {
        if(bulletNumber == 5){
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                if (transform.GetChild(i).tag == "fiveBulletBoom")
                {
                    FireLetter fire = transform.GetChild(i).GetComponent<FireLetter>();
                    fire.Boom();
                }
            }
        }
        Destroy(this.gameObject);
    }

    public bool isEnemyBullet(){
        return this.tag == "bullet-monster" || this.tag == "bullet-enemy";
    }

    public bool isBulletMonster()
    {
        return this.tag == "bullet-enemy";
    }

    public bool isPlayerBullet()
    {
        return this.tag == "bullet-player";
    }

}
