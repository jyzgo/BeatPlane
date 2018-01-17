using UnityEngine;
using System.Collections;

public class MonsterBullet : MonoBehaviour {

    public float downSpeed = 2f;
    public float upSpeed = 6f;
    public float leftSpeed = 0f;
    public float rightSpeed = 0f;
    public int order = 1;
    public int way = 1;// 1 右， -1 左

    public bool isDestroy = true;
    public bool isAttacked = false;

    private float tempUpSpeed = 6f;

    public float rotaAngle = 0.1f;

    private float timer = 0f;
    public float timerInterval = 14f;
    public float waitTimeinval = 0f;

    private bool isRotate = false;
    private float runtime = 0f;

    private Vector3 speedVector3;

    public int bulletNumber = 1;

    private GameObject player;
    private GameObject targetMonster;

    public Sprite[] skinSprite;
    private SpriteRenderer render;
    private int spriteIndex = 0;

    private float myx = 0f, myy = 0f;

    private int num_5_index = 1;

	void Start () {
	    
        if(bulletNumber == 4){
            transform.Rotate(0, 0, rotaAngle - 90f, Space.Self);
        } 
        else if (bulletNumber == 5)
        {
            transform.Rotate(0, 0, rotaAngle - 90f, Space.Self);
        }
        else if (bulletNumber == 31 || bulletNumber == 32)
        {
            transform.Rotate(0, 0, rotaAngle - 90f, Space.Self);
        }

	}

    void FixedUpdate()
    {
        switch (bulletNumber)
        {
            case 2:
                bullet_2();
                break;
            case 8:
                bullet_8();
                break;
            case 3:
                bullet_3();
                break;
            case 4:
                bullet_4();
                break;
            case 6:
                bullet_6();
                break;
            case 31:
                bullet_31();
                break;
            case 32:
                bullet_32();
                break;
            default:
                bullet_1();
                break;
        }


        if (transform.position.y < -3.65 || Mathf.Abs(transform.position.x) > 2.12)
        {
            Destroy(this.gameObject);
        }
	}

    void bullet_1()
    {
        if(downSpeed > 0){
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }

        if(upSpeed > 0){
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }
        
        if (leftSpeed > 0)
        {
            if (!isRotate)
            {
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
    }

    void bullet_2()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && way == 1)
        {
            way = -1;
            float angle = Utils.ComputeAngle(
                player.transform.position,
                transform.position
            );
            transform.Rotate(0, 0, angle, Space.Self);
        }

        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
    }

    void bullet_3()
    {
        runtime++;
        if (runtime > 60)
        {
            rightSpeed = 0;
            leftSpeed = 0;
            downSpeed = 4f;
        }
        bullet_1();
    }

    void bullet_4()
    {
        rightSpeed = 0;
        leftSpeed = 0;
        downSpeed = 3f;
        bullet_1();
    }

    void bullet_5()
    {
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
    }

    void bullet_6()
    {
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        if(transform.localScale.y < 3){
            transform.localScale += new Vector3(0, 0.1f, 0);
        }
    }

    void bullet_8()
    {
        if (downSpeed > 0)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);            
            if (downSpeed > 2f)
            {
                downSpeed = downSpeed - 0.15f;
            }
        }

        if (leftSpeed > 0)
        {
            if (!isRotate)
            {
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

    void bullet_31(){
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        if (way == 1) // 斜向下
        {
            if (transform.position.y < -2)
            {
                way = -1;
                transform.Rotate(0, 0, 130, Space.Self);
            }
        }

        if (Mathf.Abs(transform.position.y) > 3.65 || Mathf.Abs(transform.position.x) > 2.12)
        {
            Destroy(this.gameObject);
        }
    }

    void bullet_32()
    {
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        if (way == 1) // 斜向下
        {
            if (transform.position.y < -2)
            {
                way = -1;
                transform.Rotate(0, 0, -130, Space.Self);
            }
        }

        if (Mathf.Abs(transform.position.y) > 3.65 || Mathf.Abs(transform.position.x) > 2.12)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 主角的子弹
        if (isAttacked && other.tag == "bullet-player")
        {
            if(isDestroy){
                Destroy(this.gameObject);
            }
        }
    }

}
