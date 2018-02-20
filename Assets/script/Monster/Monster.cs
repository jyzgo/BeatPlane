using UnityEngine;
using System.Collections;
using System;

public enum MonsterType
{
    Down, // 直下
    LeftUp_GZ_Up, // 左上-跟踪-右上
    RightUp_GZ_Up, // 右上-跟踪-左上
    Down_Left_Right, // 下-左右摆动
    LeftDown_RightUp, // 左下右上
    RightDown_LeftUp, // 右下左上
    Looming_Down, // 隐身直下
    Telesport, // 瞬间移动
    stayTop, // 停留在顶部
    LeftDown_Stay_RightUp, // 左下-停留-右上
    RightDown_Stay_LeftUp // 右下-停留-左上
}

public class Monster : MonoBehaviour {
    readonly Color[] colors = {
        new Color(0.96f, 0.71f, 0.18f),
        new Color(0.52f,0.77f,0.31f),
        new Color(0.64f,0.56f,0.32f),
        new Color(0.89f,0.07f,0.37f),
        new Color(0.76f,0.13f,0.53f),
        new Color(0.09f,0.45f,0.74f),
        new Color(0f,0.6f,0.55f),
        new Color(0.88f,0.32f,0.12f),
        new Color(0.24f,0.15f,0.14f),
        new Color(0.1f,0.14f,0.49f),
        new Color(0.72f,0.11f,0.11f) };

    int[] LV_ARR = new int[]{
         5,
        10,
        15,
        30,
        50,
        100,
        150,
        200,
        300,
        400
        };

    Color GetCurrentColor()
    {

        return colors[GetCurrentColorIndex()];
    }

    int GetCurrentColorIndex()
    {
        for (int i = 0; i < LV_ARR.Length; i++)
        {
            if (hp < LV_ARR[i])
            {
                return i;
            }
        }
        return colors.Length - 1;
    }

    Color GetPreColor()
    {
        int index = GetCurrentColorIndex() - 1;
        if (index > 0)
        {
            return colors[index];
        }
        return colors[0];

    }
    void InitColor()
    {
        sp.color = GetCurrentColor();
    }
    Vector3 _offsetColor;
    void UpdateColor()
    {
        if (hp > LV_ARR[0])
        {

            var currentColor = GetCurrentColor();
            var preColor = GetPreColor();
            int currentIndex = GetCurrentColorIndex();
            int indexHp = LV_ARR[currentIndex];
            int preHp = LV_ARR[currentIndex - 1];
            float per = (float)(hp - preHp) / (float)(indexHp - preHp);

            sp.color = Color.Lerp(currentColor, preColor, per);
        }


    }


    public float hp = 100;
    private float totalHp = 100;

    public float left = 0f;
    public float right = 0f;
    public float up = 0f;
    public float down = 1f;

    public int score = 100;
    public GameObject explosion;
    private Explosion expScript;
    public GameObject[] rewards;
    public bool issafe = true; // 是否出生安全时
    private bool isSafeStatus = false; // 
    public bool isshot = true; // 是否发射子弹
    public bool isboss = false; // 是否为boss

    public int rewardIndex = -1; // -1 为随机产生

    private float leftMoveSpeed = 0f;
    private float rightMoveSpeed = 1f;

    private int status = 1; // 状态

    private float tempMoveSpeed = 1f;
    private float timer = 0f;
    private float timerInteval = 100f;
    private float safeTimerInteval = 20f;
    public MonsterType type;
    public int m_cls = 1;// 类型

    private bool isdead = false;    

    private float colorAlpha = 0;

    private SpriteRenderer sp;    

    private Bullet bulletScript;

    public Transform monsterSp;

    private string mid = "";

    public void SetFireSpeed()
    {
        _rotateSpeed = FIRE_SPEED;
    }

    private string[] charArr = { 
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "m", "l", "n",
        "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B",
        "C", "D", "E", "F", "G", "H", "I", "J", "K", "M", "L", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3",
        "4", "5", "6", "7", "8", "9"
    };

    private PlayerController controller;
    private GameObject Player;

    public GameObject hpline;
    private Transform hplineTransform;

    private Vector3 boomPos;
    private int boomIndex = 1;

    // Looming_Down的属性
    private bool isAppear = false; // 是否出现
    private int appearTimeLong = 0; // 是否时长
    private int appearTimer = 0;// 隐现怪物计时器
  
