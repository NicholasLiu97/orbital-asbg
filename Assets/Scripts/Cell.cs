using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : EventTrigger
{
    public Image mOutlineImage;

    [HideInInspector] public Vector2Int mBoardPosition = Vector2Int.zero;
    [HideInInspector] public Board mBoard = null;
    [HideInInspector] public RectTransform mRectTransform = null;
    [HideInInspector] public BasePiece mCurrentPiece = null;

    //ADDED
    public PieceManager mPieceManager;

    public void Setup(Vector2Int newBoardPosition, Board newBoard, PieceManager newPieceManager)
    {
        mBoardPosition = newBoardPosition;
        mBoard = newBoard;

        mPieceManager = newPieceManager;

        mRectTransform = GetComponent<RectTransform>();

        //way to change cell color
        //mOutlineImage.color = Color.red;
    }

    public void RemovePiece()
    {
        //checks to see if the current cell contains a piece, if so kill the piece
        if (mCurrentPiece != null)
        {
            mCurrentPiece.Kill();
        }
    }

    public void ReducePieceHealth(int mAttack)
    {
        if (mCurrentPiece != null)
        {
            int CalculatedDamage = 0;
            int EnemyRoll = Random.Range(1, 5);//max is exclusive, returns 1-4 range
            int FriendlyRoll = Random.Range(1, 5);

            CalculatedDamage = mAttack; //TOBECHANGED
                //(EnemyRoll + mAttack) - (FriendlyRoll + mCurrentPiece.CurrentHealth);
            Debug.Log(mAttack);
            Debug.Log(EnemyRoll);
            Debug.Log(mAttack);
            Debug.Log(CalculatedDamage);

            if (CalculatedDamage < 0)
                CalculatedDamage = 0;
            mCurrentPiece.DamagePiece(CalculatedDamage);
        }
    }

    //for moving and attacking
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Debug.Log("click cell");
        //if piece selected
        if (mPieceManager.mSelectedPiece)
        {
            
            //check for valid move
            foreach (Cell cell in mPieceManager.mSelectedPiece.mHighlightedCells)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
                {
                    Debug.Log("move valid");
                    //if mouse is within valid cell, get it and break
                    mPieceManager.mSelectedPiece.mTargetCell = cell;
                    break;
                }
                //if mouse is not within a highlighted cell, not valid move
                mPieceManager.mSelectedPiece.mTargetCell = null;
            }



            //if valid move
            if (mPieceManager.mSelectedPiece.mTargetCell)
            {
                mPieceManager.mSelectedPiece.ClearCells();
                mPieceManager.mSelectedPiece.ClearAttackCells();

                //Remove stats gained by current cell when leaving
                mPieceManager.mSelectedPiece.RemoveStats();

                Debug.Log("move");
                //move to new cell
                mPieceManager.mSelectedPiece.Move();
                
                //Update stats gained after moving to cell
                mPieceManager.mSelectedPiece.UpdateStats();

                //end turn
                mPieceManager.SwitchSides(mPieceManager.mSelectedPiece.mColor);

                //reset
                mPieceManager.mSelectedPiece.mTargetCell = null;
                mPieceManager.mSelectedPiece = null;
            }
            else //check for attack
            {
                foreach (Cell cell in mPieceManager.mSelectedPiece.mAttackHighlightedCells)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
                    {
                        Debug.Log("attack valid");
                        //if mouse is within valid cell, get it and break
                        mPieceManager.mSelectedPiece.mTargetCell = cell;
                        break;
                    }
                    //if mouse is not within a highlighted cell, not valid attack
                    mPieceManager.mSelectedPiece.mTargetCell = null;
                }

                //if valid attack
                if (mPieceManager.mSelectedPiece.mTargetCell)
                {
                    mPieceManager.mSelectedPiece.ClearCells();
                    mPieceManager.mSelectedPiece.ClearAttackCells();

                    Debug.Log("attack");
                    //selected piece attacks target cell
                    mPieceManager.mSelectedPiece.Attack();

                    //end turn
                    mPieceManager.SwitchSides(mPieceManager.mSelectedPiece.mColor);

                    //reset
                    mPieceManager.mSelectedPiece.mTargetCell = null;
                    mPieceManager.mSelectedPiece = null;
                }
            }
        }
    }
}

