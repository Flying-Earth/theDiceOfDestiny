using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public Slider HealthBar;
    public Text AttackCount;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public int attack;
    [HideInInspector]
    public int targetCounter = 0;
    private int maxHealth;
    public Animator animator;
    public Material[] enemyType;
    private bool isDead =false;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    private void Awake()
    {
        GameManager.instance.enemies.Add(this);
    }
    public void Damage(int damage)
    {
        health -= damage;
        HealthBar.value = health;
        Debug.Log("health:" + health);
        if (health <= 0)
        {
            GameManager.instance.enemyCount--;
            isDead = true;
            gameObject.SetActive(false);
            targetCounter = 0;
        }
    }
    private void OnMouseDown()
    {
        if (!GameManager.instance.IsAbility)
        {
            if (GameManager.instance.targetCounterSum() < GameManager.instance.swordCount &&
                GameManager.instance.IsChoosing)
            {
                targetCounter++;
                UpdateText();
            }
        }
        else
        {
            
            Player.instance.StartAbility(this);
            GameManager.instance.IsAbility = false;
        }
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (targetCounter > 0 && GameManager.instance.IsChoosing)
            {
                targetCounter--;
                UpdateText();
            }
        }
    }
    private void OnMouseEnter()
    {
        animator.SetBool("CanSelect",true);
    }
    private void OnMouseExit()
    {
        animator.SetBool("CanSelect", false);
    }
    public void UpdateText()
    {
        AttackCount.text = "攻击次数：" + targetCounter;
    }
    public void ChangeProperty(int Num)
    {
        transform.GetComponent<MeshRenderer>().material = enemyType[Num];
        if(Num==0)
        {
            health = 60;
            attack = 20;
        }
        if (Num == 1)
        {
            health = 200;
            attack = 40;
        }
        if (Num == 2)
        {
            health = 40;
            attack = 20;
        }
        if (Num == 3)
        {
            health = 250;
            attack = 45;
        }
        if (Num == 4)
        {
            health = 30;
            attack = 20;
        }
        if (Num == 5)
        {
            health = 50;
            attack = 30;
        }
        if (Num == 6)
        {
            health = 250;
            attack = 60;
        }
        if (Num == 7)
        {
            health = 500;
            attack = 70;
        }
        maxHealth = health;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;
    }
}
