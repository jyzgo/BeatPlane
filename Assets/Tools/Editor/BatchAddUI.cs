using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EditPrefab
{
    [MenuItem("Tools/AddUI")]
    private static void AddHitData()
    {
        if (EditorUtility.DisplayDialog("Confirm",
            "Add monster hp",
            "confirm"))
        {
            string genPath = Application.dataPath + "/Prefabs/Test";
            string[] filesPath = Directory.GetFiles(genPath, "*.prefab", SearchOption.AllDirectories);

            for (int i = 0; i < filesPath.Length; i++)
            {
                Debug.Log("file " + filesPath[i]);
                filesPath[i] = filesPath[i].Substring(filesPath[i].IndexOf("Assets"));

                GameObject _prefab = AssetDatabase.LoadAssetAtPath(filesPath[i], typeof(GameObject)) as GameObject;
                GameObject c = new GameObject();
                c.AddComponent<Canvas>();
                GameObject prefabGameobject = PrefabUtility.InstantiatePrefab(_prefab) as GameObject;
                c.transform.parent = prefabGameobject.transform;
                //LeaderAction la = prefabGameobject.GetComponent<LeaderAction>();


                //if (la != null)
                //{
                //    if (la.HIT_DATA != null) continue;
                //    if (prefabGameobject.transform.FindChild("ANIMATION_DATA/HIT") == null) continue;
                //    la.HIT_DATA = prefabGameobject.transform.FindChild("ANIMATION_DATA/HIT").GetComponent<AnimationData>();
                //    PrefabUtility.ReplacePrefab(prefabGameobject, _prefab, ReplacePrefabOptions.Default);
                //    MonoBehaviour.DestroyImmediate(prefabGameobject);
                //}
                //else
                //    continue;

            }
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("success", "Add finish！", "Confirm");
        }
    }
}