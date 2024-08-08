using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private int _gridItemNumber;
    public int gridItemNumber { get { return _gridItemNumber; } }
    private float _speed = 10f;

    private Vector2Int _targetPosition;
    public Vector2Int targetPosition { get { return _targetPosition; } }

    private bool _isMoving;
    public bool isMoving { get { return _isMoving; } }

    public void SetRandomGridItem()
    {
        int randomNumber = Random.Range(0, objects.Length);
        SetGridItem(randomNumber);
    }

    public void SetTarget(Vector2Int targetPosition) =>
        _targetPosition = targetPosition;

    public void SetGridItem(int gridItemNumber)
    {
        int maxNumber = objects.Length - 1;
        _gridItemNumber = (gridItemNumber > maxNumber) ? maxNumber : gridItemNumber;

        for (int i = 0; i < objects.Length; i++)
            objects[i].SetActive(false);

        objects[_gridItemNumber].SetActive(true);
    }

    public void Move()
    {
        Vector3 target = (Vector2)_targetPosition * transform.localScale;

        if (Vector2.Distance(transform.localPosition, target) > 0.05f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, _speed * Time.deltaTime);
            _isMoving = true;
        }
        else
        {
            transform.localPosition = target;
            _isMoving = false;
        }
    }
}
