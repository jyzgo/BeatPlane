using UnityEngine;
using System.Collections;

public class BulletBoss : MonoBehaviour {

    public float moveSpeed = 0.1f;

    public float angle = 90f;

    private GameObject player;
    private Transform m_transform;

    // 4号子弹变量
    private int way4 = 1;

    /// <summary>
    /// 1 垂直下落导弹
    /// 2 
    /// </summary>
    public int btype = 1;

	// Use this for initialization
	void Start () {
        /*if (btype == 3)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            m_transform = transform;
        }
        else
        {
            transform.Rotate(0, 0, angle - 90, Space.Self);
        }*/
        transform.Rotate(0, 0, angle - 90, Space.Self);
	}
	
	// Update is called once per frame
	void Update () {

        if (Mathf.Abs(transform.position.y) > 3.9 || Mathf.Abs(transform.position.x) > 2.12)
        {
            Destroy(this.gameObject);
        }

        switch (btype)
        {
            case 0:
                bullet_0();
                break;
            case 1:
                bullet_1();
                break;
            case 2:
                bullet_2();
                break;
            case 3:
                bullet_3();
                break;
            case 4:
                bullet_4();
                break;
            case 5:
                bullet_5();
                break;
            case 6:
                bullet_6();
                break;
            case 7:
                bullet_7();
                break;
            case 8:
                bullet_8();
                break;
            default:
                break;
        }

	}

    void bullet_0()
    {
        if (moveSpeed < 1)
        {
            moveSpeed += 0.01f;
        }
        else
        {
            moveSpeed += 0.05f;
        }
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    void bullet_1()
    {

        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void bullet_2()
    {
        
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void bullet_3()
    {
        /*if (player != null)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 0.4
                || player.transform.position.y - transform.position.y < 0)
            {
                transform.position = new Vector3(
                    Mathf.Lerp(m_transform.position.x, player.transform.position.x, Time.deltaTime),
                    Mathf.Lerp(m_transform.position.y, player.transform.position.y, Time.deltaTime),
                    0
                );
                return;
            }
        }*/
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);       

    }

    void bullet_4()
    {
        if (angle >= 110 && way4 == 1)
        {
            way4 = -1;
            transform.Rotate(0, 0, 90, Space.Self);
        }
        else if (angle <= 80 && way4 == -1)
        {
            way4 = 1;
            transform.Rotate(0, 0, -90, Space.Self);
        }
        angle = angle + 1 * way4;

        transform.position = new Vector3(); 
    }

    void bullet_5()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void bullet_6()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void bullet_7()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void bullet_8()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
