using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        if (newTeamColor == Color.blue)
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("BlueInfantry_1.0");
        }
        else
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("RedInfantry_1.0");
        }

        
        //infantry movement
        //mMovement = mColor == Color.blue ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1);
    }
    /*
    private bool MatchesState(int targetX, int targetY, CellState targetState)
    {
        //add state of current cell 
        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);

        //check if cell is empty or is enemy
        if (cellState == targetState)
        {
            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
            return true;
        }
        return false;
    }

    protected override void CheckPathing()
    {
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //check top left
        MatchesState(currentX - mMovement.z, currentY + mMovement.z, CellState.Enemy);

        //check forward
        MatchesState(currentX, currentY + mMovement.y, CellState.Free);

        //check top right
        MatchesState(currentX + mMovement.z, currentY + mMovement.z, CellState.Enemy);
    }
    */
}
