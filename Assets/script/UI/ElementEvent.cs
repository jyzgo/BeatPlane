﻿using UnityEngine;
using System.Collections;

public class ElementEvent : MonoBehaviour {

    public GameObject BtnMagic;
    public GameObject BtnSafe;
    public static ElementEvent current;

    private Vector3 pos;
    private OutMagic outMagic = null;
    private GameObject player = null;
    //private PlayerController controller;

    //PlayerController GetPlayerController(){
    //    if (controller == null)
    //    {
    //        controller = FindObjectOfType<PlayerController>();
    //    }
    //    return controller;
    //}

    private void Start()
    {
        current = this;
    }

    OutMagic GetMagic()
    {
        if (outMagic == null)
        {
            outMagic = GameObject.FindObjectOfType<OutMagic>();
        }
        return outMagic;
	}
	
	// Update is called once per frame
	//void FixedUpdate () {
 //       if (Input.GetMouseButtonDown(0))
 //       {
 //           pos = Input.mousePosition;
 //           //Debug.Log(pos.x + " : " + pos.y);
 //           if (pos.x > 300 && pos.x < 360 && pos.y > 0 && pos.y < 55)
 //           {
 //               // 是否保护盾                
 //               BtnSafe.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
 //           }
 //           else if (pos.x > 0 && pos.x < 55 && pos.y > 0 && pos.y < 55)
 //           {
 //               // 是否必杀技
 //               BtnMagic.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
 //           }
 //       } else if(Input.GetMouseButtonUp(0)){
 //           BtnSafe.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
 //           BtnMagic.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
 //           pos = Input.mousePosition;
 //           Debug.Log(pos.x + " : " + pos.y);
 //           if (pos.x > 300 && pos.x < 360 && pos.y > 0 && pos.y < 55)
 //           {
 //               // 是否保护盾                
 //               IssueSafe();
 //           }
 //           else if (pos.x > 0 && pos.x < 55 && pos.y > 0 && pos.y < 55)
 //           {
 //               // 是否必杀技                
 //               IssueMagic();
 //           }
 //       }
	//}

    public void IssueSafe()
    {

        if (!GameManage.isSafeIng && GameManage.playerSafe > 0)
        {
            //   player = GameObject.FindGameObjectWithTag("Player");
            player = PlayerController.current.playerObject;
            if (player != null)
            {
                Hero herro = player.GetComponent<Hero>();
                herro.SetSafeStatus(400);
                GameManage.playerSafe--;
                PlayerController.current.SetSafeText();
                herro = null;
            }
        }        
    }

   public void IssueMagic()
    {
        GetMagic().IssueMagic();
    }

}
