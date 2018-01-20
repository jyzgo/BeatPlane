using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject playerObject;
    private Vector3 playerPos;
    public GameObject[] scoreText;
    public GameObject[] lifeText;
    public GameObject[] magicText;
    public GameObject[] safeText;

    public GameObject[] hpSprite;

    public static int gameScore = 0;
    public static int upScore = 1000;

    public float moveSpeed = 1f;
    private OutMagic outMagic;

    public static PlayerController current;
	// Use this for initialization
	void Start () {
        current = this;
        outMagic = GameObject.FindObjectOfType<OutMagic>();

        CreatePlayer();
        setMagicText();
        SetSafeText();
	}
	
	// Update is called once per frame
	void Update () {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {// 死亡
            CreatePlayer();
        }
        else
        {
            playerPos = playerObject.transform.position;
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (isMoved(KeyCode.W))
                {
                    playerObject.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
                }                
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (isMoved(KeyCode.S))
                {
                    playerObject.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
                }
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (isMoved(KeyCode.A))
                {
                    playerObject.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (isMoved(KeyCode.D))
                {
                    playerObject.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                }
            }
            
            if(Input.GetKeyUp(KeyCode.K)){
                outMagic.IssueMagic();
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("Home");
        }

        if (Input.GetKey(KeyCode.Menu))
        {
            Application.Quit();
        }

	}

    
    public bool isMoved(KeyCode kc){
        bool moved = false;
        switch (kc)
        {
            case KeyCode.W:
                if (playerPos.y < 3.2f)
                {
                    moved = true;
                }
                break;
            case KeyCode.S:
                if (playerPos.y > -3.5f)
                {
                    moved = true;
                }
                break;
            case KeyCode.A:
                if (playerPos.x > -1.9f)
                {
                    moved = true;
                }
                break;
            case KeyCode.D:
                if (playerPos.x < 1.9f)
                {
                    moved = true;
                }
                break;
        }        
        return moved;
    }

    void CreatePlayer()
    {
        if(GameManage.playerLife > 0){
            GameManage.playerLife--;
            SetLifeText();
            playerObject = Instantiate(playerPrefab, transform.position, transform.rotation);
            playerObject.SetActive(true);
            GameManage.playerHp = 5;
            setHpSprite();
        }
        else
        {

        }         
    }
     
    public void addScore(int score)
    {
        gameScore += score;
        SetScoreText();
        if (isLevelUp())
        {
            GameManage.playerLife = GameManage.playerLife + 1;
            SetLifeText();
            AudioSource audio = GetComponent<AudioSource>();
            if(audio != null){
                audio.Play();
                GameManage.level_up_sound = true;
                Invoke("LevelUpEnd", 1.5f);
            }
        }
    }

    void LevelUpEnd()
    {
        GameManage.level_up_sound = false;
    }

    bool isLevelUp()
    {
        if (gameScore > upScore)
        {
            upScore = upScore + (int)(upScore * 1.5f);
            return true;
        }
        return false;
    }

    public void SetLifeText()
    {
        int num1 = (int) (GameManage.playerLife / 10);
        NumberObj num = lifeText[0].GetComponent<NumberObj>();
        num.SetNumber(num1);

        int num2 = GameManage.playerLife%10;
        num = lifeText[1].GetComponent<NumberObj>();
        num.SetNumber(num2);
    }

    public void setHpSprite()
    {
        GameObject sprite = null;
        for (int i = 0, k = hpSprite.Length; i < k ; i++)
        {
            sprite = hpSprite[i];
            sprite.SetActive(GameManage.playerHp > i);
        }        
    }

    public void setMagicText()
    {
        int num1 = (int)(GameManage.magicCount / 10);
        NumberObj num = magicText[0].GetComponent<NumberObj>();
        num.SetNumber(num1);

        int num2 = GameManage.magicCount % 10;
        num = magicText[1].GetComponent<NumberObj>();
        num.SetNumber(num2);

        num = magicText[2].GetComponent<NumberObj>();
        num.SetNumber(GameManage.magicLevel2 - 1);
    }

    public void SetScoreText()
    {
        string scoreStr = Utils.fillScore0(gameScore);
        string cha = "";
        int num = 0;
        NumberObj scoreObj;
        for (int i = 0, k = scoreStr.Length; i < k; i++ )
        {
            cha = scoreStr.Substring(i, 1);
            num = Int32.Parse(cha);
            scoreObj = scoreText[i].GetComponent<NumberObj>();
            scoreObj.SetNumber(num);
        }
        scoreObj = null;
        scoreStr = null;
        cha = null;
    }

    public void SetSafeText()
    {
        int num1 = (int)(GameManage.playerSafe / 10);
        NumberObj num = safeText[0].GetComponent<NumberObj>();
        num.SetNumber(num1);

        int num2 = GameManage.playerSafe % 10;
        num = safeText[1].GetComponent<NumberObj>();
        num.SetNumber(num2);
    }
    
}
