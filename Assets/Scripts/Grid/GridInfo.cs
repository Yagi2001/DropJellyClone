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
        else if (blockPieces.Length == 3)
        {
            foreach (BlockInfo piece in blockPieces)
            {
                Vector3 localPos = piece.transform.localPosition;
                Vector3 localScale = piece.transform.localScale;

                if (localScale.x == 0.5f && localScale.y == 1f) // Vertical half
                {
                    if (localPos.x <= 0) // Left half
                    {
                        topLeft = piece.blockColor;
                        bottomLeft = piece.blockColor;
                    }
                    else if (localPos.x > 0) // Right half
                    {
                        topRight = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                else if (localScale.x == 1f && localScale.y == 0.5f) // Horizontal half
                {
                    if (localPos.y >= 0) // Top half
                    {
                        topLeft = piece.blockColor;
                        topRight = piece.blockColor;
                    }
                    else if (localPos.y < 0) // Bottom half
                    {
                        bottomLeft = piece.blockColor;
                        bottomRight = piece.blockColor;
                    }
                }
                else if (localScale.x == 0.5f && localScale.y == 0.5f) // Quarter piece
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
