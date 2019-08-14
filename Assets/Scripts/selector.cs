using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selector : MonoBehaviour
{

    //    Button DragonButton;
    //    [HideInInspector] public GameObject DragonOutline;
    //    [HideInInspector] public GameObject KnightOutline;
    //    [HideInInspector] public GameObject TitanOutline;
    public Texture2D cursorTexture;

    public SelectionState GlobalObject;
    private string Player1Select;
    private string Player2Select;
    public ParticleSystem FireParticle;

    // Start is called before the first frame update
    void Start()
    {
        //        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        // DragonButton = GameObject.Find("Dragon Button").GetComponent<Button>();
        //DragonOutline = GameObject.Find("Dragon Outline");
        //KnightOutline = GameObject.Find("Knight Outline");
        //TitanOutline = GameObject.Find("Titan Outline");

        // Toggle fullscreen (added by yh)
        //Screen.SetResolution(1600, 1000, false);
        //        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        /*
        FireParticle = GetComponentInChildren<ParticleSystem>();
        var em = FireParticle.emission;
        em.enabled = true;
        FireParticle.Play();
        */
    }

    /*
    private void Update()
    {
        FireParticle = GetComponentInChildren<ParticleSystem>();
        var em = FireParticle.emission;
        em.enabled = true;
        FireParticle.Play();
    }
    */

    public void PlayerOneDone()
    {
        Debug.Log("Done button clicked");
        if (GameObject.Find("Dragon Outline").GetComponent<Image>().enabled)
        {
            Player1Select = "D";
            Debug.Log("Dragon selected");
        } else if (GameObject.Find("Knight Outline").GetComponent<Image>().enabled)
        {
            Player1Select = "G";
            Debug.Log("Knight selected");
        } else if (GameObject.Find("Titan Outline").GetComponent<Image>().enabled)
        {
            Player1Select = "T";
            Debug.Log("Titan selected");
        } else
        {
            Debug.Log("unable to tell if image is selected");
        }
        Debug.Log("number for player 1 piece is " + Player1Select);
        SelectionState.Instance.Player1Piece = Player1Select;
        Debug.Log("Player 1 global = " + SelectionState.Instance.Player1Piece);

    }

    public void PlayerTwoDone()
    {
        Debug.Log("Done button clicked");
        if (GameObject.Find("R_Dragon Outline").GetComponent<Image>().enabled)
        {
            Player2Select = "D";
            Debug.Log("R_Dragon selected");
        }
        else if (GameObject.Find("R_Knight Outline").GetComponent<Image>().enabled)
        {
            Player2Select = "G";
            Debug.Log("R_Knight selected");
        }
        else if (GameObject.Find("R_Titan Outline").GetComponent<Image>().enabled)
        {
            Player2Select = "T";
            Debug.Log("R_Titan selected");
        }
        else
        {
            Debug.Log("unable to tell if image is selected");
        }
        Debug.Log("number for player 2 piece is " + Player2Select);
        SelectionState.Instance.Player2Piece = Player2Select;
        Debug.Log("Player 1 global = " + SelectionState.Instance.Player1Piece);
        Debug.Log("Player 2 global = " + SelectionState.Instance.Player2Piece);

    }

/*    
    public void SavePlayer1Data()
    {
        SelectionState.Instance.Player1Piece = Player1Select;
        Debug.Log("Player 1 global = " + SelectionState.Instance.Player1Piece);
    }
*/

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("game scene should be loaded");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exited the game");
    }
}
