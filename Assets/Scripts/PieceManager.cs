using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PieceManager : MonoBehaviour
{
    [HideInInspector] public bool mIsKingAlive = true;

    [HideInInspector] public BasePiece mSelectedPiece = null;
    [HideInInspector] public bool mSelectedPieceMoved = false;
    [HideInInspector] public string Player1Piece = SelectionState.Instance.Player1Piece;
    [HideInInspector] public string Player2Piece = SelectionState.Instance.Player2Piece;

    private string[] bPieceOrder;
    private string[] rPieceOrder;
    private string[] mPieceOrder;

    private int TurnNum;

    public GameObject mPiecePrefab;
    public GameObject RedPlayerTurn;
    public GameObject BluePlayerTurn;
    public GameObject PlayerWinner;
//    public GameObject WinnerText;

    private List<BasePiece> mWhitePieces = null;
    private List<BasePiece> mBlackPieces = null;


/*
    private string[] mPieceOrder = new string[18]
    {
        "P","P","P","P","P","P","P","P","P",
        "Sc","C","B","H","G","H","B","C","Sc"
    };
*/

    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P", typeof(Pawn) },
        {"C", typeof(Cavalry) },
        {"B", typeof(Bowman) },
        {"H", typeof(Healer) },
        {"G", typeof(General) },
        {"D", typeof(Dragon) },
        {"T", typeof(Titan) },
        {"Sc", typeof(Scout) }


    };

    public void Setup(Board board)
    {
        bPieceOrder = new string[18]
        {
            "P","P","P","P","P","P","P","P","P",
            "Sc","C","B","H", Player1Piece,"H","B","C","Sc"
        };

        rPieceOrder = new string[18]
        {
            "P","P","P","P","P","P","P","P","P",
            "Sc","C","B","H", Player2Piece,"H","B","C","Sc"
        };

        //create blue(white)
        mWhitePieces = CreatePieces(Color.blue, new Color32(255, 255, 255, 0), board);

        //create red(black)
        mBlackPieces = CreatePieces(Color.red, new Color32(255, 255, 255, 0), board);

        //place pieces
        PlacePieces(1, 0, mWhitePieces, board);
        PlacePieces(7, 8, mBlackPieces, board);

         Debug.Log("P1 = " + Player1Piece + " P2 = " + Player2Piece);

        TurnNum = 1;

        //white goes first
        //switch sides
        SwitchSides(Color.red);
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board)
    {
        
        if (teamColor == Color.blue)
        {
            mPieceOrder = bPieceOrder;
        } else
        {
            mPieceOrder = rPieceOrder;
        }
        
        List<BasePiece> newPieces = new List<BasePiece>();

//        Debug.Log("length of mPieceOrder = " + mPieceOrder.Length);

        for (int i = 0; i < mPieceOrder.Length; i++)
        {
            //create new object
            GameObject newPieceObject = Instantiate(mPiecePrefab);
            newPieceObject.transform.SetParent(transform); //make the transform keep local orientation rather than global orientation

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
        //enable each piece to be used when side is switched
        foreach (BasePiece piece in allPieces)
            piece.enabled = value;
    }

    public void SwitchSides(Color color)
    {
        if (TurnNum == 1)
        {
            //check if king is alive
            if (!mIsKingAlive)
            {
                //reset game
                ResetPieces(color);

                //Set King to be alive
                mIsKingAlive = true;

                //Change color to red(black), so white can go first again
                color = Color.red;

                TurnNum = 0;
            }

            //check whose turn it is
            bool isBlackTurn = color == Color.blue ? true : false;

            //set interactivity
            SetInteractive(mWhitePieces, !isBlackTurn);
            SetInteractive(mBlackPieces, isBlackTurn);

            TurnNum = 0;

            //toggle turn icons
            if (!isBlackTurn) // if blue turn
            {
                BluePlayerTurn.SetActive(true);
                RedPlayerTurn.SetActive(false);
            }
            else //red turn
            {
                BluePlayerTurn.SetActive(false);
                RedPlayerTurn.SetActive(true);
            }
        }
        else
        {
            TurnNum = 1;
            //check if king is alive
            if (!mIsKingAlive)
            {
                //reset game
                ResetPieces(color);

                //Set King to be alive
                mIsKingAlive = true;

                //Change color to red(black), so white can go first again
                color = Color.red;

                TurnNum = 0;
            }
            
        }
    }

    public void ResetPieces(Color color)
    {
        Debug.Log("game ended");
        if (color == Color.blue)
        {
            Debug.Log("color now is blue");

        } else if (color == Color.red)
        {
            Debug.Log("color now is red");
        } else
        {
            Debug.Log("no color found");
        }

        SetInteractive(mWhitePieces, false);
        SetInteractive(mBlackPieces, false);

        PlayerWinner.SetActive(true);
        TextMeshProUGUI mText = PlayerWinner.GetComponentInChildren<TextMeshProUGUI>();
        SoundManagerScript.PlaySound("victory");
        
        if (color == Color.blue)
        {
            mText.text = "PLAYER 1 WINS!";
        } else
        {
            mText.text = "PLAYER 2 WINS!";
        }

/*
        //reset white
        foreach (BasePiece piece in mWhitePieces)
            piece.Reset();

        //reset black
        foreach (BasePiece piece in mBlackPieces)
            piece.Reset();
*/

    }
}
