using System.Collections;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView CellPF;
    private CellView[,] _viewCells;

    public void Init()
    {
        _viewCells = new CellView[GameLogic.GRID_ROWS, GameLogic.GRID_COLS];
        for (int i = 0; i < GameLogic.GRID_ROWS; i++)
        {
            for (int j = 0; j < GameLogic.GRID_COLS; j++)
            {
                _viewCells[i, j] = Instantiate<CellView>(CellPF, new Vector3(i, -j, 0), Quaternion.identity);
                _viewCells[i, j].Init();

            }
        }
    }

    public IEnumerator UpdateViewFromData(CellModel[,] GridData)
    {
        for (int i = 0; i < GameLogic.GRID_ROWS; i++)
        {
            for (int j = 0; j < GameLogic.GRID_COLS; j++)
            {
                if (GridData[i, j].IsExplosion) // convert to state machine for animations
                {
                    _viewCells[i, j].PlayExplosion();
                }
                else
                {
                    _viewCells[i, j].SetType(GridData[i, j].Value);
                }
            }
        }

        bool continueLooping = true;
        while (continueLooping)
        {
            yield return null;
            continueLooping = false;
            for (int i = 0; i < GameLogic.GRID_ROWS; i++)
            {
                for (int j = 0; j < GameLogic.GRID_COLS; j++)
                {
                    continueLooping |= _viewCells[i, j].IsAnimating;
                }
            }
        }
    }
}
