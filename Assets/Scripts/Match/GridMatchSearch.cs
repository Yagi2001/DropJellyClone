using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMatchSearch : MonoBehaviour
{
    [SerializeField]
    private GridInfo testGridInfo;

    private void Start()
    {
        testGridInfo = gameObject.GetComponent<GridInfo>();
    }

    private void Update()
    {
        if (testGridInfo != null)
        {
            bool hasMatch = CheckForMatchingNeighbor( testGridInfo );
            if (hasMatch)
            {
                Debug.Log( "Match" );
            }
        }
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

        if (gridInfo.topNeighbor.bottomRight != BlockColor.None &&
            gridInfo.topNeighbor.bottomRight == gridInfo.topRight)
        {
            return true;
        }

        if (gridInfo.topNeighbor.bottomLeft != BlockColor.None &&
            gridInfo.topNeighbor.bottomLeft == gridInfo.topLeft)
        {
            return true;
        }

        return false;
    }

    private bool HasMatchingBottomNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.bottomNeighbor == null) return false;

        if (gridInfo.bottomNeighbor.topRight != BlockColor.None &&
            gridInfo.bottomNeighbor.topRight == gridInfo.bottomRight)
        {
            return true;
        }

        if (gridInfo.bottomNeighbor.topLeft != BlockColor.None &&
            gridInfo.bottomNeighbor.topLeft == gridInfo.bottomLeft)
        {
            return true;
        }

        return false;
    }

    private bool HasMatchingLeftNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.leftNeighbor == null) return false;

        if (gridInfo.leftNeighbor.topRight != BlockColor.None &&
            gridInfo.leftNeighbor.topRight == gridInfo.topLeft)
        {
            return true;
        }

        if (gridInfo.leftNeighbor.bottomRight != BlockColor.None &&
            gridInfo.leftNeighbor.bottomRight == gridInfo.bottomLeft)
        {
            return true;
        }

        return false;
    }

    private bool HasMatchingRightNeighbor( GridInfo gridInfo )
    {
        if (gridInfo.rightNeighbor == null) return false;

        if (gridInfo.rightNeighbor.topLeft != BlockColor.None &&
            gridInfo.rightNeighbor.topLeft == gridInfo.topRight)
        {
            return true;
        }

        if (gridInfo.rightNeighbor.bottomLeft != BlockColor.None &&
            gridInfo.rightNeighbor.bottomLeft == gridInfo.bottomRight)
        {
            return true;
        }

        return false;
    }
}
