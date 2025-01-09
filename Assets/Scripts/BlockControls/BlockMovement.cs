using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed;
    private float _targetY;
    private bool _isFalling = false;

    private void Update()
    {
        if (_isFalling)
            BlockFall();
    }
    private bool HasReachedTarget()
    {
        if (transform.position.y <= _targetY)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = _targetY;
            transform.position = newPosition;
            return true;
        }

        return false;
    }

    private void BlockFall()
    {
        Vector3 newPosition = transform.position;
        if (HasReachedTarget())
        {
            _isFalling = false;
            return;
        }
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    public void FindPositionOfAvailableGrid( GameObject gridGroup )
    {
        Transform lowestUnoccupied = null;
        float minY = float.MaxValue;

        foreach (Transform child in gridGroup.transform)
        {
            GameObject gridTile = child.gameObject;
            GridInfo gridInfo = gridTile.GetComponent<GridInfo>();
            if (!gridInfo.isOccupied)
            {
                if (child.position.y < minY)
                {
                    minY = child.position.y;
                    lowestUnoccupied = child;
                }
            }
        }
        lowestUnoccupied.GetComponent<GridInfo>().isOccupied = true;
        lowestUnoccupied.GetComponent<GridInfo>().AttachBlocksToPositions( gameObject );
        _targetY = minY;
        _isFalling = true;
    }
}
