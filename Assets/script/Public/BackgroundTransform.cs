using UnityEngine;
using System.Collections;

public class BackgroundTransform : MonoBehaviour {

    public float backLength = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.down * GameManage.bgMoveSpeed * Time.deltaTime);

        Vector3 pos = this.transform.position;

        if (pos.y <= -backLength)
        {
            this.transform.position = new Vector3(pos.x, pos.y + backLength * 2, pos.z);
            
        }
	}
}
