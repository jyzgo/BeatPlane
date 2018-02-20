using UnityEngine;
using System.Collections;

public enum ButtonFlag {
    SuccessSure,
    easyLevel,
    diffLevel,
    hardLevel
};

public class ButtonEvent : MonoBehaviour {

    public ButtonFlag btnFlag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        switch (btnFlag)
        {
            case ButtonFlag.SuccessSure:
                Application.LoadLevel("Plane");
                break;
            case ButtonFlag.easyLevel:
                Application.LoadLevel("Plane");
                GameManage.gameStatus = GameStatus.Game;
                GameManage.gameLevel = 1;
                break;
            case ButtonFlag.diffLevel:
                Application.LoadLevel("Plane");
                GameManage.gameStatus = GameStatus.Game;
                GameManage.gameLevel = (int) (Random.value * 2) + 2;
                break;
            case ButtonFlag.hardLevel:
                Application.LoadLevel("Plane");
                GameManage.gameStatus = GameStatus.Game;
                GameManage.gameLevel = 4;
                break;
            default:
                break;
        }  
    }

    private void showTips(string text)
    {
        UIUtils util = GameObject.FindObjectOfType<UIUtils>();
        Vector3 v3 = transform.position;
        Vector3 pos = new Vector3(v3.x + 0.4f, v3.y + 0.7f, 0);
        util.show(text, pos);
    }

}
