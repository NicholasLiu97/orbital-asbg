    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Titan : BasePiece
{
    //private new int CurrentHealth = 12;
    private int TitanMaxHealth = 12;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = TitanMaxHealth;
        maxHealth = TitanMaxHealth;
        mAttack = 3;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        if (newTeamColor == Color.blue)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueTitan_1.0");
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("RedTitan_1.0");
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
        CurrentHealth = TitanMaxHealth;
        healthSlider.value = TitanMaxHealth;
        healthText.text = TitanMaxHealth.ToString();
    }
}
