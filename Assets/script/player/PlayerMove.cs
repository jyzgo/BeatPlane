using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float speed = 8;
    private bool isMouseDown = false;
    private Vector3 lastPosition = Vector3.zero;
    private Vector3 offsetPos = Vector3.zero;

    private Vector3 newPos = Vector3.zero;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
       
        if(Input.GetMouseButtonDown(0)){
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            offsetPos = Vector3.zero;
            lastPosition = Vector3.zero;
        }

        if (isMouseDown)
        {
            if(lastPosition != Vector3.zero){

                offsetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastPosition;
                newPos = transform.position + offsetPos;
                if (newPos.x > 1.9f)
                {
                    newPos = new Vector3(1.9f, newPos.y, newPos.z);
                }
                else if (newPos.x < -1.9f)
                {
                    newPos = new Vector3(-1.9f, newPos.y, newPos.z);
                }

                if (newPos.y > 3.2f)
                {
                    newPos = new Vector3(newPos.x, 3.2f, newPos.z);
                }
                else if (newPos.y < -3.5f)
                {
                    newPos = new Vector3(newPos.x, -3.5f, newPos.z);
                }

                transform.position = newPos;
            }

            lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

	}
}
