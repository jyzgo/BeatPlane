using UnityEngine;
using System.Collections;

public class OutFire : MonoBehaviour {

    private float speed = 0.1f;
    public float rate = 0.1f;

    public GameObject bullet;

    // Use this for initialization
    void Start()
    {
        startFire();
    }

    public void fire()
    {
        GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
    }

    private void startFire()
    {
        InvokeRepeating("fire", speed, rate);
    }
}
