using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTouchManager
{
    public static void SelectGridItem(ref GridItem firstGridItem, ref GridItem secondGridItem, LayerMask layerMask, Action move)
    {
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
                    move();
                else
                    firstGridItem = null;
            }
        }
    }

    private static bool CheckPositionValidity(Vector2 firstPosition, Vector2 secondPosition)
    {
        if (firstPosition == secondPosition - Vector2.up // check Up
           || firstPosition == secondPosition + Vector2.up // check Down
           || firstPosition == secondPosition - Vector2.right // check Right
           || firstPosition == secondPosition + Vector2.right) // check Left
            return true;

        return false;
    }
}
