using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    public bool isOccupied;
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomLeft;
    public GameObject bottomRight;

    public GridInfo topNeighbor;
    public GridInfo bottomNeighbor;
    public GridInfo leftNeighbor;
    public GridInfo rightNeighbor;

    private void Start()
    {
        isOccupied = false;
    }

    public void AttachBlocksToPositions( GameObject block )
    {
        BlockInfo[] blockPieces = block.GetComponentsInChildren<BlockInfo>();

        if (blockPieces.Length == 1)
        {
            GameObject piece = blockPieces[0].gameObject;
            topLeft = piece;
            topRight = piece;
            bottomLeft = piece;
            bottomRight = piece;
        }
        else if (blockPieces.Length == 2)
        {
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;
                Vector3 localScale = piece.transform.localScale;

                if (Mathf.Approximately( localScale.x, 0.5f ) && Mathf.Approximately( localScale.y, 1f ))
                {
                    if (localPos.x <= 0f)
                    {
                        topLeft = piece.gameObject;
                        bottomLeft = piece.gameObject;
                    }
                    else
                    {
                        topRight = piece.gameObject;
                        bottomRight = piece.gameObject;
                    }
                }
                else if (Mathf.Approximately( localScale.x, 1f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.y >= 0f)
                    {
                        topLeft = piece.gameObject;
                        topRight = piece.gameObject;
                    }
                    else
                    {
                        bottomLeft = piece.gameObject;
                        bottomRight = piece.gameObject;
                    }
                }
                else
                {
                    if (localPos.x <= 0 && localPos.y >= 0)
                        topLeft = piece.gameObject;
                    else if (localPos.x > 0 && localPos.y >= 0)
                        topRight = piece.gameObject;
                    else if (localPos.x <= 0 && localPos.y < 0)
                        bottomLeft = piece.gameObject;
                    else if (localPos.x > 0 && localPos.y < 0)
                        bottomRight = piece.gameObject;
                }
            }
        }
        else if (blockPieces.Length == 3)
        {
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;
                Vector3 localScale = piece.transform.localScale;

                if (Mathf.Approximately( localScale.x, 0.5f ) && Mathf.Approximately( localScale.y, 1f ))
                {
                    if (localPos.x <= 0)
                    {
                        topLeft = piece.gameObject;
                        bottomLeft = piece.gameObject;
                    }
                    else
                    {
                        topRight = piece.gameObject;
                        bottomRight = piece.gameObject;
                    }
                }
                else if (Mathf.Approximately( localScale.x, 1f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.y >= 0)
                    {
                        topLeft = piece.gameObject;
                        topRight = piece.gameObject;
                    }
                    else
                    {
                        bottomLeft = piece.gameObject;
                        bottomRight = piece.gameObject;
                    }
                }
                else if (Mathf.Approximately( localScale.x, 0.5f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.x <= 0 && localPos.y >= 0)
                        topLeft = piece.gameObject;
                    else if (localPos.x > 0 && localPos.y >= 0)
                        topRight = piece.gameObject;
                    else if (localPos.x <= 0 && localPos.y < 0)
                        bottomLeft = piece.gameObject;
                    else if (localPos.x > 0 && localPos.y < 0)
                        bottomRight = piece.gameObject;
                }
            }
        }
        else if (blockPieces.Length == 4)
        {
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;

                if (localPos.x <= 0 && localPos.y >= 0)
                    topLeft = piece.gameObject;
                else if (localPos.x > 0 && localPos.y >= 0)
                    topRight = piece.gameObject;
                else if (localPos.x <= 0 && localPos.y < 0)
                    bottomLeft = piece.gameObject;
                else if (localPos.x > 0 && localPos.y < 0)
                    bottomRight = piece.gameObject;
            }
        }
    }
}
