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
            GridMatchSearch gridMatchSearch = _lowestUnoccupied.GetComponent<GridMatchSearch>();
            CheckNeighborMatches( gridInfo );
            //gridMatchSearch.CheckForMatchingNeighbor( gridInfo );
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
        Debug.Log( "Entered here" );
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

    private void CheckNeighborMatches( GridInfo gridInfo )
    {
        if (gridInfo.topNeighbor != null)
        {
            GridMatchSearch topMatch = gridInfo.topNeighbor.GetComponent<GridMatchSearch>();
            if (topMatch != null) topMatch.CheckMatches();
        }
        if (gridInfo.bottomNeighbor != null)
        {
            GridMatchSearch bottomMatch = gridInfo.bottomNeighbor.GetComponent<GridMatchSearch>();
            if (bottomMatch != null) bottomMatch.CheckMatches();
        }
        if (gridInfo.leftNeighbor != null)
        {
            GridMatchSearch leftMatch = gridInfo.leftNeighbor.GetComponent<GridMatchSearch>();
            if (leftMatch != null) leftMatch.CheckMatches();
        }
        if (gridInfo.rightNeighbor != null)
        {
            GridMatchSearch rightMatch = gridInfo.rightNeighbor.GetComponent<GridMatchSearch>();
            if (rightMatch != null) rightMatch.CheckMatches();
        }
    }
}
