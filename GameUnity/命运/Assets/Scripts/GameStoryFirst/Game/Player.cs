using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance = null;
    public AudioClip BuffClip;
    public AudioClip NerfClip;
    public AudioSource FightSFX;
    public AudioSource SFX;
    //Basic Properties
    private int maxHealth = 400;
    private int health = 400;
    private int attack = 20;
    private int armor = 15;
    private int fullCharge = 8;
    //Actual Properties
    private int absorbDamage = 0;
    private int charge = 0;
    public int Attack
    {
        get
        {
            return attack;
        }
        set
        {
            int temp = attack;
            attack = value;
            if (temp - attack > 0)
            {
                AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.attackAnimator);
                SFX.PlayOneShot(NerfClip);
            }
            else if (temp < attack)
            {
                AnimationManager.instance.InvokeAddAnimation(AnimationManager.instance.attackAnimator);
                SFX.PlayOneShot(BuffClip);
            }
        }
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Armor
    {
        get { return armor; }
        set
        {
            int temp = armor;
            armor = value;
            if (temp - armor > 0)
            {
                AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.armorAnimator);
                SFX.PlayOneShot(NerfClip);
            }
            else if (temp < armor)
            {
                AnimationManager.instance.InvokeAddAnimation(AnimationManager.instance.armorAnimator);
                SFX.PlayOneShot(BuffClip);
            }
        }
    }

    public int FullCharge
    {
        get { return fullCharge; }
        set { fullCharge = value; }
    }
    public int MaxHealth
    {
        set
        {
            maxHealth = value;
            if (health > maxHealth)
                health = maxHealth;
        }
        get
        {
            return maxHealth;
        }
    }
    public int Charge
    {
        get { return charge; }
        set
        {
            int temp = charge;
            charge = value;
            if (temp - charge > 0)
            {
                AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.chargeAnimator);
                SFX.PlayOneShot(NerfClip);
            }
            else if (temp < charge)
            {
                AnimationManager.instance.InvokeAddAnimation(AnimationManager.instance.chargeAnimator);
                SFX.PlayOneShot(BuffClip);
            }
        }
    }
    public int AbsorbDamage
    {
        get
        {
            return absorbDamage;
        }
        set
        {
            int temp = absorbDamage;
            absorbDamage = value;
            if (temp > absorbDamage)
            {
                AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.AbsorbAnimator);
                SFX.PlayOneShot(NerfClip);
            }
            else if (temp < absorbDamage)
            {
                AnimationManager.instance.InvokeAddAnimation(AnimationManager.instance.AbsorbAnimator);
                SFX.PlayOneShot(BuffClip);
            }
        }
    }
	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
        }
    }
	public void StartAbility(Enemy target)
    {
        StartCoroutine(Ability(target));
    }
    IEnumerator Ability(Enemy target)
    {
        AnimationManager.instance.FightAbility();
        FightSFX.Play();
        AbsorbDamage += 60;
        yield return new WaitForSeconds(1f);
        target.Damage(150);
        Charge = 0;
    }
    public void CalculateCounter()
    {
        AbsorbDamage += armor * GameManager.instance.shieldCount;
        Charge += GameManager.instance.chargeCount;
        if (charge >= fullCharge)
            charge = fullCharge;
    }
    public void Damage(int damage)
    {
        AbsorbDamage -= damage;
        if (AbsorbDamage < 0)
        {
            health += AbsorbDamage;
            AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.healthAnimator);
            SFX.PlayOneShot(NerfClip);
            absorbDamage = 0;
        }
        if (health <= 0)
        {
            GameManager.instance.GameEnd();
        }
        Debug.Log("myhealth:" + health+",Absorb:"+absorbDamage);


    }
    public void ResetProperty()
    {
        absorbDamage = 0;
        charge = 0;
    }

    
    public void Recover(int recover)
    {
        health += recover;
        AnimationManager.instance.InvokeAddAnimation(AnimationManager.instance.healthAnimator);
        SFX.PlayOneShot(BuffClip);
        if (health > maxHealth)
            health = maxHealth;
    }
    public void DamageHealth(int damage)
    {
        health -= damage;
        AnimationManager.instance.InvokeMinusAnimation(AnimationManager.instance.healthAnimator);
        SFX.PlayOneShot(NerfClip);
        if (health <= 0)
        {
            GameManager.instance.GameEnd();
        }
    }
    public Player GetPlayer()
    {
        return instance;
    }

    public void SetPlayer(Player player)
    {
        health = player.health;
        armor = player.armor;
        maxHealth = player.maxHealth;
        attack = player.attack;
        fullCharge = player.fullCharge;
}
}
