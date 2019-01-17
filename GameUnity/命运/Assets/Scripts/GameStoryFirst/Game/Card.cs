using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    private AudioSource SFX;
    public AudioClip Abandon;
    private void Start()
    {
        GameManager.abandonCard += AbandonCard;
        SFX = gameObject.GetComponent<AudioSource>();
    }
    public void AbandonCard()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Abandon");
        SFX.PlayOneShot(Abandon);
        GameManager.abandonCard -= AbandonCard;
    }
    public Material[] CardMat;
    public void changeMat(int cardNum)
    {
        transform.GetComponent<MeshRenderer>().material = CardMat[cardNum];
    }
    private void OnDestroy()
    {
        GameManager.abandonCard -= AbandonCard;
    }
}
