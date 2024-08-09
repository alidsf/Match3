using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Checker : MonoBehaviour
{
    public static void SetWithoutMatch(ref GridItem[,] gridItems)
    {
        List<GridItem> matchList = GetMatch3(gridItems);

        while (matchList.Count > 0)
        {
            for (int i = 0; i < matchList.Count; i++)
                matchList[i].SetRandomGridItem();

            matchList = GetMatch3(gridItems);
        }
    }

    public static void CheckMatch(GridItem[,] gridItems, Action<List<GridItem>> matchAction, Action notMatchAction)
    {
        List<GridItem> matchList = GetMatch3(gridItems);

        if (matchList.Count > 0)
            matchAction(matchList);
        else
            notMatchAction();
    }

    private static List<GridItem> GetMatch3(GridItem[,] gridItems)
    {
        List<GridItem> matchList = new List<GridItem>();

        GridItem[,] rows = new GridItem[gridItems.GetLength(0), gridItems.GetLength(1)];
        GridItem[,] columns = new GridItem[gridItems.GetLength(1), gridItems.GetLength(0)];

        for (int x = 0; x < rows.GetLength(0); x++)
            for (int y = 0; y < rows.GetLength(1); y++)
                rows[x, y] = gridItems[x, y];

        for (int x = 0; x < columns.GetLength(0); x++)
            for (int y = 0; y < columns.GetLength(1); y++)
                columns[x, y] = gridItems[y, x];

        matchList.AddRange(GetMatch3List(rows));
        matchList.AddRange(GetMatch3List(columns));

        return matchList;
    }

    private static List<GridItem> GetMatch3List(GridItem[,] gridItems)
    {
        List<GridItem> currentList = new List<GridItem>();

        for (int x = 0; x < gridItems.GetLength(0); x++)
        {
            List<GridItem> matchGrids = new List<GridItem>();

            for (int y = 0; y < gridItems.GetLength(1); y++)
            {
                GridItem currentGrid = gridItems[x, y];

                if (matchGrids.Count == 0)
                    matchGrids.Add(currentGrid);
                else
                {
                    bool isLastGrid = y == gridItems.GetLength(1) - 1;
                    bool isMatch = matchGrids[0].gridItemNumber == currentGrid.gridItemNumber;

                    if (isMatch)
                        matchGrids.Add(currentGrid);
                    if (!isMatch || isLastGrid)
                    {
                        if (matchGrids.Count >= 3)
                            currentList.AddRange(matchGrids);

                        matchGrids.Clear();
                        matchGrids.Add(currentGrid);
                    }
                }
            }
        }

        return currentList;
    }
}
