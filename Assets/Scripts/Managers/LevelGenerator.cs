using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] _allGrids;
    [SerializeField]
    private GameObject _levelObject;
    [SerializeField]
    private GameObject[] _prefabs;

    void Start()
    {
        _allGrids = GameObject.FindGameObjectsWithTag( "Grid" );
        PlacePrefabsOnGrids();
    }

    private void PlacePrefabsOnGrids()
    {
        for (int i = 0; i <= 30; i += 6)
        {
            GameObject grid = _allGrids[i];
            int prefabIndex = (i / 6) % 2;
            GameObject selectedPrefab = _prefabs[prefabIndex];
            GameObject instantiatedPrefab = Instantiate( selectedPrefab, grid.transform.position, Quaternion.identity );
            instantiatedPrefab.transform.SetParent( _levelObject.transform );

            GridInfo gridInfo = grid.GetComponent<GridInfo>();
            if (gridInfo != null)
            {
                gridInfo.AttachBlocksToPositions( instantiatedPrefab );
                gridInfo.isOccupied = true;
            }
        }

        for (int i = 7; i < _allGrids.Length; i += 6)
        {
            GameObject grid = _allGrids[i];
            int prefabIndex = ((i - 7) / 6) % 2 == 0 ? 0 : 1;
            GameObject selectedPrefab = _prefabs[prefabIndex];
            GameObject instantiatedPrefab = Instantiate( selectedPrefab, grid.transform.position, Quaternion.identity );
            instantiatedPrefab.transform.SetParent( _levelObject.transform );
            GridInfo gridInfo = grid.GetComponent<GridInfo>();
            if (gridInfo != null)
            {
                gridInfo.AttachBlocksToPositions( instantiatedPrefab );
                gridInfo.isOccupied = true;
            }
        }
    }
}
