using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed;
    public bool isFalling;

    private void Update()
    {
        if (isFalling)
            BlockFall();
    }

    private void BlockFall()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
