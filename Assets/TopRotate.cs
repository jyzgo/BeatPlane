using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    Vector3 r = new Vector3(0, 0, 10f);
	// Update is called once per frame
	void Update () {
        transform.Rotate(r);
		
	}
}
