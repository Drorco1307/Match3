using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public BoardViewManager BoardViewManagerRef;


    /// <summary>
    /// grid width
    /// </summary>
    public static int GRID_COLS = 9;

    /// <summary>
    /// grid height
    /// </summary>
    public static int GRID_ROWS = 10;

    /// <summary>
    /// raw data
    /// </summary>
    public CellModel[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        BoardViewManagerRef.Init();
        Grid = new CellModel[GRID_ROWS, GRID_COLS];
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                Grid[i, j] = new CellModel() { Value = Random.Range(0, 4) };
            }
        }

        //List<MatchPoint> lstDetectedMatches = DetectMatches(out bool isDetected);
        //while (isDetected)
        //{
        //    EraseMatches(lstDetectedMatches);
        //    DropOverEmpty();
        //    ReInitGrid();
        //    lstDetectedMatches = DetectMatches(out isDetected);
        //}
        StartCoroutine(BoardViewManagerRef.UpdateViewFromData(Grid));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EvaluateBoardCR());
        }
    }

    private IEnumerator EvaluateBoardCR()
    {
        List<MatchPoint> lstDetectedMatches = DetectMatches(out bool isDetected);
        if (isDetected)
        {
            EraseMatches(lstDetectedMatches);
            yield return BoardViewManagerRef.UpdateViewFromData(Grid);

            for (int i = 0; i < GRID_ROWS; i++)
            {
                for (int j = 0; j < GRID_COLS; j++)
                {
                    Grid[i, j].IsExplosion = false;
                }
            }
            DropOverEmpty();
            Debug.Log("end");
        }
    }
    #region logic functions

    /// <summary>
    /// Iterates through grid to find all possible matches
    /// </summary>
    /// <param name="isDetected">An out param indicating if a single match has been found</param>
    /// <returns>A list of all positions that are a part of a match</returns>
    public List<MatchPoint> DetectMatches(out bool isDetected)
    {
        List<MatchPoint> lstMatches = new List<MatchPoint>();
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                if (j < GRID_COLS - 1)
                {
                    DetectMatch(i, j, Direction.Right, lstMatches);
                }
                if (j > 1)
                {
                    DetectMatch(i, j, Direction.Left, lstMatches);
                }
                if (i > 1)
                {
                    DetectMatch(i, j, Direction.Up, lstMatches);
                }
                if (i < GRID_ROWS - 1)
                {
                    DetectMatch(i, j, Direction.Down, lstMatches);
                }
            }
        }

        isDetected = lstMatches.Count > 0;
        return lstMatches;
    }

    /// <summary>
    /// Detects a single match in the specified direction 
    /// </summary>
    /// <param name="i">i position on the grid</param>
    /// <param name="j">j position on the grid</param>
    /// <param name="direction">the direction in which to search for a match</param>
    /// <param name="lstMatches">A list of points the are a part of a match</param>
    private void DetectMatch(int i, int j, Direction direction, List<MatchPoint> lstMatches)
    {
        int length = 1;
        int k = 0;
        switch (direction)
        {
            case Direction.Right:
                k = j + 1;
                while (k < GRID_COLS)
                {
                    if (Grid[i, k].Value == Grid[i, j].Value)
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
                    if (Grid[k, j].Value == Grid[i, j].Value)
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
                    if (Grid[i, k].Value == Grid[i, j].Value)
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
                    if (Grid[k, j].Value == Grid[i, j].Value)
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
            lstMatches.Add(new MatchPoint()
            {
                PointPosition = new Point(i, j),
                Direction = direction,
                MatchLength = length
            });
            print($"i:{i}, j:{j}, Direction:{direction.ToString()}, length:{length}");
        }
    }

    /// <summary>
    /// Iterates through all found match points and "deletes" them
    /// </summary>
    /// <param name="lstDetectedMatches"> The list of found matches</param>
    public void EraseMatches(List<MatchPoint> lstDetectedMatches)
    {
        foreach (MatchPoint point in lstDetectedMatches)
        {
            switch (point.Direction)
            {
                case Direction.Right:
                    int a = point.PointPosition.Y;
                    for (int j = 0; j < point.MatchLength; j++)
                    {
                        Grid[point.PointPosition.X, a].Value = -1;
                        Grid[point.PointPosition.X, a].IsExplosion = true;
                        a++;
                    }
                    break;
                case Direction.Down:
                    int b = point.PointPosition.X;
                    for (int i = 0; i < point.MatchLength; i++)
                    {
                        Grid[b, point.PointPosition.Y].Value = -1;
                        Grid[b, point.PointPosition.Y].IsExplosion = true;
                        b++;
                    }
                    break;
                case Direction.Left:
                    int c = point.PointPosition.Y;
                    for (int j = 0; j < point.MatchLength; j++)
                    {
                        Grid[point.PointPosition.X, c].Value = -1;
                        Grid[point.PointPosition.X, c].IsExplosion = true;
                        c--;
                    }
                    break;
                case Direction.Up:
                    int d = point.PointPosition.X;
                    for (int i = 0; i < point.MatchLength; i++)
                    {
                        Grid[d, point.PointPosition.Y].Value = -1;
                        Grid[d, point.PointPosition.Y].IsExplosion = true;
                        d--;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Implements bubble sort on every row
    /// </summary>
    public void DropOverEmpty()
    {
        int temp;
        for (int j = 0; j < GRID_COLS; j++)
        {
            for (int i = 0; i <= GRID_ROWS - 2; i++)
            {
                for (int k = 1; k <= GRID_ROWS - 1; k++)
                {
                    if (Grid[k, j].Value == -1)
                    {
                        temp = Grid[k - 1, j].Value;
                        Grid[k - 1, j].Value = -1;
                        Grid[k, j].Value = temp;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Inserts new values over deleted points
    /// </summary>
    public void ReInitGrid()
    {
        for (int i = 0; i < GRID_ROWS; i++)
        {
            for (int j = 0; j < GRID_COLS; j++)
            {
                if (Grid[i, j].Value == -1)
                {
                    Grid[i, j].Value = Random.Range(0, 4);
                }
            }
        }
    }
    #endregion
}
