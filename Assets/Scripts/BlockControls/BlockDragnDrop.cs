using UnityEngine;

public class BlockDragnDrop : MonoBehaviour
{
    private HighlightGrids _higlightGrids;
    private BlockMovement _blockMovement;
    private bool _isDraggable = true;
    private Camera _mainCamera;
    private Vector3 _mouseOffset;
    private GameObject[] _gridGroups;
    private void Start()
    {
        _mainCamera = Camera.main;
        _blockMovement = GetComponent<BlockMovement>();
        _higlightGrids = FindObjectOfType<HighlightGrids>();
    }
    // This DragnDrop Logic Needs Improvements
    private void OnMouseDown()
    {
        _gridGroups = GameObject.FindGameObjectsWithTag( "GridGroup" );
        _mouseOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (_isDraggable)
        {
            _higlightGrids.isDragging = true;
            _higlightGrids.block = gameObject;
            Vector3 newPosition = transform.position;
            newPosition.x = GetMouseWorldPosition().x + _mouseOffset.x;
            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        _higlightGrids.isDragging = false;
        _higlightGrids.block = null;
        GameObject closestGridGroup = FindMinimumDistance();
        Vector3 newPosition = transform.position;
        newPosition.x = closestGridGroup.transform.position.x;
        transform.position = newPosition;
        _blockMovement.FindPositionOfAvailableGrid( closestGridGroup );
        _isDraggable = false;
        BlockSpawner.BlocksSettled?.Invoke();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return _mainCamera.ScreenToWorldPoint( mousePosition );
    }

    //Very similar function in HighlightGrids. Might need a fix later!
    private GameObject FindMinimumDistance()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestGridGroup = null;
        foreach (var gridGroup in _gridGroups)
        {
            float distance = Mathf.Abs( transform.position.x - gridGroup.transform.position.x );
            if (distance < minDistance)
            {
                minDistance = distance;
                closestGridGroup = gridGroup;
            }
        }
        return closestGridGroup;
    }
}