using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class BlockSpawner : MonoBehaviour
{
    public static Action BlocksSettled;
    private List<GameObject> _availableCubes;
    [SerializeField]
    private GameObject[] _cubePrefabs;
    [SerializeField]
    private GameObject[] _configurationPrefabs;
    [SerializeField]
    private Transform _blockPosition;

    private void OnEnable()
    {
        BlocksSettled += SpawnRandomBlock;
    }

    private void OnDisable()
    {
        BlocksSettled -= SpawnRandomBlock;
    }
    private void Start()
    {
        _availableCubes = new List<GameObject>( _cubePrefabs );
        SpawnRandomBlock();
    }

    private void SpawnRandomBlock()
    {
        ResetAvailableCubes();
        int randomIndex = UnityEngine.Random.Range( 0, _configurationPrefabs.Length );
        GameObject newBlock = Instantiate( _configurationPrefabs[randomIndex], transform.position, Quaternion.identity );

        // Create a static list of original children
        List<Transform> originalChildren = new List<Transform>();
        foreach (Transform child in newBlock.transform)
        {
            originalChildren.Add( child );
        }

        foreach (Transform part in originalChildren)
        {
            GameObject randomCube = GetUniqueCubePrefab();
            GameObject newCube = Instantiate( randomCube, part.position, part.rotation, newBlock.transform );
            newCube.transform.localScale = part.localScale;
            Destroy( part.gameObject );
        }
        StartCoroutine( MoveToYPosition( newBlock.transform, _blockPosition.position.y ) );
    }

    private IEnumerator MoveToYPosition( Transform target, float targetY )
    {
        float duration = 0.3f;
        float elapsedTime = 0f;

        Vector3 startPosition = target.position;
        Vector3 targetPosition = new Vector3( startPosition.x, targetY, startPosition.z );

        while (elapsedTime < duration)
        {
            target.position = Vector3.Lerp( startPosition, targetPosition, elapsedTime / duration );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.position = targetPosition;
    }

    // Ensures that the same color is not used more than once on the block
    // This needs fix
    private GameObject GetUniqueCubePrefab()
    {
        int randomIndex = UnityEngine.Random.Range( 0, _availableCubes.Count );
        GameObject selectedCube = _availableCubes[randomIndex];
        _availableCubes.RemoveAt( randomIndex );
        return selectedCube;
    }

    private float GetRandomRotation()
    {
        int randomAngle = UnityEngine.Random.Range( 1, 5 ) * 90;
        return randomAngle;
    }

    private void ResetAvailableCubes()
    {
        _availableCubes = new List<GameObject>( _cubePrefabs );
    }
}