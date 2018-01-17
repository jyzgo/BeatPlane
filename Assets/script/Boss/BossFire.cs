using UnityEngine;
using System.Collections;

public class BossFire : MonoBehaviour {

    public GameObject[] bullet; // 子弹集

    public int[] wait = {50}; // 等待发射时间

    private int[] runtimer = { 0 }; // 已运行时间

    public int[] fireType = { 0 };

    // 子弹开关
    public bool[] fireCut = {false};

    // 子弹移动速度
    public float[] moveSpeed = { 0.5f };

    // 子弹波数
    public int[] fireWave = { 0 };
    private int[] fireCurrWave = { 0 };

    // 速率
    public int[] rate = { 1 };
    private int[] rateIndex = { 0 };

    // 持续时间
    public int[] durationTime = { 0 };
    private int[] currDuraTime = { 0 };
    // 间隔时间
    public int[] intervalTime = { 0 };
    private int[] currInvalTime = { 0 };
    // 发射状态 0 等待状态，1发射状态，2间隔状态
    private int[] fireStatus = { 0 };

    private BulletBoss bossBullet;
    private float xangle = 90f;
    private int way = 1;

    void Start()
    {
        runtimer = new int[wait.Length];
        rateIndex = new int[rate.Length];
        fireCurrWave = new int[fireWave.Length];
        currDuraTime = new int[durationTime.Length];
        currInvalTime = new int[intervalTime.Length];
        fireStatus = new int[wait.Length];
    }

    void FixedUpdate()
    {
        for (int i = 0, k = fireType.Length; i < k; i++)
        {
            // 延迟发射时间，，优先级最高            
            if (!fireCut[i])
            {
                continue;
            }

            // 延迟发射时间，优先级其次           
            if (runtimer[i] < wait[i])
            {
                runtimer[i]++;
                continue;
            }

            // 发射波数
            if (fireWave[i] != 0 && fireCurrWave[i] >= fireWave[i])
            {
                continue;
            }
            if (fireStatus[i] == 0)
            {
                fireStatus[i] = 1;
            }

            // 间隔期间、等待时间
            if (fireStatus[i] == 2)
            {
                if (currInvalTime[i] < intervalTime[i])
                {
                    currInvalTime[i]++;
                    continue;
                }
                else
                {
                    currInvalTime[i] = 0;
                    fireStatus[i] = 1;
                }
            }

            // 发射期间，持续时间
            if (fireStatus[i] == 1)
            {
                if (currDuraTime[i] < durationTime[i])
                {
                    currDuraTime[i]++;
                }
                else
                {
                    currDuraTime[i] = 0;
                    fireStatus[i] = 2;
                }
            }
            
            fire(i);
            fireCurrWave[i]++;
        }
    }

    public void fire(int index)
    {
        rateIndex[index]++;
        if (rateIndex[index]%rate[index] == 0)
        {
            Invoke("fire_" + fireType[index], 0);
        }            
    }

    public void fire_0()
    {
        GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
        bossBullet = bullet[0].GetComponent<BulletBoss>();
        bossBullet.moveSpeed = moveSpeed[0];
        bossBullet.btype = 0;
    }

    public void fire_1()
    {
        GameObject.Instantiate(bullet[1], transform.position, Quaternion.identity);
        bossBullet = bullet[1].GetComponent<BulletBoss>();
        bossBullet.moveSpeed = moveSpeed[1];
        bossBullet.btype = 1;
        if (xangle >= 135 && way == 1)
        {
            way = -1;
        }
        else if (xangle <= 45 && way == -1)
        {
            way = 1;
        }
        xangle = xangle + 15 * way;
        bossBullet.angle = xangle;
    }

    public void fire_2()
    {
        GameObject.Instantiate(bullet[2], transform.position, Quaternion.identity);
        bossBullet = bullet[2].GetComponent<BulletBoss>();
        bossBullet.moveSpeed = moveSpeed[2];
        bossBullet.btype = 2;
        if (xangle >= 135 && way == 1)
        {
            way = -1;
        }
        else if (xangle <= 45 && way == -1)
        {
            way = 1;
        }
        xangle = xangle + 15 * way;
        bossBullet.angle = xangle;
    }

    public void fire_3()
    {
        GameObject.Instantiate(bullet[3], transform.position, Quaternion.identity);
        bossBullet = bullet[3].GetComponent<BulletBoss>();
        bossBullet.moveSpeed = moveSpeed[3];
        bossBullet.btype = 3;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float angle = Utils.ComputeAngle(                
                player.transform.position,
                transform.parent.transform.position
            );
            bossBullet.angle = angle;
        }
        
    }

    public void fire_4()
    {
        GameObject.Instantiate(bullet[4], transform.position, Quaternion.identity);
        bossBullet = bullet[4].GetComponent<BulletBoss>();
        bossBullet.moveSpeed = moveSpeed[4];
        bossBullet.btype = 4;
    }

    public void fire_5()
    {
        GameObject.Instantiate(bullet[5], transform.position, Quaternion.identity);
    }

    public void fire_6()
    {
        GameObject.Instantiate(bullet[6], transform.position, Quaternion.identity);
    }

    public void fire_7()
    {
        GameObject.Instantiate(bullet[7], transform.position, Quaternion.identity);
    }

    public void fire_8()
    {
        GameObject.Instantiate(bullet[8], transform.position, Quaternion.identity);
    }

    public void fire_9()
    {
        GameObject.Instantiate(bullet[9], transform.position, Quaternion.identity);
    }


    void setFireCut(int index, bool flag)
    {
        for (int i = 0, k = fireCut.Length; i < k; i++)
        {
            if (index == i)
            {
                fireCut[i] = flag;
            }
            else
            {
                fireCut[i] = false;
            }
        }
    }

}
