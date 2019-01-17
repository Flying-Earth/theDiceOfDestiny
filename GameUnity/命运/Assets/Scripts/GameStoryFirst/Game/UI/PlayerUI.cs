using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject healthUI;
    public GameObject attackUI;
    public GameObject shieldUI;
    public GameObject chargeUI;
    private Text healthTxt;
    private Text attackTxt;
    private Text chargeTxt;
    private Text shieldTxt;
	// Use this for initialization
	void Start () {
        healthTxt = healthUI.GetComponentInChildren<Text>();
        attackTxt = attackUI.GetComponentInChildren<Text>();
        shieldTxt = shieldUI.GetComponentInChildren<Text>();
        chargeTxt = chargeUI.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        healthTxt.text = Player.instance.Health.ToString();
        attackTxt.text = Player.instance.Attack.ToString();
        shieldTxt.text = Player.instance.Armor.ToString();
        chargeTxt.text = Player.instance.Charge.ToString();
	}
}
