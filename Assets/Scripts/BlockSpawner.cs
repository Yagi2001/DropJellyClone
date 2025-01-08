using UnityEngine;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour
{
    private List<GameObject> _availableCubes;
    [SerializeField]
    private GameObject[] _cubePrefabs;
    [SerializeField]
    private GameObject[] _configurationPrefabs;

    private void Start()
    {
        _availableCubes = new List<GameObject>( _cubePrefabs );
        SpawnRandomBlock();
    }

    private void SpawnRandomBlock()
    {
        ResetAvailableCubes();
        int randomIndex = Random.Range( 0, _configurationPrefabs.Length );
        GameObject newBlock = Instantiate( _configurationPrefabs[randomIndex], transform.position, Quaternion.identity );
        List<GameObject> usedCubes = new List<GameObject>();

        foreach (Transform part in newBlock.transform)
        {
            GameObject randomCube = GetUniqueCubePrefab( usedCubes );
            GameObject newCube = Instantiate( randomCube, part.position, part.rotation, newBlock.transform );
            newCube.transform.localScale = part.localScale;
            Destroy( part.gameObject );
        }
        newBlock.transform.rotation = Quaternion.Euler( 0, 0, GetRandomRotation() );
    }

    // Ensures that the same color is not used more than once on the block
    private GameObject GetUniqueCubePrefab( List<GameObject> usedCubes )
    {
        int randomIndex = Random.Range( 0, _availableCubes.Count );
        GameObject selectedCube = _availableCubes[randomIndex];
        _availableCubes.RemoveAt( randomIndex );
        return selectedCube;
    }

    private float GetRandomRotation()
    {
        int randomAngle = Random.Range( 1, 5 ) * 90;
        return randomAngle;
    }

    private void ResetAvailableCubes()
    {
        _availableCubes = new List<GameObject>( _cubePrefabs );
    }
}
