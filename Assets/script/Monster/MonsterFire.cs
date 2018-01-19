using UnityEngine;
using System.Collections;

public class MonsterFire : MonoBehaviour
{

    private float speed = 0.1f;
    public float rate = 2f;

    /**
     * 子弹类型：
     *  1   普通类型
     *  2   360°发送类型
     * */
    public int type = 1;

    public float timer = 0.4f; // 延时发送时间

    private int n3 = 0;
    private int timerInterval3 = 1;
    private MonsterBullet mbull;

    public int bulletWave = 0; // 子弹波数
    private int bwave = 0; // 当前波数

    private AudioSource audio;
    public bool isAutoFire = true; // 是否自动发射子弹
    private bool isfire = false; // 是否可以发射子弹了
    private bool isfired = false; // 是否已经发射子弹了

    public GameObject[] bullet;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public Monster _monster;

    void FixedUpdate()
    {
        if (!isAutoFire)
        {
            return;
        }

        if (isfired)
        {
            return;
        }
        if (isfire)
        {
            Invoke("startFire", timer);
            isfired = true;
        }
        else
        {
            if(transform.position.y < 3.2){
                isfire = true;
            }
        }
        
    }

    public void fire()
    {
        if (bulletWave != 0)
        {
            if (bwave > bulletWave)
            {
                return;
            }
            bwave++;
        }    
        if(_monster != null)
        {
            _monster.SetFireSpeed();
        }
        switch (type)
        {
            case 1:
                fireType1();
                break;
            case 2:
                fireType2();
                break;
            case 3:
                fireType3();
                break;
            case 4:
                fireType4(); // 指向英雄发射
                break;
            case 5:
                fireType5();
                break;
            case 6:
                fireType6();
                break;
            case 8:
                fireType8();
                break;
            case 31:
                fireType31();
                break;
            case 32:
                fireType32();
                break;
            default:
                break;
        }

    }

    private void startFire()
    {
        InvokeRepeating("fire", speed, rate);               
    }

    void fireType1()
    {
        if (audio != null)
        {
            audio.Play();
        }
        for (int i = 0, k = bullet.Length; i < k; i++)
        {
            GameObject.Instantiate(bullet[i], transform.position, Quaternion.identity);
            mbull = bullet[i].GetComponent<MonsterBullet>();
            mbull.bulletNumber = 1;
        }

    }

    void fireType2()
    {
        if (audio != null)
        {
            audio.Play();
        }
        for (int i = 0, k = bullet.Length; i < k; i++)
        {
            GameObject.Instantiate(bullet[i], transform.position, Quaternion.identity);
            mbull = bullet[i].GetComponent<MonsterBullet>();
            mbull.bulletNumber = 2;
        }

    }

    void fireType3()
    {
        n3++;
        if(n3%timerInterval3 == 0){
            if (audio != null)
            {
                audio.Play();
            }
            
            GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
            mbull = bullet[0].GetComponent<MonsterBullet>();
            mbull.bulletNumber = 3;
            int index = n3 / timerInterval3;
            switch (index)
            {
                case 1:
                    mbull.upSpeed = 0f;
                    mbull.downSpeed = 0f;
                    mbull.leftSpeed = 0f;
                    mbull.rightSpeed = 1f;
                    break;
                case 2:
                    mbull.upSpeed = 0f;
                    mbull.downSpeed = 1f;
                    mbull.leftSpeed = 0f;
                    mbull.rightSpeed = 1f;
                    break;
                case 3:
                    mbull.upSpeed = 0f;
                    mbull.downSpeed = 1f;
                    mbull.leftSpeed = 0f;
                    mbull.rightSpeed = 0f;
                    break;
                case 4:
                    mbull.upSpeed = 0f;
                    mbull.downSpeed = 1f;
                    mbull.leftSpeed = 1f;
                    mbull.rightSpeed = 0f;
                    break;
                case 5:
                    mbull.upSpeed = 0f;
                    mbull.downSpeed = 0f;
                    mbull.leftSpeed = 1f;
                    mbull.rightSpeed = 0f;
                    break;
                case 6:
                    mbull.upSpeed = 1f;
                    mbull.downSpeed = 0f;
                    mbull.leftSpeed = 1f;
                    mbull.rightSpeed = 0f;
                    break;
                case 7:
                    mbull.upSpeed = 1f;
                    mbull.downSpeed = 0f;
                    mbull.leftSpeed = 0f;
                    mbull.rightSpeed = 0f;
                    break;
                case 8:
                    mbull.upSpeed = 1f;
                    mbull.downSpeed = 0f;
                    mbull.leftSpeed = 0f;
                    mbull.rightSpeed = 1f;
                    break;
                default:
                    n3 = 0;
                    break;
            }

        }        

    }


    void fireType4()
    {
        GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
        mbull = bullet[0].GetComponent<MonsterBullet>();
        mbull.bulletNumber = 4;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float angle = Utils.ComputeAngle(
                player.transform.position,
                transform.parent.transform.position
            );
            mbull.rotaAngle = angle;
        }
    }

    // 8颗子弹其发射
    void fireType5()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
            mbull = bullet[0].GetComponent<MonsterBullet>();
            mbull.bulletNumber = 5;
            mbull.rotaAngle = i * 45f;
        }
    }

    void fireType6()
    {
        GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
        mbull = bullet[0].GetComponent<MonsterBullet>();
        mbull.bulletNumber = 6;
    }

    void fireType8()
    {
        if (audio != null)
        {
            audio.Play();
        }
        for (int i = 0, k = bullet.Length; i < k; i++)
        {
            GameObject.Instantiate(bullet[i], transform.position, Quaternion.identity);
            mbull = bullet[i].GetComponent<MonsterBullet>();
            mbull.bulletNumber = 8;
        }

    }

    void fireType31()
    {
        GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
        mbull = bullet[0].GetComponent<MonsterBullet>();
        mbull.bulletNumber = 31;
        mbull.rotaAngle = 10f + 90f;
    }

    void fireType32()
    {
        GameObject.Instantiate(bullet[0], transform.position, Quaternion.identity);
        mbull = bullet[0].GetComponent<MonsterBullet>();
        mbull.bulletNumber = 32;
        mbull.rotaAngle = -10f + 90f;
    }
}
