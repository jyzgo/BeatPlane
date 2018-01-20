using UnityEngine;
using System.Collections;
using System;

public class FireLetter : MonoBehaviour {

    public GameObject bullet;

    public float rota = 0.5f;

    public int bulletNumber = 4;

    public int bulletLevel = 1;

    public int order = 1;

    public int type = 1;
    public GameObject explosion;

    public GameObject[] bulletLittle;

    private AudioSource audio = null;

	// Use this for initialization
    IEnumerator Start()
    {

        int bnum = 1, blevel = 1;
        while (type == 1)
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
                if (blevel >= bulletLevel)
                {
                    fire();
                }
            }

            yield return new WaitForSeconds(getRate());
        }

    }

    public void fire()
    {
        try
        {
            GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet bt = bullet.GetComponent<Bullet>();
            bt.order = order;
            bt.bulletNumber = bulletNumber;
            if (order % 2 == 0)
            {
                bt.way = 1;
                bt.leftSpeed = 0f;
                bt.rightSpeed = 6f;
            }
            else
            {
                bt.way = -1;
                bt.leftSpeed = 6f;
                bt.rightSpeed = 0f;
            }
        }catch(Exception e){
        }
        
    }

    public void Boom()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }


    float getRate()
    {
        return 0.5f - GameManage.bulletLevel * 0.05f;
    }
}
