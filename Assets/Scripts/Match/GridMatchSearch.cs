using UnityEngine;
using System.Collections;

public class GridMatchSearch : MonoBehaviour
{
    private bool _isThereMatch;
    private GridInfo _gridInfo;
    private float _matchTime = 1f; // Adjust as animation need
    private GridInfo _matchedGrid;
    private void Start()
    {
        _gridInfo = GetComponent<GridInfo>();
    }

    public bool CheckMatches()
    {
        bool hasMatch = CheckForMatchingNeighbor( _gridInfo );
        return hasMatch;
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.topRight, gridInfo.topNeighbor.bottomRight ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.topLeft, gridInfo.topNeighbor.bottomLeft ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.bottomRight, gridInfo.bottomNeighbor.topRight ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.bottomLeft, gridInfo.bottomNeighbor.topLeft ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.topLeft, gridInfo.leftNeighbor.topRight ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.bottomLeft, gridInfo.leftNeighbor.bottomRight ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.topRight, gridInfo.rightNeighbor.topLeft ) );
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
                StartCoroutine( DestroyMatchingObjects( gridInfo.bottomRight, gridInfo.rightNeighbor.bottomLeft ) );
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

    private IEnumerator DestroyMatchingObjects( GameObject firstBlock, GameObject secondBlock )
    {
        yield return new WaitForSeconds( 0.5f );
        firstBlock.SetActive( false );
        secondBlock.SetActive( false );
        yield return new WaitForSeconds( _matchTime );
        // Fill missing blocks
        FillBlock fillBlockFirst = firstBlock.GetComponentInParent<FillBlock>();
        FillBlock fillBlockSecond = secondBlock.GetComponentInParent<FillBlock>();
        fillBlockFirst.FillMissingBlock( firstBlock.transform );
        fillBlockSecond.FillMissingBlock( secondBlock.transform );
        Destroy( firstBlock );
        Destroy( secondBlock );
        //_gridInfo.OccupationCheck();
    }
}
