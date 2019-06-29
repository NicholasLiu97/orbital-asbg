using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [HideInInspector] public bool mIsKingAlive = true;

    [HideInInspector] public BasePiece mSelectedPiece = null;
    [HideInInspector] public bool mSelectedPieceMoved = false;


    public GameObject mPiecePrefab;

    private List<BasePiece> mWhitePieces = null;
    private List<BasePiece> mBlackPieces = null;

    private string[] mPieceOrder = new string[18]
    {
        "P","P","P","P","P","P","P","P","P",
        "Sc","C","B","H","G","H","B","C","Sc"
    };

    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P", typeof(Pawn) },
        {"C", typeof(Cavalry) },
        {"B", typeof(Bowman) },
        {"H", typeof(Healer) },
        {"G", typeof(General) }, //TODO, add option for switching type of generals
        {"Sc", typeof(Scout) },

    };

    public void Setup(Board board)
    {
        //create blue(white)
        mWhitePieces = CreatePieces(Color.blue, new Color32(255, 255, 255, 255), board);

        //create red(black)
        mBlackPieces = CreatePieces(Color.red, new Color32(255, 255, 255, 255), board);

        //place pieces
        PlacePieces(1, 0, mWhitePieces, board);
        PlacePieces(7, 8, mBlackPieces, board);

        //white goes first
        //switch sides
        SwitchSides(Color.red);
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < mPieceOrder.Length; i++)
        {
            //create new object
            GameObject newPieceObject = Instantiate(mPiecePrefab);
            newPieceObject.transform.SetParent(transform);

            //set scale and position
            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;

            //get the type 
            string key = mPieceOrder[i];
            Type pieceType = mPieceLibrary[key];

            //store new piece
            BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
            newPieces.Add(newPiece);

            //setup pieces
            newPiece.Setup(teamColor, spriteColor, this);
        }

        return newPieces;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board)
    {
        for (int i = 0; i < 9; i++)
        {
            //place pawns
            pieces[i].Place(board.mAllCells[i, pawnRow]);

            //place royalty
            pieces[i+9].Place(board.mAllCells[i, royaltyRow]);
        }
    }

    private void SetInteractive(List<BasePiece> allPieces, bool value)
    {
        //enable each piece to be used
        foreach (BasePiece piece in allPieces)
            piece.enabled = value;
    }

    public void SwitchSides(Color color)
    {
        //check if king is alive
        if (!mIsKingAlive)
        {
            //reset game
            ResetPieces();

            //Set King to be alive
            mIsKingAlive = true;

            //Change color to red(black), so white can go first again
            color = Color.red;
        }

        //check whose turn it is
        bool isBlackTurn = color == Color.blue ? true : false;

        //set interactivity
        SetInteractive(mWhitePieces, !isBlackTurn);
        SetInteractive(mBlackPieces, isBlackTurn);

    }

    public void ResetPieces()
    {
        //reset white
        foreach (BasePiece piece in mWhitePieces)
            piece.Reset();

        //reset black
        foreach (BasePiece piece in mBlackPieces)
            piece.Reset();
    }


}
