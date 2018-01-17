using UnityEngine;
using System.Collections;

public class sonPlane : MonoBehaviour {

    private SpriteRenderer render;

    public Sprite sprite;

	// Use this for initialization
	void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if(false){
            render.sprite = sprite;
        }
        else
        {
            render.sprite = null;
        }             
        */
	}
}
