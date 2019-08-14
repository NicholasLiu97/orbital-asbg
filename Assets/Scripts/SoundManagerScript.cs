using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip PlayerHit,PlayerDeath,Mouseclick,Victory;
    static AudioSource audioS;
    void Start()
    {
        PlayerDeath = Resources.Load<AudioClip>("PlayerDeath");
        PlayerHit = Resources.Load<AudioClip>("PlayerHit");
        Mouseclick = Resources.Load<AudioClip>("Mouseclick");
        Victory = Resources.Load<AudioClip>("Victorysound");
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip){ 
            case "hit":
                audioS.PlayOneShot(PlayerHit);
                break;
            case "death":
                audioS.PlayOneShot(PlayerDeath);
                break;
            case "click":
                audioS.PlayOneShot(Mouseclick);
                break;
            case "victory":
                audioS.PlayOneShot(Victory);
                break;
        }
    }
}
