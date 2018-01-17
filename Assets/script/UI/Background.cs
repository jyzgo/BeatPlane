using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public int backgroundLevel = 1;

    public void show()
    {
        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

}
