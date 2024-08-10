#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private int duration;
    [SerializeField] private int targetScore;
    [SerializeField] private int column, row;
    [SerializeField] private float boardSize;
    [SerializeField] private Transform startPosition;

    public void Create()
    {
        if (CheckError())
            return;

        LevelScriptableObject level = new LevelScriptableObject
            (duration, targetScore, column, row, boardSize, startPosition.position);

        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{levelNumber}.asset");
        AssetDatabase.SaveAssets();

        Debug.Log($"'Level {levelNumber}' was created successfully.");
    }

    public void Reset()
    {
        levelNumber = 0;
        duration = 0;
        targetScore = 0;
        column = 0;
        row = 0;
        boardSize = 0;
    }

    private bool CheckError()
    {
        if (levelNumber <= 0)
        {
            Debug.LogWarning("The 'Level number' must be higher than 0!");
            return true;
        }
        if (duration <= 0)
        {
            Debug.LogWarning("The 'Duration' must be higher than 0!");
            return true;
        }
        if (targetScore <= 0)
        {
            Debug.LogWarning("The 'Target score' must be higher than 0!");
            return true;
        }
        if (column <= 0)
        {
            Debug.LogWarning("The 'column' must be higher than 0!");
            return true;
        }
        if (row <= 0)
        {
            Debug.LogWarning("The 'row' must be higher than 0!");
            return true;
        }
        if (boardSize <= 0)
        {
            Debug.LogWarning("The 'board size' must be higher than 0!");
            return true;
        }
        if (!startPosition)
        {
            Debug.LogWarning("The 'start position' cannot be null!");
            return true;
        }
        if (Resources.Load<LevelScriptableObject>("Levels/" + levelNumber))
        {
            Debug.LogWarning($"'Level {levelNumber}' already exist!");
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (!startPosition)
            return;

        Gizmos.color = Color.green;
        Vector3 size = new Vector3(boardSize / row, boardSize / column) * Mathf.Min(column, row);
        Vector3 center = startPosition.position + size / 2;
        Gizmos.DrawWireCube(center, size);
    }
}
#endif