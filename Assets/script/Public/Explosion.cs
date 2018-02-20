using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private AudioSource audio;

    public float desTime = 100f;
    public float timer = 0f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    void FixedUpdate()
    {
        timer++;
        if (timer > desTime)
        {
            destory();
        }
    }

    public void destory()
    {
        Destroy(this.gameObject);
    }
}
