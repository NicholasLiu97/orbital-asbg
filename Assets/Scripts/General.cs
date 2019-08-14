using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class General : BasePiece
{
    //private new int CurrentHealth = 10;
    private int GenMaxHealth = 10;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = GenMaxHealth;
        maxHealth = GenMaxHealth;
        mAttack = 3;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        //movement
        mMovement = new Vector3Int(1, 1, 1);
        mAttackRange = new Vector3Int(2, 2, 1);

        //animation
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("attacked", false);
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (newTeamColor == Color.blue)
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("BlueGeneral_1.0");
        }
        else
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("RedGeneral_1.0");
        }

    }

    public override void Kill()
    {
        base.Kill();

        mPieceManager.mIsKingAlive = false;
    }
    //ad
    public override void Reset()
    {
        Kill();
        CurrentHealth = maxHealth;
        UpdateHealth();
        Place(mOriginalCell);
    }
}
