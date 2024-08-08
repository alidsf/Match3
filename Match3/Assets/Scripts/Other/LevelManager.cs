using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private void Start()
    {
        gridManager.Init();
    }
}
