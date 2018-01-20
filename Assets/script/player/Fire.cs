using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public float upSpeed = 0f;
    public float downSpeed = 0f;

    public bool isshot = true;
    public int type = 0;
    public int bulletNumber = 1;
    public string outLevel = "12345";
    public GameObject bullet;
    private AudioSource audio = null;
    private bool getSound = false;

    public float[] rota = {0.8f, 0.7f, 0.6f, 0.5f, 0.4f};

    public int order = 1;

    // Use this for initialization
    IEnumerator Start()
    {
        if (!getSound)
        {
            audio = GetComponent<AudioSource>();
            getSound = true;
        }

        int bnum = 1, blevel = 1;
        while (true)
        {
            bnum = GameManage.bulletNumber;
            blevel = GameManage.bulletLevel;
            if (GameManage.isMagicCasting)
            {
                bnum = GameManage.bulletTempNumber;
                blevel = GameManage.bulletTempLevel;
            }

            if (bnum == bulletNumber)
            {
                if (isOkFire(blevel))
                {
                    fire();
                }
            }

            yield return new WaitForSeconds(getRate());
        }        
    }

    bool isOkFire(int lev)
    {
        return outLevel.IndexOf("" + lev) != -1;
    }

    void Update()
    {
        
    }

    public void fire()
    {

     //   bulletNumber = GameManage.bulletNumber;
        
        if (isshot)
        {
            GameObject go = GameObject.Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Bullet bt = go.GetComponent<Bullet>();
            bt.order = order;
            bt.bulletNumber = bulletNumber;

            // 1号子弹
            if (bulletNumber == 1)
            {
                fireNumber1(bt, go);
            }

            // 2号子弹
            if (bulletNumber == 2)
            {
                fireNumber2(bt, go);
            }

            // 3号子弹
            if (bulletNumber == 3)
            {
                fireNumber3(bt, go);                              
            }

            //4号子弹
            if(bulletNumber == 4){
                fireNumber4(bt, go);   
            }

            // 5号子弹
            if (bulletNumber == 5)
            {
                fireNumber5(bt, go);   
            }
        }
    }

    public void fireNumber1(Bullet bt, GameObject go)
    {
        bt.upSpeed = upSpeed;
        if (order == 4)
        {
            bt.way = 1;
        }
        else if (order == 5)
        {
            bt.way = -1;
        }
    }

    public void fireNumber2(Bullet bt, GameObject go)
    {

    }

    public void fireNumber3(Bullet bt, GameObject go)
    {
        bt.upSpeed = upSpeed;
        bt.downSpeed = downSpeed;
        if (GameManage.bulletLevel == 1)
        {
            if (order == 1)
            {
                go.transform.Rotate(0, 0, 0, Space.Self);
            }
            else if (order == 2)
            {
                go.transform.Rotate(0, 0, 30, Space.Self);
            }
            else if (order == 3)
            {
                go.transform.Rotate(0, 0, -30, Space.Self);
            }
        }
        else if (GameManage.bulletLevel == 2)
        {
            if (order == 2)
            {
                go.transform.Rotate(0, 0, 10, Space.Self);
            }
            else if (order == 3)
            {
                go.transform.Rotate(0, 0, -10, Space.Self);
            }
            else if (order == 4)
            {
                go.transform.Rotate(0, 0, 45, Space.Self);
            }
            else if (order == 5)
            {
                go.transform.Rotate(0, 0, -45, Space.Self);
            }
        }
        else if (GameManage.bulletLevel >= 3)
        {
            if (order == 1)
            {
                go.transform.Rotate(0, 0, 0, Space.Self);
            }
            else if (order == 2)
            {
                go.transform.Rotate(0, 0, 45, Space.Self);
            }
            else if (order == 3)
            {
                go.transform.Rotate(0, 0, -45, Space.Self);
            }
            else if (order == 4)
            {
                go.transform.Rotate(0, 0, 90, Space.Self);
            }
            else if (order == 5)
            {
                go.transform.Rotate(0, 0, -90, Space.Self);
            }

        }
    }

    public void fireNumber4(Bullet bt, GameObject go)
    {
        bt.upSpeed = upSpeed;
        bt.downSpeed = downSpeed;
        if (order == 2)
        {
            bt.downSpeed = 0.2f;
            bt.waitTimeinval = 1f;
        }
    }

    public void fireNumber5(Bullet bt, GameObject go)
    {
        bt.upSpeed = upSpeed;
        bt.downSpeed = downSpeed;
        if (order == 1)
        {
            go.transform.Rotate(0, 0, 0, Space.Self);
        }
        else if (order == 2)
        {
            go.transform.Rotate(0, 0, 3, Space.Self);
        }
        else if (order == 3)
        {
            go.transform.Rotate(0, 0, -3, Space.Self);
        }
        else if (order == 4)
        {
            go.transform.Rotate(0, 0, 7, Space.Self);
        }
        else if (order == 5)
        {
            go.transform.Rotate(0, 0, -7, Space.Self);
        }
        else if (order == 6)
        {
            go.transform.Rotate(0, 0, 10, Space.Self);
        }
        else if (order == 7)
        {
            go.transform.Rotate(0, 0, -10, Space.Self);
        }
    }

    public void stopFire()
    {
        isshot = false;
    }

    public void addRate()
    {

        if (GameManage.bulletLevel == 1)
        {
            upSpeed = 6f;
            downSpeed = 2f;
        }
        else if (GameManage.bulletLevel == 2)
        {
            upSpeed = 7f;
            downSpeed = 3f;
        }
        else if (GameManage.bulletLevel == 3)
        {
            upSpeed = 7f;
            downSpeed = 4f;
        }
    }

    public float getRate()
    {
        float rate = rota[GameManage.bulletLevel - 1];
        if(bulletNumber == 3){
            rate = rate - 0.04f * (order - 1);
        }
        else if (bulletNumber == 1)
        {
            if (order == 4 || order == 5)
            {
                rate = rate + 0.06f;
            }
        }
        return rate;
    }
}
