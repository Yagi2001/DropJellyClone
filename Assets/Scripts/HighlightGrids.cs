using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGrids : MonoBehaviour
{
    private GameObject[] _gridGroups;
    private GameObject _previousHighlightedGroup;
    public GameObject block;
    public bool isDragging = false;
    void Start()
    {
        _gridGroups = _gridGroups = GameObject.FindGameObjectsWithTag( "GridGroup" );
    }

    private void Update()
    {
        if (isDragging)
            HighlightGroup( FindMinimumDistance() );
        else
            UnhighlightGroup( _previousHighlightedGroup );
    }

    private GameObject FindMinimumDistance()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestGridGroup = null;
        foreach (var gridGroup in _gridGroups)
        {
            float distance = Mathf.Abs( block.transform.position.x - gridGroup.transform.position.x );
            if (distance < minDistance)
            {
                minDistance = distance;
                closestGridGroup = gridGroup;
            }
        }
        return closestGridGroup;
    }

    private void HighlightGroup( GameObject gridGroup )
    {
        if (_previousHighlightedGroup != gridGroup)
            UnhighlightGroup( _previousHighlightedGroup );
        SpriteRenderer[] spriteRenderers = gridGroup.GetComponentsInChildren<SpriteRenderer>();

        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.red;
        }
        _previousHighlightedGroup = gridGroup;
    }

    private void UnhighlightGroup( GameObject gridGroup )
    {
        if( gridGroup != null)
        {
            SpriteRenderer[] spriteRenderers = gridGroup.GetComponentsInChildren<SpriteRenderer>();

            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
