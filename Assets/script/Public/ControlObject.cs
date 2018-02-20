using UnityEngine;
using System.Collections;
using System;

public class ControlObject : MonoBehaviour {

    public string type = "1"; // 对象类型

    // 11111111
    public float angle = 10f; // 每帧旋转角度，当type=1时有效

    // 222222222
    public float scale = 0.1f; // 每帧缩放倍数，当type=2时有效
    private int scaleStatus = -1;// 1放大，-1缩小
    private int scaleTimer = 0;

    // 444444444
    public float speed = 0.1f; // 每帧移动的距离，当type=4时有效
    private float colorAlpha = 0;
    private float m_fScale = 1;

    // 55555 6666666
    private int fs_way = 1; // 方向
    public int fs_xval = 10; // 摆动幅度
    private int fs_timer = 0; // 计时器

    // 7777777
    public int hidInterval = 10; // 若隐若现帧率，当type=7时有效
    private int hidTimer = 0; // 若隐若现计时器
    private float h_fAlpha = 1;
    private float h_status = 1;

    private string[] typeArr = null;

    private SpriteRenderer render;
    private Material m_mMaterial;

	// Use this for initialization
	void Start () {
        typeArr = type.Split(',');
        render = GetComponent<SpriteRenderer>();
        m_mMaterial = render.material;
        colorAlpha = 1;
        m_fScale = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int n = 1;
        for (int i = 0, k = typeArr.Length; i < k; i++ )
        {
            n = Int32.Parse(typeArr[i]);
            switch (n)
            {
                case 1:
                    type_1(); // 旋转
                    break;
                case 2:
                    type_2(); // 放大缩小
                    break;
                case 3:
                    type_3(); // 渐渐透明消失
                    break;
                case 4:
                    type_4(); // 向上移动消失
                    break;
                case 5:
                    type_5(); // 向左摆动
                    break;
                case 6:
                    type_6(); // 向右摆动
                    break;
                case 7:
                    type_7(); // 若影若现
                    break;
                case 8:
                    type_8(); // 下落
                    break;
                case 10:
                    type_10(); // 下落，流星雨
                    break;
                default:

                    break;
            }
        }
        


	}

    void type_1()
    {
        transform.Rotate(0, 0, angle, Space.Self);
    }

    void type_2()
    {
        transform.localScale += scaleStatus * new Vector3(scale, scale, 0);
        scaleTimer++;
        if (scaleStatus == 1 && scaleTimer > 20)
        {
            scaleStatus = -1;
            scaleTimer = 0;
        }
        else if (scaleStatus == -1 && scaleTimer > 20)
        {
            scaleStatus = 1;
            scaleTimer = 0;
        }
    }

    void type_3()
    {
        render.color =  new Color(1.0f, 1.0f, 1.0f, m_fScale);
        m_fScale -= 0.02f;
        if (m_fScale < 0)
        {
            Destroy(gameObject);
        }
    }

    void type_4()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if(transform.position.y > 3.65){
            Destroy(gameObject);
        }
    }

    void type_5()
    {
        fs_timer++;
        if(fs_timer >= fs_xval){
            fs_way = fs_way == 1 ? -1 : 1;
            fs_timer = 0;
        }

        if (fs_way == 1)
        {// 向左
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            transform.Rotate(0, 0, 0.1f, Space.Self);
        }
        else// 向右
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            transform.Rotate(0, 0, -0.1f, Space.Self);
        }       
        
    }

    void type_6()
    {
        fs_timer++;
        if (fs_timer >= fs_xval)
        {
            fs_way = fs_way == 1 ? -1 : 1;
            fs_timer = 0;
        }

        if (fs_way == 1)
        {// 向右
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            transform.Rotate(0, 0, -0.1f, Space.Self);
        }
        else// 向左
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            transform.Rotate(0, 0, 0.1f, Space.Self);
        } 
    }

    void type_7()
    {
        h_fAlpha -= h_status * (1 / hidInterval);
        render.color = new Color(1.0f, 1.0f, 1.0f, h_fAlpha);
        hidTimer ++;
        if (hidTimer >= hidInterval)
        {
            hidTimer = 0;
            h_status = h_status == 1 ? -1 : 1;
        }
    }

    void type_8()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0f);
        }
    }

    void type_10()
    {
        int r = (int)(UnityEngine.Random.value * 20);
        transform.Translate(Vector3.down * Time.deltaTime * (speed + r));
        if(transform.position.y < - 10){
            float x = UnityEngine.Random.value * 1.5f;
            float a = r % 10 > 4 ? 1f : -1f;
            transform.position = new Vector3(x * a, 10f, 0f);
        }
    }

}
