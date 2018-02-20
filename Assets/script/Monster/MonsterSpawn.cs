using UnityEngine;
using System.Collections;

public class MonsterSpawn : MonoBehaviour {

    public Transform[] m_monster; // 怪物列表

    public MonsterType[] m_type; // 怪物类型

    public int[] m_timer; // 怪物生产延迟时间
    public int[] m_rate; // 怪物生产速率
    public int[] m_random; // 怪物生产间隔增加随机时间最大值，0代表不增加

    public int[] m_reward; // 奖励物品

    public float born_x = 100; // 出生点x坐标
    public float born_y = 100; // 出生点y坐标

    private int[] timer;

    protected Transform m_transform;

    private Monster monsterScript;
    private Transform monsterObj;

    private Vector3 new_v3;

	void Start () {
        m_transform = this.transform;
        timer = new int[m_timer.Length];
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManage.isOutBoss)
        {
            return;
        }

        for (int i = 0, k = m_monster.Length; i < k; i++ )
        {
            timer[i] = timer[i] + 1;

            if(timer[i] > m_timer[i]){

                if (timer[i] > m_timer[i] + 1)
                {
                    
                    if(m_random[i] > 0){
                        m_timer[i] = (int) (Random.value * m_random[i]);
                        if (m_timer[i] < m_rate[i])
                        {
                            m_timer[i] = m_rate[i];
                        }
                    }
                    else
                    {
                        m_timer[i] = m_rate[i];                        
                    }
                    timer[i] = 0;
                }
                else
                {
                    monsterObj = m_monster[i];
                    float x = born_x;
                    float y = born_y;

                    if (born_x == 100) // 出生点X不固定坐标
                    {
                        int r = (int)(Random.value * 10);
                        x = Random.value * 1.7f * (r % 2 == 0 ? 1 : -1);
                    }

                    if (born_y == 100) // 出生点Y不固定坐标
                    {
                        y = m_transform.position.y;
                    }

                    // 出生坐标点
                    new_v3 = new Vector3( x, y, m_transform.position.z);
                    
                    Instantiate(monsterObj, new_v3, Quaternion.identity);
                    monsterScript = monsterObj.GetComponent<Monster>();
                    monsterScript.type = m_type[i];
                    monsterScript.rewardIndex = m_reward[i];                                        

                }
            }

        }

	}

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "11.png", true);
    }
}
