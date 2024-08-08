using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private Grid gridPrefab;
    [SerializeField] private GridItem gridItemPrefab;
    [SerializeField] private GridCreator gridCreator;

    private Grid[,] _grids;
    private GridItem[,] _gridItems;

    [SerializeField] private LayerMask layerMask;
    private GridItem firstGridItem = null;
    private GridItem secondGridItem = null;
    bool _canNextMove = true;

    private void Move()
    {
        for (int x = 0; x < _gridItems.GetLength(0); x++)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                _gridItems[x, y].Move();
    }

    private bool CheckAllMovesDone()
    {
        for (int x = 0; x < _gridItems.GetLength(0); x++)
            for (int y = 0; y < _gridItems.GetLength(1); y++)
                if (_gridItems[x, y].isMoving)
                    return false;

        return true;
    }

    private void FixedUpdate()
    {
        Move();

        if (CheckAllMovesDone() && !_canNextMove)
        {
            List<GridItem> matchList = Match3Checker.GetMatch3(_gridItems);
            if (matchList.Count > 0)
            {
                print(matchList.Count);
            }
            else
            {
                ChangeSelectedGridItemPosition();
                firstGridItem = null;
            }
        }

        if (!Input.GetMouseButton(0))
            _canNextMove = true;

        if (!Input.GetMouseButton(0) || !CheckAllMovesDone() || !_canNextMove)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, layerMask);

        if (raycastHit2D)
        {
            GridItem currentGridItem = raycastHit2D.collider.GetComponent<GridItem>();

            if (!firstGridItem)
                firstGridItem = currentGridItem;
            else if (firstGridItem != currentGridItem)
            {
                secondGridItem = currentGridItem;

                if (CheckPositionValidity(firstGridItem.targetPosition, secondGridItem.targetPosition))
                    ChangeSelectedGridItemPosition();
                else
                    firstGridItem = null;
            }
        }
    }

    private void ChangeSelectedGridItemPosition()
    {
        if (!firstGridItem)
            return;

        Vector2Int firstPosition = firstGridItem.targetPosition;
        Vector2Int secondPosition = secondGridItem.targetPosition;

        firstGridItem.SetTarget(secondPosition);
        secondGridItem.SetTarget(firstPosition);

        _gridItems[firstPosition.x, firstPosition.y] = secondGridItem;
        _gridItems[secondPosition.x, secondPosition.y] = firstGridItem;

        _canNextMove = false;
    }

    private bool CheckPositionValidity(Vector2 firstPosition, Vector2 secondPosition)
    {
        if (firstPosition == secondPosition - Vector2.up // check Up
           || firstPosition == secondPosition + Vector2.up // check Down
           || firstPosition == secondPosition - Vector2.right // check Right
           || firstPosition == secondPosition + Vector2.right) // check Left
            return true;

        return false;
    }

    public void Init()
    {
        _grids = gridCreator.GetArray(gridPrefab);
        _gridItems = gridCreator.GetArray(gridItemPrefab);

        for (int x = 0; x < _gridItems.GetLength(0); x++)
        {
            for (int y = 0; y < _gridItems.GetLength(1); y++)
            {
                _gridItems[x, y].SetTarget(new Vector2Int(x, y));
                _gridItems[x, y].SetRandomGridItem();
            }
        }

        List<GridItem> matchList = Match3Checker.GetMatch3(_gridItems);

        while (matchList.Count > 0)
        {
            for (int i = 0; i < matchList.Count; i++)
                matchList[i].SetRandomGridItem();

            matchList = Match3Checker.GetMatch3(_gridItems);
        }
    }
}
