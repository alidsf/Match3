using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : PooledObject<GridItem>
{
    [SerializeField] private GameObject itemObject;
    [SerializeField] private GameObject[] objects;

    private int _gridItemNumber;
    public int gridItemNumber { get { return _gridItemNumber; } }

    private Vector2Int _targetPosition;
    public Vector2Int targetPosition { get { return _targetPosition; } }

    private bool _isMoving;
    public bool isMoving { get { return _isMoving; } }

    public bool isEnable { get { return itemObject.activeSelf; } }

    private float _speed = 8f;

    public void SetTarget(Vector2Int targetPosition) =>
            _targetPosition = targetPosition;

    public void SetRandomGridItem()
    {
        int randomNumber = Random.Range(0, objects.Length);
        SetGridItem(randomNumber);
    }

    private void SetGridItem(int gridItemNumber)
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

        if (Vector2.Distance(transform.localPosition, target) > 0.1f)
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

    public void SetNewPosition()
    {
        SetEnable(true);
        SetRandomGridItem();
        SetFallPosition();
    }

    public void SetEnable(bool value)
    {
        itemObject.SetActive(value);

        if (!value)
            Blast();
    }

    private void Blast() =>
        BlastParticleManager.Instance.Blast(_gridItemNumber,
                objects[_gridItemNumber].transform.position, transform.localScale);

    private void SetFallPosition()
    {
        int height = targetPosition.y;
        Vector3 fallPosition = Vector2.up * height;
        transform.localPosition += fallPosition;
    }

    public void Reset()
    {
        Blast();
        _release(this);
    }
}
