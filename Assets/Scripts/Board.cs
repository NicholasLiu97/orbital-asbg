using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutofBounds
}

public class Board : MonoBehaviour
{
    public GameObject mCellPrefab;
    public GameObject mCellBarrackPrefab;
    public GameObject mCellHillPrefab;

    public PieceManager mPieceManager;


    [HideInInspector] public Cell[,] mAllCells = new Cell[9, 9];

    public void Create()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                GameObject newCell;
                //create cell
                if ((x == 2 || x == 4 || x == 6) && y == 4)
                {
                    newCell = Instantiate(mCellHillPrefab, transform);
                }
                else if ((x == 3 || x == 4 || x == 5) && (y == 1 || y == 7))
                {
                    newCell = Instantiate(mCellBarrackPrefab, transform);
                }
                else
                {
                    newCell = Instantiate(mCellPrefab, transform);
                }
                    

                //position
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                //setup
                mAllCells[x, y] = newCell.GetComponent<Cell>();
                mAllCells[x, y].Setup(new Vector2Int(x, y), this, mPieceManager);
            }
        }
    }

    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {
        //bounds check 9x9 board
        if (targetX < 0 || targetX > 8)
            return CellState.OutofBounds;
        if (targetY < 0 || targetY > 8)
            return CellState.OutofBounds;

        //get cell
        Cell targetCell = mAllCells[targetX, targetY];

        //if the cell contains a piece
        if(targetCell.mCurrentPiece != null)
        {
            //if friendly
            if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                return CellState.Friendly;

            //if enemy
            if (checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                return CellState.Enemy;
        }
        return CellState.Free;
    }
}
