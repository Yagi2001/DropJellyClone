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
            _gridInfo.isOccupied = false;
        }
    }
    private bool OccupyCheck()
    {
        return !(_gridInfo.bottomRight == null &&
                 _gridInfo.bottomLeft == null &&
                 _gridInfo.topLeft == null &&
                 _gridInfo.topRight == null);
    }
}
