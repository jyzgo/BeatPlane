using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public BossType bossType;
    public int bossHead = 1;

    public int frame = 15;

    public float left = 0;
    public float right = 0;
    public float up = 0;
    public float down = 0;

    public float x = 0; // boss出现在屏幕中的x坐标
    public float y = 0; // boss出现在屏幕中的y坐标

    private int hway = 1;
    private int wway = 1;

    private int timer = 0;
    private int step = 1;
    

    private Vector3 b_position;

    void Start()
    {
        b_position = transform.position;
    }

    void FixedUpdate()
    {
        switch (bossType)
        {
            case BossType.Boss_1:
                break;
            case BossType.Boss_2:
                break;
            case BossType.Boss_3:
                Boss_3();
                break;
            case BossType.Boss_4:
                break;
            case BossType.Boss_5:
                break;
            case BossType.Boss_6:
                break;
            case BossType.Boss_7:
                break;
            case BossType.Boss_8:
                break;
            case BossType.Boss_9:
                break;
            default:
                break;
        }
    }

    public void Boss_3()
    {
        timer++;
        float temp1 = 0f, temp2 = 0f;
        if (step == 1)
        {// 右下
            if (timer > frame)
            {
                timer = 0;
                step = 2;
            }            
            move(temp1, down, temp2, right);
        }
        if (step == 2)
        {// 右上
            if (timer > frame)
            {
                timer = 0;
                step = 3;
            } 
            move(up, temp1, temp2, right);
        }
        if (step == 3)
        {// 左上
            if (timer > frame)
            {
                timer = 0;
                step = 4;
            }
            move(up, temp1, left, temp2);
        }
        if (step == 4)
        {// 左下
            if (timer > frame)
            {
                timer = 0;
                step = 5;
            }
            move(temp1, down, left, temp2);
        }
        if (step == 5)
        {// 左下
            if (timer > frame)
            {
                timer = 0;
                step = 6;
            }
            move(temp1, down, left, temp2);
        }
        if (step == 6)
        {// 左上
            if (timer > frame)
            {
                timer = 0;
                step = 7;
            }
            move(up, temp1, left, temp2);
        }
        if (step == 7)
        {// 右上
            if (timer > frame)
            {
                timer = 0;
                step = 8;
            }
            move(up, temp1, temp2, right);
        }
        if (step == 8)
        {// 右下
            if (timer > frame)
            {
                timer = 0;
                step = 1;
                transform.position = new Vector3(0, b_position.y, b_position.z);
            }
            move(temp1, down, temp2, right);
        }
       
    }

    void move(float up, float down, float left, float right)
    {
        if(up > 0){
            transform.Translate(Vector3.up * up * Time.deltaTime);
        }
        if (down > 0)
        {
            transform.Translate(Vector3.down * down * Time.deltaTime);
        }
        if (left > 0)
        {
            transform.Translate(Vector3.left * left * Time.deltaTime);
        }
        if (right > 0)
        {
            transform.Translate(Vector3.right * right * Time.deltaTime);
        }
    }

    public void injured()
    {
        GameManage.bossHead--;
        Destroy(this.gameObject);
        if (GameManage.bossHead == 0)
        {
            GameManage.isOutBoss = false;
        }
    }
}
