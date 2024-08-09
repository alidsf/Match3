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

    public void Init()
    {
        _gridItems = gridCreator.GetArray(gridItemPrefab);

        for (int x = 0; x < _gridItems.GetLength(0); x++)
        {
            for (int y = 0; y < _gridItems.GetLength(1); y++)
            {
                _gridItems[x, y].SetTarget(new Vector2Int(x, y));
                _gridItems[x, y].SetRandomGridItem();
            }
        }

        Match3Checker.SetWithoutMatch(ref _gridItems);
    }

    private void Update()
    {
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

        ResetGrideItems(matchList);
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

    private void ResetGrideItems(List<GridItem> matchList)
    {
        for (int i = 0; i < matchList.Count; i++)
            matchList[i].Reset();

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
        if (!Input.GetMouseButton(0) || !_allMovesDone || !_mouseUp)
            return;

        GridTouchManager.SelectGridItem(ref firstGridItem, ref secondGridItem, layerMask, NewMove);
    }

    private void NewMove()
    {
        ChangeGridItemPosition(firstGridItem, secondGridItem);
        _canCheckMatch3 = true;
    }
}
