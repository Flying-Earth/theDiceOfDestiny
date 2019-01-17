using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Animator animator;
    private bool bSelected;
    [Header("01为剑，23为盾, 45为电")]
    public Material[] DiceMat;
    private int diceIndex;

	// Use this for initialization
	void Start () {
        animator = transform.GetComponent<Animator>();
        DiceManager.throwDices += throwDice;
        GameManager.getCounter += ReportCounter;
        bSelected = true;
	}

    void throwDice()
    {
        if (bSelected)
        {
            StartCoroutine(thrDice());
        }
    }

    IEnumerator thrDice()
    {
        animator.SetTrigger("Rotate");
        bSelected = false;
        transform.GetComponent<MeshRenderer>().material = DiceMat[diceIndex];
        yield return new WaitForSeconds(0.3f);
        diceIndex = Random.Range(0, 3);
        transform.GetComponent<MeshRenderer>().material = DiceMat[diceIndex];
    }
    private void OnMouseDown()
    {
        if (DiceManager.instance.IsReseting)
        {
            if (!bSelected)
            {
                bSelected = true;
                transform.GetComponent<MeshRenderer>().material = DiceMat[diceIndex + 3];
            }
            else
            {
                bSelected = false;
                transform.GetComponent<MeshRenderer>().material = DiceMat[diceIndex];
            }
        }
    }

    public void ResetDice()
    {
        bSelected = true;
    }
    void ReportCounter()
    {
        if (diceIndex == 0 )
            GameManager.instance.swordCount++;
        else if (diceIndex == 1)
            GameManager.instance.shieldCount++;
        else if (diceIndex == 2)
            GameManager.instance.chargeCount++;
        transform.GetComponent<MeshRenderer>().material = DiceMat[diceIndex];
    }
    private void OnDestroy()
    {
        DiceManager.throwDices -= throwDice;
        GameManager.getCounter -= ReportCounter;
    }
}
