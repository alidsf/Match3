using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private ObjectPoolBase<GridItem> _pool = new ObjectPoolBase<GridItem>();
    private GridItem _gridItemPrefab;

    public void Init(GridItem prefab)
    {
        _gridItemPrefab = prefab;
        _pool.Init(_gridItemPrefab, transform, Instantiate, Destroy);
    }

    public GridItem[,] GetArray(int column, int row, float boardSize)
    {
        GridItem[,] array = new GridItem[column, row];

        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < row; y++)
            {
                GridItem newItem = GetNewGridItem(column, row, boardSize);
                array[x, y] = newItem;
            }
        }

        return array;
    }

    private GridItem GetNewGridItem(int column, int row, float boardSize)
    {
        float gridSize = boardSize / Mathf.Max(column, row);

        GridItem newItem = _pool.GetNewObject();
        newItem.transform.localScale = Vector2.one * gridSize;
        newItem.transform.localPosition = Vector3.zero;
        
        return newItem;
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Vector3 size = new Vector3(boardSize / row, boardSize / column) * Mathf.Min(column, row);
    //     Vector3 center = transform.position + size / 2;
    //     Gizmos.DrawWireCube(center, size);
    // }
}
