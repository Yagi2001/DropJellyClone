using System.Collections;
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
            if (Mathf.Abs( positionDiff.x ) < 0.1f && child.localScale.y < 0.75f)
            {
                child.localPosition = new Vector3( child.localPosition.x, 0f, child.localPosition.z );
                child.localScale = new Vector3( child.localScale.x, 1f, child.localScale.z );
                break;
            }
            if (Mathf.Abs( positionDiff.y ) < 0.1f && child.localScale.x < 0.75f)
            {
                child.localPosition = new Vector3( 0f, child.localPosition.y, child.localPosition.z );
                child.localScale = new Vector3( 1f, child.localScale.y, child.localScale.z );
                break;
            }
        }

        StartCoroutine( ReAttachColors() );
    }
    private IEnumerator ReAttachColors()
    {
        GridInfo gridInfo = occupiedGrid.GetComponent<GridInfo>();
        gridInfo.OccupationChange();
        yield return new WaitForSeconds( 0.25f );
        gridInfo.AttachBlocksToPositions( gameObject );
        gridInfo.LoopThroughParts();
    }
}
