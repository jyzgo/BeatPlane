using UnityEngine;
using System.Collections;

public class BornBaby : MonoBehaviour {

    public GameObject monster;

    public int number = 5;
    
    public void Born(Vector3 v3)
    {
        Vector3 sonV3 = v3;
        for (int i = 0; i < number; i++ )
        {
            sonV3 = new Vector3(i * 0.7f - 1.4f, v3.y, v3.z);
            GameObject.Instantiate(monster, sonV3, Quaternion.identity);
        }
    }

}
