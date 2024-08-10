using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/NewLevel", order = 0)]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] private int duration;
    [SerializeField] private int targetScore;
    [SerializeField] private int column, row;
    [SerializeField] private float boardSize;
    [SerializeField] Vector2 startPosition;

    public int Duration { get { return duration; } }
    public int TargetScore { get { return targetScore; } }
    public int Column { get { return column; } }
    public int Row { get { return row; } }
    public float BoardSize { get { return boardSize; } }
    public Vector2 StartPosition { get { return startPosition; } }
}
