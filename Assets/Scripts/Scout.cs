using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Scout : BasePiece
{
    //private new int CurrentHealth = 5;
    private int ScoutMaxHealth = 5;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        mPieceManager = newPieceManager;

        mColor = newTeamColor;
        GetComponent<Image>().color = newSpriteColor;
        mRectTransform = GetComponent<RectTransform>();

        //assigning health
        CurrentHealth = ScoutMaxHealth;
        maxHealth = ScoutMaxHealth;
        mAttack = 2;

        //Setting up the piece health
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = CurrentHealth;
        healthText = GetComponentInChildren<Text>();

        healthText.text = CurrentHealth.ToString();

        //animation
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("attacked", false);
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        
        if (newTeamColor == Color.blue)
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("BlueScout_1.0");
        }
        else
        {
            GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("RedScout_1.0");
        }

        mMovement = new Vector3Int(8, 8, 0);
        mAttackRange = new Vector3Int(1, 1, 0);

    }

    public override void Reset()
    {
        Kill();
        CurrentHealth = maxHealth;
        UpdateHealth();
        Place(mOriginalCell);
    }

   
}
