using System;
using System.Collections;
using UnityEngine;

public class BoardViewManager : MonoBehaviour
{
    public CellView CellPF;
    public Action<Direction> CellSwipe;

    private CellView[,] _viewCells;
    private bool _isSwiping = false;
    private Vector3 _startDragPosition;

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

    private void Update()
    {
        if (_isSwiping)
        {
            if ((_startDragPosition - Input.mousePosition).magnitude > .1f)
            {
                _isSwiping = false;
                // calculate direction of swipe
                Direction swipeDirection = Direction.Up;

                //call action to gamelogic
                CellSwipe(swipeDirection);
            }
        }
        else
        {
            for (int i = 0; i < GameLogic.GRID_ROWS; i++)
            {
                for (int j = 0; j < GameLogic.GRID_COLS; j++)
                {
                    if (_viewCells[i, j].IsSelected)
                    {
                        _isSwiping = true;
                        _startDragPosition = Input.mousePosition;
                        _viewCells[i, j].IsSelected = false;
                    }
                }
            }
        }
    }
}
