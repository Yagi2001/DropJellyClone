using UnityEngine;

public class BlockDragnDrop : MonoBehaviour
{
    private HighlightGrids _higlightGrids;
    private Camera _mainCamera;
    private Vector3 _mouseOffset;

    private void Start()
    {
        _mainCamera = Camera.main;
        _higlightGrids = FindObjectOfType<HighlightGrids>();
    }

    private void OnMouseDown()
    {
        _mouseOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        _higlightGrids.isDragging = true;
        _higlightGrids.block = gameObject;
        transform.position = GetMouseWorldPosition() + _mouseOffset;
    }

    private void OnMouseUp()
    {
        _higlightGrids.isDragging = false;
        _higlightGrids.block = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return _mainCamera.ScreenToWorldPoint( mousePosition );
    }
}
