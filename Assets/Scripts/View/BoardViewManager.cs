using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView CellPF;
    private CellView[,] _cells;

    public void Init()
    {
        _cells = new CellView[GameLogic.GRID_ROWS, GameLogic.GRID_COLS];
        for (int i = 0; i < GameLogic.GRID_ROWS; i++)
        {
            for (int j = 0; j < GameLogic.GRID_COLS; j++)
            {
                _cells[i, j] = Instantiate<CellView>(CellPF, new Vector3(i, -j, 0), Quaternion.identity);
                _cells[i, j].Init();

            }
        }
    }

    public void UpdateData(CellModel[,] grid)
    {
        for (int i = 0; i < GameLogic.GRID_ROWS; i++)
        {
            for (int j = 0; j < GameLogic.GRID_COLS; j++)
            {
                if (grid[i, j].IsExplosion)
                {
                    _cells[i, j].PlayExplosion();
                }
                else
                {
                    _cells[i, j].SetType(grid[i, j].Value);
                }

            }
        }
    }
}
