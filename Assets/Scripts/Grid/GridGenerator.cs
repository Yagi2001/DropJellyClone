using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private int _rows = 6;
    [SerializeField]
    private int _columns = 6;
    [SerializeField]
    private float _spacing = -0.145f;
    [SerializeField]
    private Transform _parentGrid;
    [SerializeField]
    private GameObject _tilePrefab;

    private GridInfo[,] _gridTiles;

    private void Start()
    {
        GenerateGrid();
        AssignNeighbors();
    }

    private void GenerateGrid()
    {
        _gridTiles = new GridInfo[_columns, _rows];

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                float xPosition = col + col * _spacing;
                float yPosition = row + row * _spacing;
                Vector3 position = new Vector3( xPosition, yPosition, 0 );

                GameObject tile = Instantiate( _tilePrefab, position, Quaternion.identity, _parentGrid );
                GridInfo gridInfo = tile.GetComponent<GridInfo>();
                if (gridInfo != null)
                {
                    _gridTiles[col, row] = gridInfo;
                }
            }
        }
    }

    private void AssignNeighbors()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                GridInfo currentTile = _gridTiles[col, row];
                if (currentTile == null) continue;
                GridInfo top = row + 1 < _rows ? _gridTiles[col, row + 1] : null;
                GridInfo bottom = row - 1 >= 0 ? _gridTiles[col, row - 1] : null;
                GridInfo left = col - 1 >= 0 ? _gridTiles[col - 1, row] : null;
                GridInfo right = col + 1 < _columns ? _gridTiles[col + 1, row] : null;

                currentTile.topNeighbor = top;
                currentTile.bottomNeighbor = bottom;
                currentTile.leftNeighbor = left;
                currentTile.rightNeighbor = right;
            }
        }
    }
}
