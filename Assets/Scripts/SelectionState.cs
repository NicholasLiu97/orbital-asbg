using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : MonoBehaviour
{
    public static SelectionState Instance;
    [HideInInspector] public string Player1Piece;
    [HideInInspector] public string Player2Piece;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
