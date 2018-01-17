using UnityEngine;
using System.Collections;

public class Utils {

    public static GameObject getTargetMonster(Vector3 pos)
    {
        GameObject monster = null;
        ArrayList monsterList = new ArrayList();
        Monster monScript;
        foreach (DictionaryEntry de in GameManage.monsterMap)
        {
            monster = (GameObject)de.Value;
            if (monster != null)
            {
                monScript = monster.GetComponent<Monster>();
                if (monster.transform.position.y > -3.7 && !monScript.issafe)
                {
                    monsterList.Add(monster);
                }
            }
        }

        if (monsterList.Count > 0)
        {
            GameObject temp1 = null, temp2 = null;
            float len1 = 0, len2 = 0;
            float x1 = pos.x;
            float y1 = pos.y;
            Vector3 mv3 = Vector3.zero;
            for (int i = 0, k = monsterList.Count; i < k; i++)
            {
                temp1 = (GameObject)monsterList[i];
                mv3 = temp1.transform.position;
                len1 = executeLength(x1, y1, mv3.x, mv3.y);
                for (int j = i, p = monsterList.Count; j < p; j++)
                {
                    temp2 = (GameObject)monsterList[j];
                    mv3 = temp2.transform.position;
                    len2 = executeLength(x1, y1, mv3.x, mv3.y);

                    if (len1 > len2)
                    {
                        monsterList[i] = temp2;
                        monsterList[j] = temp1;
                    }

                }
            }

            return (GameObject)monsterList[0];
        }
        return null;
    }


    static float executeLength(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Abs(x2 - x1) * Mathf.Abs(x2 - x1) + Mathf.Abs(y2 - y1) * Mathf.Abs(y2 - y1));
    }


    public static GameObject getPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// 由角度计算弧度
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float getRadianByAngle(float angle)
    {
        return 2 * Mathf.PI / 360 * angle;
    }

    /// <summary>
    /// 已知角度与x/y其中一边，计算另一边坐标
    /// </summary>
    /// <param name="y"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float getXPixByYPixAngle(float y, float angle)
    {
        angle = getRadianByAngle(angle); // 由角度计算弧度
        float x = y / Mathf.Tan(angle);
        return x;
    }

    /// <summary>
    /// 已知x、y坐标，计算角度
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static float getAngleByXY(float x, float y)
    {
        float tan = Mathf.Atan(y / x) * 180 / Mathf.PI;
        if (0 > x && 0 > y)//第一象限
        {
            return -tan;
        }
        else if (0 > x && 0 < y)//第二象限
        {
            return tan;
        }
        else if (0 < x && 0 > y)//第三象限
        {
            return tan - 180;
        }
        else
        {
            return 180 - tan;
        }
    }


    /// <summary>
    ///     计算旋转角度
    /// </summary>
    /// <param name="nowpoint"></param>
    /// <returns></returns>
    public static float ComputeAngle(Vector3 CentPoint, Vector3 nowpoint)
    {
        //斜边长度
        float length = PointLegth(nowpoint, CentPoint);
        //对边比斜边 sin
        float hudu = Mathf.Asin(Mathf.Abs(nowpoint.y - CentPoint.y) / length);
        float ag = hudu * 180 / Mathf.PI;
        //第2象限90-
        if ((CentPoint.x - nowpoint.x) <= 0 && (CentPoint.y - nowpoint.y) >= 0){
            ag = 0 -ag;
            //Debug.Log("--2--" + ag);
        }
        //第3象限90+
        else if ((CentPoint.x - nowpoint.x) <= 0 && (CentPoint.y - nowpoint.y) <= 0){
            //Debug.Log("--3--" + ag);
        }
        //第4象限270-
        else if ((CentPoint.x - nowpoint.x) >= 0 && (CentPoint.y - nowpoint.y) <= 0){            
            ag = 180 - ag;
            //Debug.Log("--4--" + ag);
        }

        //第1象限270+
        else if ((CentPoint.x - nowpoint.x) >= 0 && (CentPoint.y - nowpoint.y) >= 0)
        {
            ag = ag + 180;
            //Debug.Log("--1--" + ag);
        }

        return ag;
    }

    /// <summary>
    ///     计算两点间距离
    /// </summary>
    /// <param name="pa"></param>
    /// <param name="pb"></param>
    /// <returns></returns>
    public static float PointLegth(Vector3 pa, Vector3 pb)
    {
        return Mathf.Sqrt(Mathf.Pow((pa.x - pb.x), 2) + Mathf.Pow((pa.y - pb.y), 2));
    }

    public static string fillScore0(int value)
    {
        string result = "" + value;
        for (int i = 0, k = (8 - result.Length); i < k; i++)
        {
            result = "0" + result;
        }
        return result;
    }

    public static string fillTime0(int value)
    {
        string result = "" + value;
        if (value < 10)
        {
            result = "0" + value;
        }
        return result;
    }

    public static GUIStyle createStyle(Color color, int fontSize){
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null; //这是设置背景填充的
        bb.normal.textColor = color == null ? new Color(1, 1, 1) : color; //设置字体颜色的    
        bb.fontSize = fontSize == 0 ? 20 : fontSize;  //当然，这是字体颜色
        return bb;
    }

}
