using UnityEngine;
using System.Collections;

public class MonsterControl : MonoBehaviour {

    public GameObject[] levelSence;

    public static MonsterLevel currLevelScript = null;
    private MonsterLevel levelScript;

    void FixedUpdate()
    {

        levelScript = getActiveBackground();
        if (null != levelScript)
        {
            if (currLevelScript == null)
            {
                levelScript.show();
                currLevelScript = levelScript;
            }
            else if (currLevelScript.monsterLevel != levelScript.monsterLevel)
            {
                levelScript.show();
                currLevelScript.hide();
                currLevelScript = levelScript;
            }
        }

    }

    MonsterLevel getActiveBackground()
    {
        GameObject back = null;
        for (int i = 0, k = levelSence.Length; i < k; i++)
        {
            back = levelSence[i];
            levelScript = back.GetComponent<MonsterLevel>();
            if (levelScript.monsterLevel == GameManage.gameLevel)
            {
                return levelScript;
            }
        }
        return null;
    }
}
