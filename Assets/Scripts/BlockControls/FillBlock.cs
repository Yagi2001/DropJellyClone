using UnityEngine;

public class FillBlock : MonoBehaviour
{
    public GameObject occupiedGrid;
    public void FillMissingBlock( Transform destroyedBlock )
    {
        foreach (Transform child in transform)
        {
            if (child == destroyedBlock)
                continue;
            Vector3 positionDiff = child.localPosition - destroyedBlock.localPosition;

            //Vertical Growth has a priority
            if (Mathf.Abs( positionDiff.x ) < 0.05f && child.localScale.y < 0.95f)
            {
                child.localPosition = new Vector3( child.localPosition.x, 0f, child.localPosition.z );
                child.localScale = new Vector3( child.localScale.x, 1f, child.localScale.z );
                break;
            }
            if (Mathf.Abs( positionDiff.y ) < 0.05f && child.localScale.x < 0.95f)
            {
                child.localPosition = new Vector3( 0f, child.localPosition.y, child.localPosition.z );
                child.localScale = new Vector3( 1f, child.localScale.y, child.localScale.z );
                break;
            }
        }
        ReAttachColors();
    }
    private void ReAttachColors()
    {
        GridInfo gridInfo = occupiedGrid.GetComponent<GridInfo>();
        gridInfo.OccupationChange();
        gridInfo.AttachBlocksToPositions( gameObject );
    }
}
