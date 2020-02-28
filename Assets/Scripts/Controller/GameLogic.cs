using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public BoardViewManager BoardViewManagerRef;

    public static int GRID_COLS = 9;
    public static int GRID_ROWS = 10;
    public int[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[GRID_ROWS, GRID_COLS];
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                Grid[i, j] = Random.Range(0, 4);
            }
        }

        BoardViewManagerRef.Init(Grid);
        List<DirectedPoint> lstDetectedMatches = DetectMatches();
    }

    #region logic functions

    public List<DirectedPoint> DetectMatches()
    {
        List<DirectedPoint> lstTrios = new List<DirectedPoint>();
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                if (j < GRID_COLS - 1)
                {
                    DetectMatch(i, j, Direction.Right, lstTrios);
                }
                if (j > 1)
                {
                    DetectMatch(i, j, Direction.Left, lstTrios);
                }
                if (i > 1)
                {
                    DetectMatch(i, j, Direction.Up, lstTrios);
                }
                if (i < GRID_ROWS - 1)
                {
                    DetectMatch(i, j, Direction.Down, lstTrios);
                }
            }
        }

        return lstTrios;
    }

    private void DetectMatch(int i, int j, Direction direction, List<DirectedPoint> lstTrios)
    {
        int length = 1;
        int k = 0;
        switch (direction)
        {
            case Direction.Right:
                k = j + 1;
                while (k < GRID_COLS)
                {
                    if (Grid[i, k] == Grid[i, j])
                    {
                        length++;
                        k++;
                    }
                    else
                        break;
                }
                break;
            case Direction.Down:
                k = i + 1;
                while (k < GRID_ROWS)
                {
                    if (Grid[k, j] == Grid[i, j])
                    {
                        length++;
                        k++; 
                    }
                    else
                        break;
                }
                break;
            case Direction.Left:
                k = j - 1;
                while (k > 0)
                {
                    if (Grid[i, k] == Grid[i, j])
                    {
                        length++;
                        k--; 
                    }
                    else
                        break;
                }
                break;
            case Direction.Up:
                k = i - 1;
                while (k > 0)
                {
                    if (Grid[k, j] == Grid[i, j])
                    {
                        length++;
                        k--; 
                    }
                    else
                        break;
                }
                break;
        }

        if (length > 2)
        {
            lstTrios.Add(new DirectedPoint()
            {
                Point = new Point(i, j),
                Direction = direction,
                Length = length
            });
            print($"i:{i}, j:{j}, Direction:{direction.ToString()}, length:{length}");
        }
    }

    public void EraseTrios()
    {

    }

    public void DropOverEmpty()
    { }

    public void FillUpperRows()
    { }
    #endregion
}
