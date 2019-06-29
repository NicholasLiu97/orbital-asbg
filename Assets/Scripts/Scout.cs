using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scout : BasePiece
{
    //private new int CurrentHealth = 4;
    private int ScoutMaxHealth = 4;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = ScoutMaxHealth;
        maxHealth = ScoutMaxHealth;
        mAttack = 3;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        mMovement = new Vector3Int(3, 3, 3);
        if (newTeamColor == Color.blue)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueScout_1.0");
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("RedScout_1.0");
        }

        mMovement = new Vector3Int(3, 3, 0);
        
    }

    public override void Reset()
    {
        Kill();
        Place(mOriginalCell);
        CurrentHealth = ScoutMaxHealth;
        healthSlider.value = ScoutMaxHealth;
        healthText.text = ScoutMaxHealth.ToString();
    }

   
}
