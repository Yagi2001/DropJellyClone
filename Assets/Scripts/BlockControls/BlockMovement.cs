using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed;
    private float _bottomBorder;
    public bool isFalling;

    private void Start()
    {
        GameObject bottomBorder = GameObject.FindGameObjectWithTag( "Border" );
        _bottomBorder = bottomBorder.transform.position.y;
    }

    private void Update()
    {
        if (isFalling)
            BlockFall();
    }

    private void BlockFall()
    {
        Vector3 newPosition = transform.position;
        if (HasReachedBottomBorder())
        {
            newPosition.y = _bottomBorder;
            isFalling = false;
            return;
        }
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private bool HasReachedBottomBorder()
    {
        return transform.position.y <= _bottomBorder;
    }
}
