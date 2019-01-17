using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

    private Text txt;
	// Use this for initialization
	void Start () {
        txt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = "攻击次数：" + GameManager.instance.swordCount + "\n伤害吸收：" + Player.instance.AbsorbDamage;
	}
}