	// Use this for initialization
    void Awake()
    {
        mid = GenerateId();
    }

	void Start () {        
        GameManage.monsterMap.Add(mid, this.gameObject);
        controller = FindObjectOfType<PlayerController>();
        sp = GetComponentInChildren<SpriteRenderer>();
        mfire = GetComponentInChildren<MonsterFire>();
        mfire._monster = this;
        totalHp = hp;
        if (isboss)
        {
            hplineTransform = hpline.transform;
        }
        UpdateColor();
	}
    MonsterFire mfire;
    Vector3 _rotate = new Vector3(0, 0, 10f);
    const float NORMAL_SPEED = 1f;
    const float FIRE_SPEED = 15f;
    const float SLOWDOWN_SPEED = 0.2f;
    float _rotateSpeed = NORMAL_SPEED;
	// Update is called once per frame
	void FixedUpdate () {
        if (_rotateSpeed > NORMAL_SPEED)
        {
            _rotateSpeed -= SLOWDOWN_SPEED;
        }else
        {
            _rotateSpeed = NORMAL_SPEED;
        }


        sp.transform.Rotate(new Vector3(0,0,_rotateSpeed));
        //if (colorAlpha > 0)
        //{
        //    colorAlpha -= 0.1f;
        //    sp.color = Color.Lerp(Color.white, Color.red, colorAlpha);
        //}else
        //{
        //    UpdateColor();
        //}

        switch (type)
        {
            case MonsterType.Down:
                Move(Vector3.down, down);
                break;
            case MonsterType.LeftUp_GZ_Up:
                MoveLeftUpGzUp();
                break;
            case MonsterType.RightUp_GZ_Up:
                MoveRightUpGzUp();
                break;
            case MonsterType.Down_Left_Right:
                break;
            case MonsterType.LeftDown_RightUp:
                LeftDownRightUp();
                break;
            case MonsterType.RightDown_LeftUp:
                RightDownLeftUp();
                break;
            case MonsterType.LeftDown_Stay_RightUp:
                LeftDownStayRightUp();
                break;
            case MonsterType.RightDown_Stay_LeftUp:
                RightDownStayLeftUp();
                break;
            case MonsterType.Looming_Down:
                LoomingDown();
                break;
            case MonsterType.Telesport:
                Telesport();
                break;
            case MonsterType.stayTop:
                StayTop();
                break;
            default:
                break;
        }

        timer++;
        if (timer > timerInteval)
        {
            timer = 0;
            tempMoveSpeed = rightMoveSpeed;
            rightMoveSpeed = leftMoveSpeed;
            leftMoveSpeed = tempMoveSpeed;
        }
        
	}

    private void Boom()
    {
        int count = 1;
        if (isboss)
        {
            count = 3;
        }

        for (int i = 0; i < count; i++)
        {
            createExplosion();
        }
    }

