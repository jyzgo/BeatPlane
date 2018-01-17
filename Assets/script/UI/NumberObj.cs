using UnityEngine;
using System.Collections;

public class NumberObj : MonoBehaviour {

    public Sprite[] icos;

    private SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    public void SetNumber(int i)
    {
        if (render == null)
        {
            render = GetComponent<SpriteRenderer>();
        }
        render.sprite = icos[i];
    }
}
