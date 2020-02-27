using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public BoardViewManager BoardViewManagerRef;

    public static int GRID_WIDTH = 9;
    public static int GRID_HEIGHT = 10;
    public int[,] Grid;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[GRID_WIDTH, GRID_HEIGHT];
        for (int i = 0; i < GRID_WIDTH; i++)
        {
            for (int J = 0; J < GRID_HEIGHT; J++)
            {
                Grid[i, J] = Random.Range(0, 4);
            }
        }

        BoardViewManagerRef.Init(Grid);
    }

    #region logic functions
    public void DetectTrios()
    { }

    public void EraseTrios()
    { }

    public void DropOverEmpty()
    { }

    public void FillUpperRows()
    { }
    #endregion
}
