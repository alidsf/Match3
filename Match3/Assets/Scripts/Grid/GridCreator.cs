using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private int column, row;
    [SerializeField] private float boardSize;

    public T[,] GetArray<T>(T prefab) where T : MonoBehaviour
    {
        T[,] array = new T[column, row];

        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < row; y++)
            {
                Vector2Int position = new Vector2Int(x, y);

                T newItem = InstantiateItem(prefab, position);
                array[x, y] = newItem;
            }
        }

        return array;
    }

    public T InstantiateItem<T>(T prefab, Vector2 position) where T : MonoBehaviour
    {
        float gridSize = boardSize / Mathf.Max(column, row);
        T newItem = Instantiate(prefab, transform);
        newItem.transform.localScale = Vector2.one * gridSize;
        newItem.name = $"{typeof(T)}: ({position.x}, {position.y})";
        return newItem;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(boardSize / row, boardSize / column) * Mathf.Min(column, row);
        Vector3 center = transform.position + size / 2;
        Gizmos.DrawWireCube(center, size);
    }
}
