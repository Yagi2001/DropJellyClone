using UnityEngine;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject[] _cubePrefabs;
    [SerializeField]
    private GameObject[] _configurationPrefabs;

    private void Start()
    {
        SpawnRandomBlock();
    }

    private void SpawnRandomBlock()
    {
        int randomIndex = Random.Range( 0, _configurationPrefabs.Length );
        GameObject newBlock = Instantiate( _configurationPrefabs[randomIndex], _spawnPoint.position, Quaternion.identity );
        List<GameObject> usedCubes = new List<GameObject>();

        foreach (Transform part in newBlock.transform)
        {
            GameObject randomCube = GetUniqueCubePrefab( usedCubes );
            GameObject newCube = Instantiate( randomCube, part.position, part.rotation );
            newCube.transform.localScale = part.localScale;
            Destroy( part.gameObject );
        }
    }

    private GameObject GetUniqueCubePrefab( List<GameObject> usedCubes )
    {
        GameObject randomCube;
        do
        {
            randomCube = _cubePrefabs[Random.Range( 0, _cubePrefabs.Length )];
        } while (usedCubes.Contains( randomCube ));
        usedCubes.Add( randomCube );
        return randomCube;
    }
}
