using UnityEngine;
using System.Collections;

public class HpText : MonoBehaviour {

    private Monster mons;

	// Use this for initialization
	void Start () {
        //hplineTransform = hpline.transform;
        mons = GetComponentInParent<Monster>();
	}

    void OnGUI()
    {
        Vector3 v3 = transform.position;
        GUIStyle bb = Utils.createStyle(Color.white, 20);
        Debug.Log(v3.x + ":" + v3.y);
        GUI.Label(new Rect(v3.x, v3.y, 100, 10), mons.hp + "", bb);
    }


}
