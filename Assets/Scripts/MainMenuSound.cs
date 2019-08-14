using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    public Texture2D cursorTexture;

    public static AudioClip Mouseclick, MouseOver;
    static AudioSource audioS;
    void Start()
    {
        Mouseclick = Resources.Load<AudioClip>("Mouseclick");
        MouseOver = Resources.Load<AudioClip>("mouseover");
        audioS = GetComponent<AudioSource>();

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MainMenuSound.PlaySound("click");
        }
    }

    public static void PlaySound(string clip)
    {

        if (audioS)
        {
            switch (clip)
            {
                case "click":
                    audioS.PlayOneShot(Mouseclick);
                    break;
                case "mouseover":
                    audioS.PlayOneShot(MouseOver);
                    break;
            }
        }
        
    }
}
