using UnityEngine;
using System.Collections;

public class MagicIcon : MonoBehaviour {

    public Sprite[] sprite;

    private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (GameManage.magic2)
        {
            case MagicType.Magic1:
                spriteRender.sprite = sprite[0];
                break;
            case MagicType.Magic2:
                spriteRender.sprite = sprite[1];
                break;
            case MagicType.Magic3:
                spriteRender.sprite = sprite[2];
                break;
            case MagicType.Magic4:
                spriteRender.sprite = sprite[3];
                break;
            case MagicType.Magic5:
                spriteRender.sprite = sprite[4];
                break;
            default:
                break;
        }
	}
}
