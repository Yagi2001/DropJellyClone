using UnityEngine;

public class GridMatchSearch : MonoBehaviour
{
    private GridInfo _gridInfo;

    private void Start()
    {
        _gridInfo = GetComponent<GridInfo>();
    }

    private void Update()
    {
        CheckMatches();
    }

    public bool CheckMatches()
    {
        if (_gridInfo != null)
        {
            bool hasMatch = CheckForMatchingNeighbor( _gridInfo );
            if (hasMatch)
            {
                Debug.Log( "Match" );
            }
            return hasMatch;
        }
        return false;
    }

    public bool CheckForMatchingNeighbor( GridInfo gridInfo )
    {
        if (gridInfo == null) return false;
        if (HasMatchingTopNeighbor( gridInfo )) return true;
        if (HasMatchingBottomNeighbor( gridInfo )) return true;
        if (HasMatchingLeftNeighbor( gridInfo )) return true;
        if (HasMatchingRightNeighbor( gridInfo )) return true;
        return false;
    }

    private bool HasMatchingTopNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.topNeighbor == null) return false;

        if (gridInfo.topNeighbor.bottomRight != null && gridInfo.topRight != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.topNeighbor.bottomRight );
            BlockColor ourColor = GetBlockColor( gridInfo.topRight );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.topRight, gridInfo.topNeighbor.bottomRight );
                Destroy( gridInfo.topRight );
                Destroy( gridInfo.topNeighbor.bottomRight );
                gridInfo.topRight = null;
                gridInfo.topNeighbor.bottomRight = null;
                return true;
            }
        }

        if (gridInfo.topNeighbor.bottomLeft != null && gridInfo.topLeft != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.topNeighbor.bottomLeft );
            BlockColor ourColor = GetBlockColor( gridInfo.topLeft );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.topLeft, gridInfo.topNeighbor.bottomLeft );
                Destroy( gridInfo.topLeft );
                Destroy( gridInfo.topNeighbor.bottomLeft );
                gridInfo.topLeft = null;
                gridInfo.topNeighbor.bottomLeft = null;
                return true;
            }
        }

        return false;
    }

    private bool HasMatchingBottomNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.bottomNeighbor == null) return false;

        if (gridInfo.bottomNeighbor.topRight != null && gridInfo.bottomRight != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.bottomNeighbor.topRight );
            BlockColor ourColor = GetBlockColor( gridInfo.bottomRight );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.bottomRight, gridInfo.bottomNeighbor.topRight );
                Destroy( gridInfo.bottomRight );
                Destroy( gridInfo.bottomNeighbor.topRight );
                gridInfo.bottomRight = null;
                gridInfo.bottomNeighbor.topRight = null;
                return true;
            }
        }

        if (gridInfo.bottomNeighbor.topLeft != null && gridInfo.bottomLeft != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.bottomNeighbor.topLeft );
            BlockColor ourColor = GetBlockColor( gridInfo.bottomLeft );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.bottomLeft, gridInfo.bottomNeighbor.topLeft );
                Destroy( gridInfo.bottomLeft );
                Destroy( gridInfo.bottomNeighbor.topLeft );
                gridInfo.bottomLeft = null;
                gridInfo.bottomNeighbor.topLeft = null;
                return true;
            }
        }

        return false;
    }

    private bool HasMatchingLeftNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.leftNeighbor == null) return false;

        if (gridInfo.leftNeighbor.topRight != null && gridInfo.topLeft != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.leftNeighbor.topRight );
            BlockColor ourColor = GetBlockColor( gridInfo.topLeft );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.topLeft, gridInfo.leftNeighbor.topRight );
                Destroy( gridInfo.topLeft );
                Destroy( gridInfo.leftNeighbor.topRight );
                gridInfo.topLeft = null;
                gridInfo.leftNeighbor.topRight = null;
                return true;
            }
        }

        if (gridInfo.leftNeighbor.bottomRight != null && gridInfo.bottomLeft != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.leftNeighbor.bottomRight );
            BlockColor ourColor = GetBlockColor( gridInfo.bottomLeft );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.bottomLeft, gridInfo.leftNeighbor.bottomRight );
                Destroy( gridInfo.bottomLeft );
                Destroy( gridInfo.leftNeighbor.bottomRight );
                gridInfo.bottomLeft = null;
                gridInfo.leftNeighbor.bottomRight = null;
                return true;
            }
        }

        return false;
    }

    private bool HasMatchingRightNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.rightNeighbor == null) return false;

        if (gridInfo.rightNeighbor.topLeft != null && gridInfo.topRight != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.rightNeighbor.topLeft );
            BlockColor ourColor = GetBlockColor( gridInfo.topRight );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.topRight, gridInfo.rightNeighbor.topLeft );
                Destroy( gridInfo.topRight );
                Destroy( gridInfo.rightNeighbor.topLeft );
                gridInfo.topRight = null;
                gridInfo.rightNeighbor.topLeft = null;
                return true;
            }
        }

        if (gridInfo.rightNeighbor.bottomLeft != null && gridInfo.bottomRight != null)
        {
            BlockColor neighborColor = GetBlockColor( gridInfo.rightNeighbor.bottomLeft );
            BlockColor ourColor = GetBlockColor( gridInfo.bottomRight );
            if (neighborColor != BlockColor.None && neighborColor == ourColor)
            {
                DestroyMatchingObjects( gridInfo.bottomRight, gridInfo.rightNeighbor.bottomLeft );
                Destroy( gridInfo.bottomRight );
                Destroy( gridInfo.rightNeighbor.bottomLeft );
                gridInfo.bottomRight = null;
                gridInfo.rightNeighbor.bottomLeft = null;
                return true;
            }
        }

        return false;
    }

    private BlockColor GetBlockColor( GameObject piece )
    {
        BlockInfo info = piece.GetComponent<BlockInfo>();
        if (info != null) return info.blockColor;
        return BlockColor.None;
    }

    private void DestroyMatchingObjects (GameObject firstBlock, GameObject secondBlock )
    {
        FillBlock fillBlockFirst = firstBlock.GetComponentInParent<FillBlock>();
        FillBlock fillBlockSecond = secondBlock.GetComponentInParent<FillBlock>();
        fillBlockFirst.FillMissingBlock(firstBlock.transform);
        fillBlockSecond.FillMissingBlock( secondBlock.transform );
    }
}
