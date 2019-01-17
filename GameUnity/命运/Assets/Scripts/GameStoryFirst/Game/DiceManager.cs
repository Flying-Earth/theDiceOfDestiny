using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {

    public static DiceManager instance = null;
    public delegate void ThrowYourDice();
    public static event ThrowYourDice throwDices;
    public Dice[] dices;
    private bool isReseting = false;
    public AudioClip DiceClip;
    private AudioSource SFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        SFX = gameObject.GetComponent<AudioSource>();
    }
    public bool IsReseting
    {
        get
        {
            return isReseting;
        }

        set
        {
            isReseting = value;
        }
    }
	// Use this for initialization
    public void ThrowDices()
    {
        if (throwDices != null)
        {
            throwDices();
            SFX.PlayOneShot(DiceClip);
        }
    }

    public void ResetDices()
    {
        foreach(var dice in dices)
        {
            dice.ResetDice();
        }
    }
}
