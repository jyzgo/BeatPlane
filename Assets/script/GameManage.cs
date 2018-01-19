using UnityEngine;
using System.Collections;

/// <summary>
/// 大招类型
/// </summary>
public enum MagicType {
    Magic1,
    Magic2,
    Magic3,
    Magic4,
    Magic5
}

/// <summary>
/// 游戏状态
/// </summary>
public enum GameStatus
{
    Home, // 在首页
    Game, // 游戏中
    Success, // 战斗胜利
    Failed, // 战斗失败
    Setting // 游戏设置

}

/// <summary>
/// 玩家战机样式
/// </summary>
public enum PlayerStyle
{
    PlayerOne,
    PlayerTwo,
    PlayerThree,
    PlayerFour,
    PlayerFive
}

public class GameManage : MonoBehaviour
{
    public static int gameLevel = 8; // 游戏关卡

    public static GameStatus gameStatus = GameStatus.Home; // 游戏状态

    public static bool isPlaySound = true;// 是否播放音乐

    public static PlayerStyle playerStyle = PlayerStyle.PlayerOne; // 玩家战机模型

    public static int playerLife = 10; // 玩家生命

    public static int gemNum = 1000; // 宝石数量

    public static int playerSafe = 2; // 玩家拥有的无敌护盾数量
    public static bool isSafeIng = false; // 是否开启无敌护盾中

    public static int bulletNumber = 5; // 子弹编号
    public static int bulletTempNumber = 1; // 子弹临时编号
    public static int bulletLevel = 4; // 子弹等级
    public static int bulletTempLevel = 1; // 子弹临时等级
    public static int playerHp = 5; // 玩家血量

    public static bool level_up_sound = false; // 是否播放升级音乐

    public static float bgMoveSpeed = 1.6f; // 背景移动速度
    // 法术
    public static MagicType magic2 = MagicType.Magic3; // 大招编号
    public static MagicType currMagic = MagicType.Magic1;
    public static int magicCount = 10; // 大招数量
    public static int magicLevel2 = 1; // 大招等级
    public static int currMagicLevel = 1; // 当前释放的大招等级
    public static bool isOutMagic = false; // 是否释放大招
    public static bool isMagicIng = false;  // 是否正在释放大招
    public static int[] magicTimeArray = { 400, 400, 500, 400, 400 }; // 每个大招的释放时长

    public static bool isOutBoss = false; // 是否boss出现
    public static int bossHead = 1; // boss头颅，默认一个

    // 敌机数组
    public static Hashtable monsterMap = new Hashtable();

    private float timer = 0;

    // FPS
    public float f_UpdateInterval = 0.5F;
    private float f_LastInterval;
    private int i_Frames = 0;
    private float f_Fps;

    void Start()
    {
        Application.targetFrameRate = 60;

        f_LastInterval = Time.realtimeSinceStartup;
        i_Frames = 0;
    }

    void FixedUpdate()
    {
        ++i_Frames;
        if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
        {
            f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
            i_Frames = 0;
            f_LastInterval = Time.realtimeSinceStartup;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 200, 200), "FPS:" + f_Fps.ToString("f2"));
    }

    public static void initPlayerBulletNumber()
    {
        bulletLevel = 1;
        bulletNumber = 1;
    }

    public static void changeBulletNumber(int num){
        if(bulletNumber == num){
            if(bulletLevel < 5){
                bulletLevel++;
            }
        }
        bulletNumber = num;
    }

    public static void changeMagicNumber(MagicType type)
    {
        if (magic2 == type)
        {            
            if(magicLevel2 < 2){
                magicLevel2 = 2;
            }
        }
        else
        {
            magicLevel2 = 1;
        }
        magicCount++;
        magic2 = type;
    }

}
