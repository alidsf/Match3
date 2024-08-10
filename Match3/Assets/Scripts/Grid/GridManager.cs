using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GridItem gridItemPrefab;
    [SerializeField] private GridCreator gridCreator;

    private GridItem[,] _gridItems;
    private GridItem firstGridItem = null;
    private GridItem secondGridItem = null;
    private bool _mouseUp = true;
    private bool _allMovesDone;
    private bool _canCheckMatch3;

    private int _numberOfMatches;
    public int numberOfMatches { get { return _numberOfMatches; } }

    private bool _isInitialized;

    public void Init(int column, int row, float boardSize, Vector2 startPosition)
    {
        transform.position = startPosition;

        gridCreator.Init(gridItemPrefab);
        _gridItems = gridCreator.GetArray(column, row, boardSize);

        for (int x = 0; x < _gridItems.GetLength(0); x++)
        {
            for (int y = 0; y < _gridItems.GetLength(1); y++)
            {
                _gridItems[x, y].SetTarget(new Vector2Int(x, y));
                _gridItems[x, y].SetRandomGridItem();
            }
        }

        Match3Checker.SetWithoutMatch(ref _gridItems);

        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        if (!Input.GetMouseButton(0))
            _mouseUp = true;

        Move();
        CheckMatch3();
    }

    private void Move()
    {
        for (int x = 0; x < _gridItems.GetLength(0); x++)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                _gridItems[x, y].Move();

        _allMovesDone = CheckAllMovesDone();
    }

    private bool CheckAllMovesDone()
    {
        for (int x = 0; x < _gridItems.GetLength(0); x++)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                if (_gridItems[x, y].isMoving)
                    return false;

        return true;
    }

    private void CheckMatch3()
    {
        if (!_allMovesDone || !_canCheckMatch3)
            return;

        _canCheckMatch3 = false;

        Match3Checker.CheckMatch(_gridItems, RemoveMatches,
            () => ChangeGridItemPosition(firstGridItem, secondGridItem));

        firstGridItem = null;
    }

    private void RemoveMatches(List<GridItem> matchList)
    {
        for (int i = 0; i < matchList.Count; i++)
            matchList[i].SetEnable(false);

        for (int x = _gridItems.GetLength(0) - 1; x >= 0; x--)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                if (_gridItems[x, y].isEnable)
                    MoveDown(_gridItems[x, y]);

        _numberOfMatches += matchList.Count;
        SetNewPositionGrideItems(matchList);
    }

    private void MoveDown(GridItem currentGridItem)
    {
        int startYIndex = currentGridItem.targetPosition.y - 1;
        int startXIndex = currentGridItem.targetPosition.x;

        for (int i = startYIndex; i >= 0; i--)
        {
            GridItem targetGrideItem = _gridItems[startXIndex, i];

            if (!targetGrideItem.isEnable)
                ChangeGridItemPosition(currentGridItem, targetGrideItem);
            else
                break;
        }
    }

    private void SetNewPositionGrideItems(List<GridItem> matchList)
    {
        for (int i = 0; i < matchList.Count; i++)
            matchList[i].SetNewPosition();

        _canCheckMatch3 = true;
    }

    private void ChangeGridItemPosition(GridItem first, GridItem second)
    {
        if (!first || !second)
            return;

        Vector2Int firstPosition = first.targetPosition;
        Vector2Int secondPosition = second.targetPosition;

        first.SetTarget(secondPosition);
        second.SetTarget(firstPosition);

        _gridItems[firstPosition.x, firstPosition.y] = second;
        _gridItems[secondPosition.x, secondPosition.y] = first;

        _mouseUp = false;
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButton(0) || !_allMovesDone ||
            !_mouseUp || !_isInitialized)
            return;

        GridTouchManager.SelectGridItem(ref firstGridItem, ref secondGridItem, layerMask, NewMove);
    }

    private void NewMove()
    {
        ChangeGridItemPosition(firstGridItem, secondGridItem);
        _canCheckMatch3 = true;
    }

    public void Reset()
    {
        if (!_isInitialized)
            return;

        for (int x = _gridItems.GetLength(0) - 1; x >= 0; x--)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                if (_gridItems[x, y].isEnable)
                    _gridItems[x, y].Reset();

        _numberOfMatches = 0;
        _isInitialized = false;
    }
}
