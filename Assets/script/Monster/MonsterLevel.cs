using UnityEngine;
using System.Collections;

public class MonsterLevel : MonoBehaviour {

    public int monsterLevel = 1;

    public void show()
    {
        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }
}