    private void createExplosion()
    {
        if (isboss)
        {
            boomPos = new Vector3(
                transform.position.x + (UnityEngine.Random.value * 2f) * 1,
                transform.position.y + (UnityEngine.Random.value * 0.2f) * 1,
                transform.position.z
            );
            Instantiate(explosion, boomPos, transform.rotation);
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    public void Move(Vector3 way, float speed)
    {
        if (transform.position.y < 2.9)
        {
            issafe = false;
        }
        transform.Translate(way * speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 2.10)
        {
            timer = 0;
            tempMoveSpeed = rightMoveSpeed;
            rightMoveSpeed = leftMoveSpeed;
            leftMoveSpeed = tempMoveSpeed;
        }

        if (transform.position.y < -3.7 && way == Vector3.down)
        {
            Destroy(this.gameObject);
        }

        if(Mathf.Abs(transform.position.x) > 2.3){
            Destroy(this.gameObject);
        }

        if (transform.position.y > 3.8 && way == Vector3.up)
        {
            Destroy(this.gameObject);
        }

    }

    void MoveLeftUpGzUp()
    {
        if (status == 1) // 左边垂直上升
        {
            transform.Translate(Vector3.up * up * Time.deltaTime);
            if (transform.position.y > 3)
            {
                status = 2;
                right = Utils.getXPixByYPixAngle(down, 80);
                issafe = false;
            }
        }
        else if (status == 2) // 跟踪
        {
            Move(Vector3.down, down);
            Move(Vector3.right, Mathf.Abs(right) * 2);

            // distance = Vector3.Distance(Player.transform.position, transform.position);

            ////导弹朝向人  法一
            //transform.LookAt(Player.transform);          

            //导弹朝向人  法二            
            //Quaternion missileRotation = Quaternion.LookRotation(Player.transform.position - transform.position, Vector3.up);
            //missile.transform.rotation = Quaternion.Slerp(missile.transform.rotation, missileRotation, Time.deltaTime * missileRotateSpeed);
            //transform.rotation = missileRotation;
                
            //导弹朝向人   法三            
            //Vector3 targetDirection = man.transform.position - missile.transform.position;
            //float angle = Vector3.Angle(targetDirection,missile.transform.forward);//取得两个向量间的夹角
            //print("angle:"+angle.ToString());
            //if (angle > 5)
            //{
            //    missile.transform.Rotate(Vector3.up, angle);
            //}
            //transform.Translate(Vector3.forward * Time.deltaTime * down);

            if (transform.position.x > 1 || transform.position.y  < -1)
            {
                status = 3;
            }
        }
        else if (status == 3) // 右上快速上升
        {
            Move(Vector3.up, up);
            Move(Vector3.right, Mathf.Abs(right));

            if (transform.position.y > 3.5)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void MoveRightUpGzUp()
    {
        if (status == 1) // 右边垂直上升
        {
            transform.Translate(Vector3.up * up * Time.deltaTime);
            if(transform.position.y > 3){
                status = 2;
                left = Utils.getXPixByYPixAngle(down, 80);
                issafe = false;                
            }
        }
        else if (status == 2) // 跟踪
        {
            Move(Vector3.down, down);
            Move(Vector3.left, Mathf.Abs(left) * 2);

            if (transform.position.x < -1 || transform.position.y < -1)
            {
                status = 3;
            }
        }
        else if (status == 3) // 左上快速上升
        {
            Move(Vector3.up, up);
            Move(Vector3.left, Mathf.Abs(left));

            if (transform.position.y > 3.5)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void LeftDownRightUp()
    {
        if (status == 1)
        { // 左下
            Move(Vector3.down, down);
            Move(Vector3.right, right);
            if(transform.position.y < -1){
                status = 2;
            }
        }
        else if (status == 2)
        { // 右上
            Move(Vector3.up, up);
            Move(Vector3.right, right);
        }
    }

    void RightDownLeftUp()
    {
        if (status == 1)
        { // 右下
            Move(Vector3.down, down);
            Move(Vector3.left, left);
            if (transform.position.y < -1)
            {
                status = 2;
            }
        }
        else if (status == 2)
        { // 左上
            Move(Vector3.up, up);
            Move(Vector3.left, left);
        }
    }

    void LeftDownStayRightUp()
    {
        if (status == 1)
        { // 左下
            Move(Vector3.down, down);
            Move(Vector3.right, right);
            if (transform.position.y < -1)
            {
                status = 2;
            }
        }
        else if (status == 2)
        { // 停留
            Move(Vector3.down, 0.03f);
            if (transform.position.y < -1.1)
            {
                status = 3;
            }
        }
        else if (status == 3)
        { // 右上
            Move(Vector3.up, up);
            Move(Vector3.right, right);
        }
    }

    void RightDownStayLeftUp()
    {
        if (status == 1)
        { // 右下
            Move(Vector3.down, down);
            Move(Vector3.left, left);
            if (transform.position.y < -1)
            {
                status = 2;
            }
        }
        else if (status == 2)
        { // 停留
            Move(Vector3.down, 0.03f);
            if (transform.position.y < -1.1)
            {
                status = 3;
            }
        }
        else if (status == 3)
        { // 左上
            Move(Vector3.up, up);
            Move(Vector3.left, left);
        }
    }

    // 若隐若现
    void LoomingDown()
    {
        Move(Vector3.down, down);
        isAppear = false;
        if(transform.position.y < 2.8f){
            appearTimer++;
            if (appearTimer == 100) // 出现
            {
                isAppear = true;
                isSafeStatus = false;
                sp.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            }
            else if (appearTimer == 1010) // 隐藏
            {
                isAppear = false;
                isSafeStatus = true;
                sp.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);
                appearTimer = 0;
            }
        }        

        // 出现时 发射一颗子弹
        if (isAppear)
        {
           // mfire = GetComponentInChildren<MonsterFire>();
            mfire.fire();
            Debug.Log("FIRE");
            _rotateSpeed = FIRE_SPEED;    
        }
    }

    // 位置随机变换
    void Telesport()
    {
        if (transform.position.y > 2.7f)
        {
            Move(Vector3.down, down);
        }
        else
        {
            appearTimer++;
            if (appearTimer % 40 == 0) // 随机变换位置
            {
                float x = UnityEngine.Random.value * 1.7f;
                float y = UnityEngine.Random.value * 3f;
                int a = (int)(UnityEngine.Random.value * 10);
                if(a > 5){
                    x = -x;
                }
                transform.position = new Vector3(x, y, 0);
            }
            else if (appearTimer % 41 == 0) // 发射子弹
            {
               // mfire = GetComponentInChildren<MonsterFire>();
                mfire.fire();

                _rotateSpeed = FIRE_SPEED;
            }
        }
    }

    void StayTop()
    {
        if (transform.position.y > 2.2f)
        {
            Move(Vector3.down, down);
        }
        else
        {
            if (transform.position.x > 1.7f)
            {
                status = -1;
            }
            else if (transform.position.x < -1.7f)
            {
                status = 1;
            }

            if (status == 1) // 向右
            {
                Move(Vector3.right, right);
            }
            else if (status == -1) // 向左
            {
                Move(Vector3.left, left);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isSafeStatus)
        {
            return;
        }

        // 主角的子弹
        if (isPlayerBullet(other))
        {
            bulletScript = other.GetComponent<Bullet>();
            int delHp = bulletScript.hurtHp;
            injured(delHp);
            if (bulletScript.isDestroy)
            {
                bulletScript.DestorySelf();
            }
        }
        // 法宝
        if (isMagicElement(other))
        {    
            int delHp = 100;
            if (GameManage.currMagic == MagicType.Magic1)
            {
                delHp = 50;
            }
            if (GameManage.currMagic == MagicType.Magic2)
            {
                delHp = 300;
            }
            else if (GameManage.currMagic == MagicType.Magic4)
            {
                delHp = 100 + (75 * GameManage.currMagicLevel);
            }
            else if (GameManage.currMagic == MagicType.Magic5)
            {
                delHp = 150;
                delHp *= GameManage.currMagicLevel;
            }
            injured(delHp);

            if (GameManage.currMagic == MagicType.Magic4 && GameManage.currMagicLevel == 1)
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void injured(int delHp)
    {
        if (!issafe && !isSafeStatus)
        {
            hp -= delHp;
            if (isboss)
            {
                float xScal = hp / totalHp;
                hplineTransform.localScale = new Vector3(xScal, 1, 1);
            }
        }        
        if (hp <= 0 && !isdead)
        {
            isdead = true;
            Boom();
            addScore(score);
            DeadBeforeEvent();
            GameManage.monsterMap.Remove(mid);
            Destroy(this.gameObject);
            if (isboss)
            {
                GameManage.bossHead--;
                if (GameManage.bossHead == 0)
                {
                    GameManage.isOutBoss = false;
                }
            }
        }
        else
        {
            UpdateColor();
            colorAlpha = 1f;
        }
    }

    public void DeadBeforeEvent()
    {
        if (m_cls == 2) // 生产小怪物
        {
            BornBaby bb = GetComponent<BornBaby>();
            bb.Born(transform.position);
        }


        if (rewards != null && rewards.Length > 0)
        {
            int f = (int)(UnityEngine.Random.value * 10);
            if (f % 5 == 0)
            {
                if (rewardIndex == -1)
                {
                    f = (int)(UnityEngine.Random.value * rewards.Length);
                    Instantiate(rewards[f], transform.position, transform.rotation);
                }
                else
                {
                    f = rewardIndex < rewards.Length ? rewardIndex : 0;
                    Instantiate(rewards[f], transform.position, transform.rotation);
                }
                
            }

            

            
        }

    }

    private string GenerateId()
    {
        long i = 1;
        foreach (byte b in System.Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        string id = string.Format("{0:x}", i - System.DateTime.Now.Ticks);
        int index = 0;
        for (int j = 0; j < 8; j++ )
        {
            index = (int)(UnityEngine.Random.value * charArr.Length);
            id += "" + charArr[index];
        }        
        return id;
    }



    public void addScore(int score)
    {
        controller.addScore(score);
    }

    public bool isPlayerBullet(Collider2D other)
    {
        return other.tag == "bullet-player";
    }

    public bool isMagicElement(Collider2D other)
    {
        return other.tag == "magic";
    }

}