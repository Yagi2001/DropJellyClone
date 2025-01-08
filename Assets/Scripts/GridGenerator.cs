using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private int _rows = 6;
    [SerializeField]
    private int _columns = 6;
    [SerializeField]
    private float _spacing = 0f;
    [SerializeField]
    private Transform _parentGrid;
    [SerializeField]
    private GameObject _tilePrefab;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                float xPosition = col + col * _spacing;
                float yPosition = row + row * _spacing;
                Vector3 position = new Vector3( xPosition, yPosition, 0 );
                Instantiate( _tilePrefab, position, Quaternion.identity, _parentGrid );
            }
        }
    }
}
