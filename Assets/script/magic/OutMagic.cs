using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class OutMagic : MonoBehaviour {

    public MagicType magicType;

    public GameObject magicElement = null;
    public GameObject magicElement42 = null;
    private static bool magicEleCreate = false;
    private GameObject player = null;

    private PlayerController controller;

    private int magicTimer = 0;

    public float timer = 0f;
    public float timeInterval = 4f;

    private int magic3Count = 0;

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<PlayerController>();
	}

    // 释放魔法
    public void IssueMagic()
    {
        //if (GameManage.magicCount > 0)
        {
            if (!GameManage.isMagicCasting)
            {
                GameManage.magicCount -= GameManage.magicLevel2;
                if (GameManage.magicCount < 0)
                {
                    GameManage.magicCount = 0;
                    GameManage.magicLevel2 = 1;
                }
                else if (GameManage.magicCount == 1)
                {
                    GameManage.magicLevel2 = 1;
                }

                GameManage.isMagicCasting = true;
                GameManage.currMagic = GameManage.magic2;
                GameManage.currMagicLevel = GameManage.magicLevel2;
                magicTimer = 0;
                timer = 0;
                magicEleCreate = false;
                controller.setMagicText();
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (magicElement == null) {
            return;
        }
	    if(GameManage.isMagicCasting) {
            switch (GameManage.currMagic)
            {
                case MagicType.Magic1:
                    if (magicType == GameManage.currMagic)
                    {
                        magic1();
                    }                        
                    break;
                case MagicType.Magic2:
                    if (magicType == GameManage.currMagic)
                    {
                        magic2();
                    }
                    break;
                case MagicType.Magic3:
                    if (magicType == GameManage.currMagic)
                    {
                        magic3();
                    }
                    break;
                case MagicType.Magic4:
                    if (magicType == GameManage.currMagic)
                    {
                        magic4();
                    }
                    break;
                case MagicType.Magic5:
                    if (magicType == GameManage.currMagic)
                    {
                        magic5();
                    }
                    break;
                default:
                    break;
            }
        }
	}

    public void magic1()
    {
        magicTimer++;
        if (magicTimer < GameManage.magicTimeArray[0])
        {
            timer++;
            if (timer > timeInterval)
            {
                timer = 0;
                createMagic1();
            }
        }
        else
        {
            endMagic();
        }
        
    }

    public void createMagic1()
    {
        float x = 0;
        int num = GameManage.currMagicLevel > 1 ? 6 : 3;
        for (int i = 0; i < num; i++)
        {
            x = Random.value * 1.95f * (Random.value * 1 > 0.5 ? 1 : -1);
            Instantiate(magicElement, new Vector3(x, 2.95f, 0), Quaternion.identity);
        }
    }

    public void magic2()
    {
        magicTimer++;
        if (magicTimer < GameManage.magicTimeArray[1])
        {
            createMagic2();
        }
        else
        {
            endMagic();
        }
    }

    void createMagic2()
    {
        if (!GameManage.isMagicIng)
        {
            GameManage.isMagicIng = true;
            Instantiate(magicElement, new Vector3(0f, 0f, 0), Quaternion.identity);
            MagicElement me = magicElement.GetComponent<MagicElement>();
            me.way = 1;
            if (GameManage.currMagicLevel == 2)
            {
                Instantiate(magicElement, new Vector3(0f, 0f, 0), Quaternion.identity);
                me = magicElement.GetComponent<MagicElement>();
                me.way = -1;
            }
            
        }
    }

    public void magic3()
    {
        magicTimer++;
        if (magicTimer < GameManage.magicTimeArray[2])
        {
            timer++;
            if (timer > timeInterval)
            {
                timer = 0;
                createMagic3();
            }
        }
        else
        {
            endMagic();
        }

        
    }

    void createMagic3()
    {
        int num = GameManage.currMagicLevel > 1 ? 55 : 31;
        if (!GameManage.isMagicIng)
        {
            if (magic3Count < num)
            {
                magic3Count++;
                if (magic3Count % 6 == 0)
                {
                    Instantiate(magicElement, new Vector3(0f, 0f, 0f), Quaternion.identity);
                }
            }
            else
            {
                GameManage.isMagicIng = true;
            }

        }
    }


    public void magic4()
    {
        magicTimer++;
        if (magicTimer < GameManage.magicTimeArray[3])
        {
            timer++;
            if (timer > timeInterval)
            {
                timer = 0;
                createMagic4();
            }
        }
        else
        {
            endMagic();
        }
    }

    void createMagic4()
    {
        GameObject monster = Utils.GetTargetMonster(transform.position);
        if (monster != null)
        {
            Instantiate(magicElement, new Vector3(0, -2, 0), Quaternion.identity);
        }

        if (!magicEleCreate)
        {
            magicEleCreate = true;
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Instantiate(magicElement42, player.transform.position, Quaternion.identity);
            }            
        }
    }

    public void magic5()
    {
        magicTimer++;
        if (magicTimer < GameManage.magicTimeArray[4])
        {
            timer++;
            if (timer > timeInterval)
            {
                timer = 0;
                createMagic5();
            }
        }
        else
        {
            endMagic();
        }
    }

    void createMagic5()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Instantiate(magicElement, player.transform.position, Quaternion.identity);
        }        
    }

    public void endMagic()
    {
        GameManage.isMagicCasting = false;
        GameManage.isMagicIng = false;
        magicTimer = 0;
        magic3Count = 0;
    }

}
