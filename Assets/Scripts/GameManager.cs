using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Texture2D cursorTexture;

    public Board mBoard;

    public PieceManager mPieceManager;
    void Start()
    {
        mBoard.Create();
        
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        mPieceManager.Setup(mBoard);
    }

    
}
