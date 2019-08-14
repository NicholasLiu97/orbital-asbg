using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Texture2D cursorTexture;

    public Board mBoard;

    public PieceManager mPieceManager;

    [HideInInspector] public string Player1Piece;
    [HideInInspector] public string Player2Piece;

    void Start()
    {
        Player1Piece = SelectionState.Instance.Player1Piece;
        Player2Piece = SelectionState.Instance.Player2Piece;
        Debug.Log("Player 1 piece is " + Player1Piece + " and Player 2 piece is " + Player2Piece);

        mBoard.Create();
        Debug.Log("Start Game");
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        mPieceManager.Setup(mBoard);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManagerScript.PlaySound("click");
        }
    }

}
