using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class FrameBuilder : EditorWindow
{
    [MenuItem("Tools/Frame Builder")]
    public static void ShowWindow()
    {
        GetWindow<FrameBuilder>("Frame Builder");
    }

    GameObject BlockPrefab, ParentGo;
    int countX = 40;
    int countY = 30;
    float spacing = 1.0f;

    void OnGUI()
    {
        GUILayout.Label("Prefab Spawner", EditorStyles.boldLabel);
        BlockPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", BlockPrefab, typeof(GameObject), false);
        ParentGo = (GameObject)EditorGUILayout.ObjectField("Parent GameObject", ParentGo, typeof(GameObject), true);
        countX = EditorGUILayout.IntField("Count X", countX);
        countY = EditorGUILayout.IntField("Count Y", countY);
        spacing = EditorGUILayout.FloatField("Spacing", spacing);

        if (GUILayout.Button("Build Frame"))
        {
            BuildFrame();
        }
    }

    void BuildFrame()
    {
        if (BlockPrefab == null)
        {
            Debug.LogError("Prefab is null. Please assign a prefab.");
            return;
        }
        for (int x = 0; x < countX; x++)
        {
            (PrefabUtility.InstantiatePrefab(BlockPrefab, ParentGo.transform) as GameObject).transform.SetPositionAndRotation(new Vector3(x, 0), Quaternion.identity);
            (PrefabUtility.InstantiatePrefab(BlockPrefab, ParentGo.transform) as GameObject).transform.SetPositionAndRotation(new Vector3(x, countY - 1), Quaternion.identity);
        }
        for (int y = 1; y < countY; y++)
        {
            (PrefabUtility.InstantiatePrefab(BlockPrefab, ParentGo.transform) as GameObject).transform.SetPositionAndRotation(new Vector3(0, y), Quaternion.identity);
            (PrefabUtility.InstantiatePrefab(BlockPrefab, ParentGo.transform) as GameObject).transform.SetPositionAndRotation(new Vector3(countX - 1, y), Quaternion.identity);
        }
    }
}