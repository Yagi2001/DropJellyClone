using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupation : MonoBehaviour
{
    private GridInfo _gridInfo;
    private void Start()
    {
        _gridInfo = GetComponent<GridInfo>();
    }

    public void OccupationCheck()
    {
        if (OccupyCheck() == false)
        {
            Debug.Log( "NonOccupied" );
            _gridInfo.isOccupied = false;
        }
        else
            Debug.Log( "occupied" );
    }
    private bool OccupyCheck()
    {
        return !(_gridInfo.bottomRight == null &&
                 _gridInfo.bottomLeft == null &&
                 _gridInfo.topLeft == null &&
                 _gridInfo.topRight == null);
    }
}
