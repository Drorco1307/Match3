using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView CellPF;

    public void Init(int[,] grid)
    {
        for (int i = 0; i <GameLogic.GRID_WIDTH; i++)
        {
            for (int j = 0; j < GameLogic.GRID_HEIGHT; j++)
            {
                CellView cv = Instantiate<CellView>(CellPF, new Vector3(i, -j, 0), Quaternion.identity);
                cv.Init();
                cv.SetType(grid[i, j]);
            }
        }
    }
}
