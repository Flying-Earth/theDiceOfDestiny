using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animator healthAnimator;
    public Animator armorAnimator;
    public Animator attackAnimator;
    public Animator chargeAnimator;
    public Animator fightAnimator;
    public Animator AbsorbAnimator;
    public Animator SelectCardAnimator;
    public static AnimationManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void InvokeAddAnimation(Animator animator)
    {
        animator.SetTrigger("Add");
    }
    public void InvokeMinusAnimation(Animator animator)
    {
        animator.SetTrigger("Minus");
    }
	public void FightEnemy()
    {
        fightAnimator.SetTrigger("FightEnemy");

    }
    public void FightPlayer()
    {
        fightAnimator.SetTrigger("FightPlayer");

    }
    public void FightAbility()
    {
        fightAnimator.SetTrigger("FightAbility");

    }
    public void CardHighlight(bool bHighlight)
    {
        SelectCardAnimator.SetBool("CanSelect", bHighlight);
    }

}
