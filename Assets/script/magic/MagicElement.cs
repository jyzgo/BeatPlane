using UnityEngine;
using System.Collections;

public class MagicElement : MonoBehaviour {

    public float downSpeed = 0.1f;
    public float leftSpeed = 0.1f;

    public float rateSpeed = 0.1f;

    public MagicType type = MagicType.Magic1;

    private float timer = 0f;
    private float timerInterval = 2f;

    // magic3
    private Vector3 v3 = Vector3.zero;
    public int way = 1;

    private float algne = 0;
    private float r = 1.8f;

    private float runTimer = 0;
    public float runTimerInterval = 100f;

    public Sprite[] sprite2;
    private int spriteIndex = 0;

    //magic4
    private float currPosY = 0;
    private float currPosX = 0;

    private SpriteRenderer render;

    private GameObject player;

    private float m_y = 0f;

	// Use this for initialization
	void Start () {        
        switch (type)
        {
            case MagicType.Magic1:
                runTimerInterval = GameManage.magicTimeArray[0];
                break;
            case MagicType.Magic2:
                render = GetComponent<SpriteRenderer>();
                runTimerInterval = GameManage.magicTimeArray[1];
                break;
            case MagicType.Magic3:
                runTimerInterval = GameManage.magicTimeArray[2];
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    m_y = player.transform.position.y;
                }                
                break;
            case MagicType.Magic4:
                render = GetComponent<SpriteRenderer>();
                runTimerInterval = GameManage.magicTimeArray[3];
                currPosX = transform.position.x;
                currPosY = transform.position.y;
                break;
            case MagicType.Magic5:
                render = GetComponent<SpriteRenderer>();
                runTimerInterval = GameManage.magicTimeArray[4];
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        switch (type)
        {
            case MagicType.Magic1:
                magic1();
                break;
            case MagicType.Magic2:
                magic2();
                break;
            case MagicType.Magic3:
                magic3();
                break;
            case MagicType.Magic4:
                magic4();
                break;
            case MagicType.Magic5:
                magic5();
                break;
            default:
                break;
        }
        

	}

    void magic1()
    {
        transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        //rateSpeed = rateSpeed + 0.1f;
        //transform.Rotate(new Vector3(0, 0, rateSpeed));

        if (transform.position.y < -3.5 || Mathf.Abs(transform.position.x) > 1.95)
        {
            Destroy(this.gameObject);
        }
    }

    void magic2()
    {
        runTimer++;
        if (runTimer > runTimerInterval)
        {
            Destroy(this.gameObject);
            return;
        }

        render.sprite = sprite2[spriteIndex];
        timer++;
        if (timer > timerInterval)
        {
            timer = 0;
            spriteIndex++;
            if (spriteIndex >= sprite2.Length)
            {
                spriteIndex = 0;
            }
        }

        v3 = new Vector3(leftSpeed, 0, 0);
        transform.position = v3;
        if (leftSpeed > 1.95f && way == 1)
        {
            way = -1;
        }
        else if (leftSpeed < -1.95f && way == -1)
        {
            way = 1;
        }
        leftSpeed = leftSpeed + downSpeed * way;
    }

    void magic3()
    {
        runTimer++;
        if (runTimer > runTimerInterval)
        {
            Destroy(this.gameObject);
            return;
        }
        algne = algne + 15f;
        if(algne > 360f){
            algne = 0f;
        }
        float x = Mathf.Sin((2f * Mathf.PI / 360f) * algne) * r;
        float y = Mathf.Cos((2f * Mathf.PI / 360f) * algne) * r;
        if (m_y >= 1.5f)
        {
            y += 1.5f;
        }
        else if (m_y < -1.5f)
        {
            y -= 1.5f;
        }
        else
        {
            y += m_y;
        }
        v3 = new Vector3(x, y, 0);        
        transform.position = v3;
    }

    void magic4()
    {
        if (sprite2.Length > 0)
        {
            render.sprite = sprite2[spriteIndex/10];
            spriteIndex ++;
            if (spriteIndex/10 >= sprite2.Length)
            {
                spriteIndex = 0;
            }
        }

        if (leftSpeed == 0 && downSpeed == 0)
        {
            runTimer++;
            if (runTimer > runTimerInterval)
            {
                Destroy(this.gameObject);
                return;
            }
            transform.Rotate(0, 0, 2, Space.Self);
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                transform.position = player.transform.position;   
            }           
        }
        else
        {
            float x = transform.position.x;
            float y = transform.position.y;

            y = y + downSpeed * Time.deltaTime;
            x = x + leftSpeed * Time.deltaTime;
            Vector3 speedVector3 = new Vector3(x, y, 0);
            transform.position = speedVector3;

            if (transform.position.y > 3.5)
            {
                Destroy(this.gameObject);
            }
        }
          

    }

    void magic5()
    {

        if (sprite2.Length > 0)
        {
            spriteIndex = GameManage.currMagicLevel > 1 ? 1 : 0;
            render.sprite = sprite2[spriteIndex];
        }

        runTimer++;
        if (runTimer > 100)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.Rotate(0, 0, 4, Space.Self);
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.position = player.transform.position;   
        }             
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet-monster" || other.tag == "bullet-boss")
        {
            Destroy(other.gameObject);
        }
    }
}
