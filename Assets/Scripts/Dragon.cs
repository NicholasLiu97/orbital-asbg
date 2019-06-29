using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dragon : BasePiece
{
    //private new int CurrentHealth = 8;
    private int DragonMaxHealth = 8;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = DragonMaxHealth;
        maxHealth = DragonMaxHealth;
        mAttack = 4;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        if (newTeamColor == Color.blue)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueDragon_1.0");
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("RedDragon_1.0");
        }
    }

    public override void Kill()
    {
        base.Kill();

        mPieceManager.mIsKingAlive = false;
    }
    
    public override void Reset()
    {
        Kill();
        Place(mOriginalCell);
        CurrentHealth = DragonMaxHealth;
        healthSlider.value = DragonMaxHealth;
        healthText.text = DragonMaxHealth.ToString();
    }
}
