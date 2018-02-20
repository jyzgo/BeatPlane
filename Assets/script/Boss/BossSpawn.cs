using UnityEngine;
using System.Collections;
using System;

public enum BossType
{
    Boss_1,
    Boss_2,
    Boss_3,
    Boss_4,
    Boss_5,
    Boss_6,
    Boss_7,
    Boss_8,
    Boss_9
}

public class BossSpawn : MonoBehaviour {

    public int timer = 0;
    public int second = 0;
    public int minute = 0;
    public int hour = 0;    

    private BossType bossType;

    public GameObject[] bossList_1;
    public GameObject[] bossList_2;
    public GameObject[] bossList_3;
    public GameObject[] bossList_4;
    public GameObject[] bossList_5;
    public GameObject[] bossList_6;
    public GameObject[] bossList_7;
    public GameObject[] bossList_8;
    public GameObject[] bossList_9;
    public GameObject[] bossList_10;
    public GameObject[] bossList_11;
    public GameObject[] bossList_12;

    public string[] bosstime_1;
    public string[] bosstime_2;
    public string[] bosstime_3;
    public string[] bosstime_4;
    public string[] bosstime_5;
    public string[] bosstime_6;
    public string[] bosstime_7;
    public string[] bosstime_8;
    public string[] bosstime_9;
    public string[] bosstime_10;
    public string[] bosstime_11;
    public string[] bosstime_12;

    public int bossIndex = 0;

    private GameObject bossObj;
    private Boss bossScript;

    protected Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
	}

    void OnGUI()
    {
        GUIStyle bb = Utils.createStyle(Color.white, 20);
        string timeStr = Utils.fillTime0(hour) + ":" + Utils.fillTime0(minute) + ":" + Utils.fillTime0(second);
        GUI.Label(new Rect(Screen.width - 160, 75, 160, 20), "Time：" + timeStr, bb);
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.isOutBoss)
        {
            return;
        }
        timer++;
        if(timer >= 60){
            timer = 0;
            second++;
            if (second >= 60)
            {
                second = 0;
                minute++;
                if(minute >= 60){
                    minute = 0;
                    hour++;
                    GC.Collect();
                }
            }
        }
        GameObject[] bossList = getBossList();
        if (bossIndex < bossList.Length)
        {
            bossObj = bossList[bossIndex];
            string createTime = getBossTime(bossIndex);
            bossScript = bossObj.GetComponent<Boss>();
            if (createTime.Equals(hour + ":" + minute + ":" + second) && !GameManage.isOutBoss)
            {
                bossIndex++;
                Instantiate(bossObj, new Vector3(bossScript.x, bossScript.y, 0), Quaternion.identity);
                GameManage.bossHead = bossScript.bossHead;
                GameManage.isOutBoss = true;
            }
        }
        

	}

    GameObject[] getBossList()
    {
        GameObject[] list = new GameObject[0];
        switch(GameManage.gameLevel){
            case 1:
                list = bossList_1;
                break;
            case 2:
                list = bossList_2;
                break;
            case 3:
                list = bossList_3;
                break;
            case 4:
                list = bossList_4;
                break;
            case 5:
                list = bossList_5;
                break;
            case 6:
                list = bossList_6;
                break;
            case 7:
                list = bossList_7;
                break;
            case 8:
                list = bossList_8;
                break;
            case 9:
                list = bossList_9;
                break;
            case 10:
                list = bossList_10;
                break;
            case 11:
                list = bossList_11;
                break;
            case 12:
                list = bossList_12;
                break;
        }

        return list;
    }

    string getBossTime(int index)
    {
        string[] list = new string[0];
        switch (GameManage.gameLevel)
        {
            case 1:
                list = bosstime_1;
                break;
            case 2:
                list = bosstime_2;
                break;
            case 3:
                list = bosstime_3;
                break;
            case 4:
                list = bosstime_4;
                break;
            case 5:
                list = bosstime_5;
                break;
            case 6:
                list = bosstime_6;
                break;
            case 7:
                list = bosstime_7;
                break;
            case 8:
                list = bosstime_8;
                break;
            case 9:
                list = bosstime_9;
                break;
            case 10:
                list = bosstime_10;
                break;
            case 11:
                list = bosstime_11;
                break;
            case 12:
                list = bosstime_12;
                break;
        }

        return list[index];
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "boss.png", true);
    }

}
