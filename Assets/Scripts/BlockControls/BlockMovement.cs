using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed;

    private float _targetY;
    private bool _isFalling = false;
    private GameObject _lowestUnoccupied;
    private FillBlock _blockInfo;

    private void Start()
    {
        _blockInfo = GetComponent<FillBlock>();
    }

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

            GridInfo gridInfo = _lowestUnoccupied.GetComponent<GridInfo>();
            gridInfo.AttachBlocksToPositions( gameObject );
            gridInfo.isOccupied = true;

            // Continuously check neighbors until there are no more matches
            ContinuouslyCheckNeighborMatches( gridInfo );

            return true;
        }
        return false;
    }

    private void BlockFall()
    {
        if (HasReachedTarget())
        {
            _isFalling = false;
            return;
        }
        Vector3 newPosition = transform.position;
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    public void FindPositionOfAvailableGrid( GameObject gridGroup )
    {
        Transform lowestUnoccupied = null;
        float minY = float.MaxValue;

        foreach (Transform child in gridGroup.transform)
        {
            GridInfo gridInfo = child.GetComponent<GridInfo>();
            if (!gridInfo.isOccupied)
            {
                if (child.position.y < minY)
                {
                    minY = child.position.y;
                    lowestUnoccupied = child;
                }
            }
        }

        if (lowestUnoccupied != null)
        {
            _lowestUnoccupied = lowestUnoccupied.gameObject;
            _blockInfo.occupiedGrid = _lowestUnoccupied;
            _targetY = minY;
            _isFalling = true;
        }
    }
    private void ContinuouslyCheckNeighborMatches( GridInfo gridInfo )
    {
        bool foundMatchesInPass;
        do
        {
            foundMatchesInPass = false;
            if (gridInfo.topNeighbor != null)
            {
                GridMatchSearch topSearch = gridInfo.topNeighbor.GetComponent<GridMatchSearch>();
                if (topSearch != null && topSearch.CheckMatches())
                    foundMatchesInPass = true;
            }
            if (gridInfo.bottomNeighbor != null)
            {
                GridMatchSearch bottomSearch = gridInfo.bottomNeighbor.GetComponent<GridMatchSearch>();
                if (bottomSearch != null && bottomSearch.CheckMatches())
                    foundMatchesInPass = true;
            }
            if (gridInfo.leftNeighbor != null)
            {
                GridMatchSearch leftSearch = gridInfo.leftNeighbor.GetComponent<GridMatchSearch>();
                if (leftSearch != null && leftSearch.CheckMatches())
                    foundMatchesInPass = true;
            }
            if (gridInfo.rightNeighbor != null)
            {
                GridMatchSearch rightSearch = gridInfo.rightNeighbor.GetComponent<GridMatchSearch>();
                if (rightSearch != null && rightSearch.CheckMatches())
                    foundMatchesInPass = true;
            }
        }
        while (foundMatchesInPass);
    }
}
