using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private int column, row;
    [SerializeField] private float gridSize;

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
        T newItem = Instantiate(prefab, transform);
        newItem.transform.localPosition = position * gridSize;
        newItem.transform.localScale = Vector2.one * gridSize;
        newItem.name = $"{typeof(T)}: ({position.x}, {position.y})";
        return newItem;
    }
}
