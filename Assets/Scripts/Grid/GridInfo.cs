using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    public bool isOccupied;
    public BlockColor topLeft;
    public BlockColor topRight;
    public BlockColor bottomLeft;
    public BlockColor bottomRight;

    public GridInfo topNeighbor;
    public GridInfo bottomNeighbor;
    public GridInfo leftNeighbor;
    public GridInfo rightNeighbor;

    private void Start()
    {
        isOccupied = false;
    }

    public void AttachColorToPositions( GameObject block )
    {
        // Get all colors of the block
        BlockInfo[] blockPieces = block.GetComponentsInChildren<BlockInfo>();

        if (blockPieces.Length == 1)
        {
            // Single block covers all 4 corners
            BlockColor color = blockPieces[0].blockColor;
            topLeft = color;
            topRight = color;
            bottomLeft = color;
            bottomRight = color;
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
                        // Left half
                        topLeft = piece.blockColor;
                        bottomLeft = piece.blockColor;
                    }
                    else
                    {
                        // Right half
                        topRight = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                else if (Mathf.Approximately( localScale.x, 1f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.y >= 0f)
                    {
                        // Top half
                        topLeft = piece.blockColor;
                        topRight = piece.blockColor;
                    }
                    else
                    {
                        // Bottom half
                        bottomLeft = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                else
                {
                    // Fallback: Quadrant-based approach
                    if (localPos.x <= 0 && localPos.y >= 0)
                        topLeft = piece.blockColor;
                    else if (localPos.x > 0 && localPos.y >= 0)
                        topRight = piece.blockColor;
                    else if (localPos.x <= 0 && localPos.y < 0)
                        bottomLeft = piece.blockColor;
                    else if (localPos.x > 0 && localPos.y < 0)
                        bottomRight = piece.blockColor;
                }
            }
        }
        else if (blockPieces.Length == 3)
        {
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;
                Vector3 localScale = piece.transform.localScale;

                // Vertical half: covers left or right
                if (Mathf.Approximately( localScale.x, 0.5f ) && Mathf.Approximately( localScale.y, 1f ))
                {
                    if (localPos.x <= 0)
                    {
                        topLeft = piece.blockColor;
                        bottomLeft = piece.blockColor;
                    }
                    else
                    {
                        topRight = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                // Horizontal half: covers top or bottom
                else if (Mathf.Approximately( localScale.x, 1f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.y >= 0)
                    {
                        topLeft = piece.blockColor;
                        topRight = piece.blockColor;
                    }
                    else
                    {
                        bottomLeft = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                else if (Mathf.Approximately( localScale.x, 0.5f ) && Mathf.Approximately( localScale.y, 0.5f ))
                {
                    if (localPos.x <= 0 && localPos.y >= 0)
                        topLeft = piece.blockColor;
                    else if (localPos.x > 0 && localPos.y >= 0)
                        topRight = piece.blockColor;
                    else if (localPos.x <= 0 && localPos.y < 0)
                        bottomLeft = piece.blockColor;
                    else if (localPos.x > 0 && localPos.y < 0)
                        bottomRight = piece.blockColor;
                }
            }
        }
        else if (blockPieces.Length == 4)
        {
            // Four piece block: one piece per corner
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;

                if (localPos.x <= 0 && localPos.y >= 0)
                    topLeft = piece.blockColor;
                else if (localPos.x > 0 && localPos.y >= 0)
                    topRight = piece.blockColor;
                else if (localPos.x <= 0 && localPos.y < 0)
                    bottomLeft = piece.blockColor;
                else if (localPos.x > 0 && localPos.y < 0)
                    bottomRight = piece.blockColor;
            }
        }
    }
}
