using UnityEngine;
using System.Collections;

public class WeiFire : MonoBehaviour {

    public float speed = 4f;
    public float desSpeed = 0.01f;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, desSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
