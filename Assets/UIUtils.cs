using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIUtils : MonoBehaviour {

    public GameObject text_tip;
    public Text tip_text;

    private int timer = 0;
    private int timerInterval = 100;

    void FixedUpdate()
    {
        if (timer > 0 && timer < timerInterval)
        {
            timer++;
            if (timer == timerInterval)
            {
                hide();
            }
        }
    }

    public void show(string text, Vector3 pos)
    {
        text_tip.SetActive(true);
        text_tip.transform.position = pos;
        tip_text.text = text;
        timer = 1;
    }

    private void hide()
    {
        if (text_tip != null)
        {
            text_tip.SetActive(false);
            timer = 0;
        }
    }
	
}
