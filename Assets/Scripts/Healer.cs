using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healer : BasePiece
{
    //private new int CurrentHealth = 6;
    private int HealerMaxHealth = 6;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = HealerMaxHealth;
        maxHealth = HealerMaxHealth;
        mAttack = 2;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        if (newTeamColor == Color.blue)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueHealer_1.0");
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("RedHealer_1.0");
        }


        mAttackRange = new Vector3Int(2, 2, 0);
    }

    public override void Reset()
    {
        Kill();
        Place(mOriginalCell);
        CurrentHealth = HealerMaxHealth;
        healthSlider.value = HealerMaxHealth;
        healthText.text = HealerMaxHealth.ToString();
    }
}
