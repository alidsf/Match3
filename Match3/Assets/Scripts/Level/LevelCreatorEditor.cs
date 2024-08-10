#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelCreator levelCreator = (LevelCreator)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create Level"))
        {
            levelCreator.Create();
        }

        if (GUILayout.Button("Clear"))
        {
            levelCreator.Reset();
        }

        GUILayout.EndHorizontal();
    }
}
#endif