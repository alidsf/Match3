using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/NewLevel", order = 0)]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] private int duration;
    [SerializeField] private int targetScore;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private float boardSize;
    [SerializeField] private Vector2 startPosition;

    public int Duration { get { return duration; } }
    public int TargetScore { get { return targetScore; } }
    public int Column { get { return column; } }
    public int Row { get { return row; } }
    public float BoardSize { get { return boardSize; } }
    public Vector2 StartPosition { get { return startPosition; } }

    public LevelScriptableObject(int duration, int targetScore, int column, int row, float boardSize, Vector2 startPosition)
    {
        this.duration = duration;
        this.targetScore = targetScore;
        this.column = column;
        this.row = row;
        this.boardSize = boardSize;
        this.startPosition = startPosition;
    }
}
