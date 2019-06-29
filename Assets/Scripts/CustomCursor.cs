using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;

    private void Start()
    {
        Debug.Log("cursor");
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        
    }
}
