using UnityEngine;
using System.Collections;

public class BackgroundControll : MonoBehaviour
{

    private AudioSource audio;
    
    public GameObject[] background;
    public static Background currBackScript = null;
    private Background backScript;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (GameManage.level_up_sound)
        //{
        //    audio.Stop();
        //}
        //else if(!audio.isPlaying)
        //{
        //    audio.Play();
        //}

        backScript = getActiveBackground();
        if (null != backScript)
        {
            if (currBackScript == null)
            {
                backScript.show();
                currBackScript = backScript;
            } else if (currBackScript.backgroundLevel != backScript.backgroundLevel)
            {
                backScript.show();
                currBackScript.hide();
                currBackScript = backScript;
            }            
        }

	}

    Background getActiveBackground()
    {
        GameObject back = null;
        for (int i = 0, k = background.Length; i < k; i++ )
        {
            back = background[i];
            backScript = back.GetComponent<Background>();
            if (backScript.backgroundLevel == GameManage.gameLevel)
            {
                return backScript;
            }
        }
        return null;
    }

}
