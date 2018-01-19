using UnityEngine;
using System.Collections;

public class PlayerPart : MonoBehaviour {

    public int way = 1;
    public float angle = 5; // 每帧旋转角度


	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0, 0, angle * way, Space.Self);
	}
}
