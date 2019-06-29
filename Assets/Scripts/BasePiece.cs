﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public abstract class BasePiece : EventTrigger
{
    [HideInInspector] public Color mColor = Color.clear;


    //Piece Health
    public Slider healthSlider;
    public int CurrentHealth;
    public int maxHealth;
    public Text healthText;

    protected Cell mOriginalCell = null;
    public Cell mCurrentCell = null;

    protected RectTransform mRectTransform = null;
    protected PieceManager mPieceManager;

    public Cell mTargetCell = null;

    protected Vector3Int mMovement = new Vector3Int(1, 1, 0);
    public List<Cell> mHighlightedCells = new List<Cell>();

    //Added for attack
    protected Vector3Int mAttackRange = new Vector3Int(1, 1, 0);
    public List<Cell> mAttackHighlightedCells = new List<Cell>();
    protected int mAttack;
    protected int mDiceMax = 4;
    protected int EnemyRoll, FriendlyRoll;

    public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = 7;
        maxHealth = 7;
        mAttack = 2;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();
        
        healthText.text = CurrentHealth.ToString();

    }
     
    

    public void Place(Cell newCell)
    {
        //cell
        mCurrentCell = newCell;
        mOriginalCell = newCell;
        mCurrentCell.mCurrentPiece = this;
        UpdateStats();


        //Object
        transform.position = newCell.transform.position;
        gameObject.SetActive(true);
    }

    public virtual void Reset()
    {
        Kill();
        CurrentHealth = maxHealth;
        UpdateHealth();
        Place(mOriginalCell);
        
    }

    public virtual void Kill()
    {
        //clear current cell 
        mCurrentCell.mCurrentPiece = null;

        //remove piece, clear data within board
        gameObject.SetActive(false);
    }

    #region Movement
    private void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        //get position
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //check each cell
        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            //get state of target cell
            CellState cellState = CellState.None;
            cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);

            //if enemy, break
            if(cellState == CellState.Enemy)
            {
                //mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;
            }

            //if cell is not free,break
            if (cellState != CellState.Free)
                break;

            //add to list of possible places to move to
            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
        }

    }

    protected virtual void CheckPathing()
    {
        //Horizontal
        CreateCellPath(1, 0, mMovement.x);
        CreateCellPath(-1, 0, mMovement.x);

        //Vertical
        CreateCellPath(0, 1, mMovement.y);
        CreateCellPath(0, -1, mMovement.y);

        //Upper Diagonal
        CreateCellPath(1, 1, mMovement.z);
        CreateCellPath(-1, 1, mMovement.z);

        //Lower Diagonal
        CreateCellPath(-1, -1, mMovement.z);
        CreateCellPath(1, -1, mMovement.z);

    }

    protected void ShowCells()
    {
        //loop through cells that can be moved to
        foreach (Cell cell in mHighlightedCells)
        {
            cell.mOutlineImage.enabled = true;
            cell.mOutlineImage.color = Color.black;
        }    

    }

    public void ClearCells()
    {
        foreach (Cell cell in mHighlightedCells)
            cell.mOutlineImage.enabled = false;
        

        mHighlightedCells.Clear();

    }

    public virtual void Move()
    {
        //if enemy piece,remove it
        mTargetCell.RemovePiece();

        //clear current
        mCurrentCell.mCurrentPiece = null;

        //switch cells
        mCurrentCell = mTargetCell;
        mCurrentCell.mCurrentPiece = this;

        //Move on board
        transform.position = mCurrentCell.transform.position;
        mTargetCell = null;

    }
    #endregion

    #region Attack

    private void CreateAttackCellPath(int xDirection, int yDirection, int movement)
    {
        //get position
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //check each cell
        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            //get state of target cell
            CellState cellState = CellState.None;
            cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);

            //if enemy, break
            if (cellState == CellState.Enemy)
            {
                mAttackHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;//i think this prevents further checking down the cell
            }
        }

    }
    protected virtual void CheckAttackPathing()
    {
        //Horizontal
        CreateAttackCellPath(1, 0, mAttackRange.x);
        CreateAttackCellPath(-1, 0, mAttackRange.x);

        //Vertical
        CreateAttackCellPath(0, 1, mAttackRange.y);
        CreateAttackCellPath(0, -1, mAttackRange.y);

        //Upper Diagonal
        CreateAttackCellPath(1, 1, mAttackRange.z);
        CreateAttackCellPath(-1, 1, mAttackRange.z);

        //Lower Diagonal
        CreateAttackCellPath(-1, -1, mAttackRange.z);
        CreateAttackCellPath(1, -1, mAttackRange.z);

    }

    protected void ShowAttackCells()
    {
        //loop through cells that can be moved to
        foreach (Cell cell in mAttackHighlightedCells)
        {
            cell.mOutlineImage.enabled = true;
            cell.mOutlineImage.color = Color.red;
            cell.mCurrentPiece.enabled = true; 
        }

    }

    public void ClearAttackCells()
    {
        foreach (Cell cell in mAttackHighlightedCells)
        {
            cell.mOutlineImage.enabled = false;
            cell.mCurrentPiece.enabled = false;
        }
            
        mAttackHighlightedCells.Clear();

    }

    public virtual void Attack()
    {
        mTargetCell.ReducePieceHealth(mAttack);
        mTargetCell.mCurrentPiece.UpdateHealth();
        if (mTargetCell.mCurrentPiece.CurrentHealth <= 0) //if piece dies
        {
            mTargetCell.RemovePiece();
        }
        
    }

    public virtual void DamagePiece(int mAttack)
    {
        CurrentHealth -= mAttack;
    }

    public virtual void UpdateHealth()
    {
        healthSlider.value = CurrentHealth;
        healthText.text = CurrentHealth.ToString();
    }


    #endregion


    #region Buffs
    public void UpdateStats()
    {
        if (mCurrentCell.tag == "Hill")
        {
            mAttack += 2;
        }
        else if (mCurrentCell.tag == "Barrack")
        {
            CurrentHealth += 2;
            UpdateHealth();
        }
    }

    public void RemoveStats()
    {
        if (mCurrentCell.tag == "Hill")
        {
            mAttack -= 2;
        }
        else if (mCurrentCell.tag == "Barrack")
        {
            CurrentHealth -= 2;
            UpdateHealth();
        }
    }


    #endregion
    #region Events



    //when piece is clicked
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click piece");
        base.OnPointerClick(eventData);
        
        


        //if no piece selected
        if (mPieceManager.mSelectedPiece == null)
        {
            Debug.Log("not selected");
            //Current piece is selected
            mPieceManager.mSelectedPiece = this;

            //test for cells
            CheckPathing();
            //show valid cells
            ShowCells();

            //test for cells to attack  
            CheckAttackPathing();
            //show valid cells to attack
            ShowAttackCells();

        }
        //if different friendly piece selected
        else if((mPieceManager.mSelectedPiece != this) && (mPieceManager.mSelectedPiece.mColor == this.mColor))
        {
            Debug.Log("different piece");
            //clear previous data
            mPieceManager.mSelectedPiece.ClearCells();
            mPieceManager.mSelectedPiece.ClearAttackCells(); // clears attack cells
            mPieceManager.mSelectedPiece = null;

            //assign clicked object
            mPieceManager.mSelectedPiece = this;

            //test for cells
            CheckPathing();
            //show valid cells
            ShowCells();

            //test for cells to attack  
            CheckAttackPathing();
            //show valid cells to attack
            ShowAttackCells();
        }
        //if enemy piece selected attack
        else if ((mPieceManager.mSelectedPiece != this) && (mPieceManager.mSelectedPiece.mColor != this.mColor))
        {
            Debug.Log("enemy piece");
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

    /*
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        //test for cells
        CheckPathing();
        //show valid cells
        ShowCells();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        //follow pointer
        transform.position += (Vector3)eventData.delta;

        //check for overlapping available square
        foreach (Cell cell in mHighlightedCells)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, Input.mousePosition))
            {
                //if mouse is within valid cell, get it and break
                mTargetCell = cell;
                break;
            }

            //if mouse is not within a highlighted cell, not valid move
            mTargetCell = null;
        }

    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        //Hide
        ClearCells();

        //return to original position
        if (!mTargetCell)
        {
            //transform the position of the component to its original position
            transform.position = mCurrentCell.gameObject.transform.position;
            return;
        }

        //move to new cell
        Move();

        //end turn
        mPieceManager.SwitchSides(mColor);

    }
    */
    #endregion 
}