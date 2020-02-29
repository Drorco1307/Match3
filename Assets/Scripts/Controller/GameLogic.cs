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

        List<DirectedPoint> lstDetectedMatches = DetectMatches(out bool isDetected);
        while (isDetected)
        {
            EraseMatches(lstDetectedMatches);
            DropOverEmpty();
            ReInitGrid();
            lstDetectedMatches = DetectMatches(out isDetected);
        }
        BoardViewManagerRef.Init(Grid);
    }

    #region logic functions

    public List<DirectedPoint> DetectMatches(out bool isDetected)
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

        isDetected = lstTrios.Count > 0;
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

    public void EraseMatches(List<DirectedPoint> lstDetectedMatches)
    {
        foreach (DirectedPoint point in lstDetectedMatches)
        {
            switch (point.Direction)
            {
                case Direction.Right:
                    int a = point.Point.Y;
                    for (int j = 0; j < point.Length; j++)
                    {
                        Grid[point.Point.X, a] = -1;
                        a++;
                    }
                    break;
                case Direction.Down:
                    int b = point.Point.X;
                    for (int i = 0; i < point.Length; i++)
                    {
                        Grid[b, point.Point.Y] = -1;
                        b++;
                    }
                    break;
                case Direction.Left:
                    int c = point.Point.Y;
                    for (int j = 0; j < point.Length; j++)
                    {
                        Grid[point.Point.X, c] = -1;
                        c--;
                    }
                    break;
                case Direction.Up:
                    int d = point.Point.X;
                    for (int i = 0; i < point.Length; i++)
                    {
                        Grid[d, point.Point.Y] = -1;
                        d--;
                    }
                    break;
            }
        }
    }

    public void DropOverEmpty()
    {
            int temp;
        for (int j = 0; j < GRID_COLS; j++)
        {
            for (int i = 0; i <= GRID_ROWS-2; i++)
            {
                for (int k = 1; k <= GRID_ROWS-1; k++)
                {
                    if (Grid[k,j] == -1)
                    {
                        temp = Grid[k - 1, j];
                        Grid[k - 1, j] = -1;
                        Grid[k,j] = temp;
                    }
                }
            } 
        }
    }

    public void ReInitGrid()
    {
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                if (Grid[i,j] == -1)
                {
                    Grid[i, j] = Random.Range(0, 4);
                }
            }
        }
    }
    #endregion
}
