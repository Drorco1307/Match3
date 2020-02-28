using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView CellPF;

    public void Init(int[,] grid)
    {
        for (int i = 0; i < GameLogic.GRID_ROWS; i++)
        {
            for (int j = 0; j < GameLogic.GRID_COLS; j++)
            {
                CellView cv = Instantiate<CellView>(CellPF, new Vector3(j, -i, 0), Quaternion.identity);
                cv.Init();
                cv.SetType(grid[i, j]);
            }
        }
    }
}
