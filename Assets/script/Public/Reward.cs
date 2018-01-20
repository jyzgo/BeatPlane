using UnityEngine;
using System.Collections;

public enum RewardType
{
    AddHp,
    BulletNum1,
    BulletNum2,
    BulletNum3,
    BulletNum4,
    BulletNum5,
    Magic1,
    Magic2,
    Magic3,
    Magic4,
    Magic5,
    AddScore,
    AddBulletLevel,
    AddSafe,
    AddGem
}

public class Reward : MonoBehaviour {

    public RewardType type = RewardType.BulletNum1;

   // public GameObject tipobj; // 提示效果

    private PlayerController controller;

    private int way = -1; // 移动方向：-1向下，1向上
    private int deadWay = 1; //死亡方向： -1向下，1向上
    private bool isleft = true;
    private float speed = 0.5f;
    private float leftSpeed = 1f;
    private float rightSpeed = 1f;
    private float tempSpeed = 1f;

    private float timer = 0f;
    private float timeInterval = 60f;

    public void setWay(int w)
    {
        this.way = w;
        deadWay = -1 * way;
    }

    public void setSpeed(float s)
    {
        this.speed = s;
    }

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer ++;
        if(timer > timeInterval){
            timer = 0;
            isleft = isleft ? false : true;
        }

        if (transform.position.x > 2.00)
        {
            timer = 0;
            isleft = true;
        }
        else if (transform.position.x < -2.00)
        {
            timer = 0;
            isleft = false;
        }

        if (isleft)
        {
            transform.Translate(Vector3.left * leftSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * rightSpeed * Time.deltaTime);
        }

        if (way == -1)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (deadWay == 1 && transform.position.y < -3.4)
            {
                way = 1;
            }
        }
        else if (way == 1)
        {
            transform.Translate(Vector3.up * (speed + 0.3f) * Time.deltaTime);
            if (deadWay == -1 && transform.position.y > 3.4)
            {
                way = -1;
            }
        }


        if (deadWay == 1)
        {
            if (transform.position.y > 3.65)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (transform.position.y < -3.65)
            {
                Destroy(this.gameObject);
            }
        }
        

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            switch (type)
            {
                case RewardType.AddHp:
                    GameManage.playerHp = 5;
                    controller.setHpSprite();
                    break;
                case RewardType.BulletNum1:
                    GameManage.ChangeBulletNumber(1);
                    break;
                case RewardType.BulletNum2:
                    GameManage.ChangeBulletNumber(2);
                    break;
                case RewardType.BulletNum3:
                    GameManage.ChangeBulletNumber(3);
                    break;
                case RewardType.BulletNum4:
                    GameManage.ChangeBulletNumber(4);
                    break;
                case RewardType.BulletNum5:
                    GameManage.ChangeBulletNumber(5);
                    break;
                case RewardType.Magic1:
                    GetMagic(MagicType.Magic1);
                    break;
                case RewardType.Magic2:
                    GetMagic(MagicType.Magic2);
                    break;
                case RewardType.Magic3:
                    GetMagic(MagicType.Magic3);
                    break;
                case RewardType.Magic4:
                    GetMagic(MagicType.Magic4);
                    break;
                case RewardType.Magic5:
                    GetMagic(MagicType.Magic5);
                    break;
                case RewardType.AddBulletLevel:
                    setBulletLevel();
                    break;
                case RewardType.AddSafe:
                    SetPlayerSafe(other.gameObject);
                    break;
                case RewardType.AddScore:
                    AddScore();
                    break;
                case RewardType.AddGem:
                    AddGem();
                    break;
                default:
                    break;
            }
            DestoryMy();
        }
    }


    void DestoryMy()
    {
        //if (tipobj != null)
        //{
        //    Instantiate(tipobj, transform.position, Quaternion.identity);
        //}        
        Destroy(this.gameObject);
    }

    void GetMagic(MagicType type)
    {
        GameManage.ChangeMagicNumber(type);
        controller.setMagicText();
    }

    void setBulletLevel()
    {
        if (GameManage.bulletLevel < 5)
        {
            GameManage.bulletLevel++;
        }
    }

    void AddScore()
    {
        controller.addScore(1000);
    }

    void AddGem()
    {
        GameManage.gemNum = GameManage.gemNum + 100;
    }

    void SetPlayerSafe(GameObject player)
    {
        GameManage.playerSafe++;
        ElementEvent.current.IssueSafe();
        controller.SetSafeText();
    }
}
